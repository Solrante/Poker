using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Cliente_Poker
{
    /// <summary>
    /// Definicion de la vista de juego blackjack
    /// </summary>
    public partial class SalaBlackJack : Form
    {
        /// <summary>
        /// Instancia del formulario principal del cliente
        /// </summary>
        MenuPrincipal MenuPrincipal;

        /// <summary>
        /// Instancia de la conexión al servidor
        /// </summary>
        ConexionServidor conexion;

        /// <summary>
        /// Delegado para cambiar la imagen de un picturebox
        /// </summary>
        private delegate void DelCambiarImagenPictureBox(PictureBox p, string s);

        /// <summary>
        /// Delegado para cambiar el texto de un label
        /// </summary>
        private delegate void DelCambiarTextoLabel(Label l, string s);

        /// <summary>
        /// Delegado para cambiar la visibilidad de un control
        /// </summary>
        private delegate void DelCambiarVisibilidadControl(Control c, bool b);

        /// <summary>
        /// Delegado para modificar el listado de controles
        /// </summary>
        private delegate void DelModificarControls(Control c, bool b);

        /// <summary>
        /// Instancia del delegado para cambiar imagen a un picturebox
        /// </summary>
        private DelCambiarImagenPictureBox imagenPictureBox;

        /// <summary>
        /// Instancia del delegado para cambiar el texto de un label
        /// </summary>
        private DelCambiarTextoLabel textoLabel;

        /// <summary>
        /// Instancia del delegado para cambia la visibilidad de un control
        /// </summary>
        private DelCambiarVisibilidadControl visibilidad;

        /// <summary>
        /// Instancia de delegado que cambia el atributo enable de un control
        /// </summary>
        private DelCambiarVisibilidadControl activo;

        /// <summary>
        /// Instancia del delegado para modificar el listado de controles.
        /// </summary>
        private DelModificarControls controles;

        /// <summary>
        /// Bandera para la lectura de información del servidor
        /// </summary>
        private bool bRecogerRespuesta;

        /// <summary>
        /// Posicion dentro del cliente para la colocacion de nuevas cartas del crupier
        /// </summary>
        private int posXJugador;

        /// <summary>
        /// Posicion dentro del cliente para la colocacion de nuevas cartas del jugador
        /// </summary>
        private int posXCrupier;

        /// <summary>
        /// Cantidad de pixel entre cartas
        /// </summary>
        private int separaciónCartas = 60;

        /// <summary>
        /// Objeto para control de uso de elementos comunes
        /// </summary>
        static readonly private object l = new object();

        /// <summary>
        /// Saldo
        /// </summary>
        private double saldo;

        /// <summary>
        /// Indica si se esta en la fase de reinicio entre partidas
        /// </summary>
        private bool finMano = false;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:Cliente_Poker.SalaBlackJack"/>.
        /// </summary>
        /// <param name="menuPrincipal">Instancia del formulario principal del cliente.</param>
        public SalaBlackJack(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            MenuPrincipal = menuPrincipal;
            imagenPictureBox = new DelCambiarImagenPictureBox(cambiarImagenCarta);
            textoLabel = new DelCambiarTextoLabel(TextoLabel);
            visibilidad = new DelCambiarVisibilidadControl(CambiarVisibilidad);
            controles = new DelModificarControls(ModificarListadoControls);
            activo = new DelCambiarVisibilidadControl(CambiarActivo);
            conexion = MenuPrincipal.conexion;
            saldo = menuPrincipal.usuario.Saldo;
            lblSaldo.Text = saldo.ToString();
            new Thread(HiloComunicacion).Start();
        }

        /// <summary>
        /// Gestiona el evento click del boton salir de sala
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btnSalirSala_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje(Clave.Volver);
            MenuPrincipal.recibirUsuario(conexion.recibirMensaje());
            conexion.enviarMensaje(Clave.ListaSalas);
            MenuPrincipal.actualizarSalas();
            MenuPrincipal.Show();
            Dispose();
        }

        /// <summary>
        /// Gestiona el evento click de controles del juego
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void ClickListener(object sender, EventArgs e)
        {
            if (!finMano)
            {
                string mensaje = "";
                bool ficha = false;
                Button btn = sender as Button;
                if (btn == btnFichaUno || btn == btnFichaDos || btn == btnFichaTres)
                {
                    mensaje = Clave.Ficha + Clave.Separador + btn.Tag.ToString();
                    ficha = true;
                }
                if (btn == btnPedir)
                {
                    mensaje = Clave.Pedir;
                    Invoke(visibilidad, btnDoblar, false);
                    Invoke(visibilidad, btnRetirarse, false);
                }
                if (btn == btnPlantarse)
                {
                    mensaje = Clave.Plantarse;
                }
                if (btn == btnDoblar)
                {
                    mensaje = Clave.Doblar;
                    Invoke(visibilidad, btnDoblar, false);
                }
                if (btn == btnRetirarse)
                {
                    mensaje = Clave.Retirarse;
                }
                if (ficha)
                {
                    cambiarVisibilidadFichas(false);
                }
                conexion.enviarMensaje(mensaje);
                bRecogerRespuesta = true;
            }
        }

        /// <summary>
        /// Cambia la visibilidad de los controles del juego segun un parametro recibido como parametro
        /// </summary>
        /// <param name="estado">Modificador de estado.</param>
        private void cambiarVisibilidadFichas(bool estado)
        {
            Invoke(visibilidad, btnFichaTres, estado);
            Invoke(visibilidad, btnFichaDos, estado);
            Invoke(visibilidad, btnFichaUno, estado);
            Invoke(visibilidad, btnPedir, !estado);
            Invoke(visibilidad, btnPlantarse, !estado);
            Invoke(visibilidad, btnDoblar, !estado);
            Invoke(visibilidad, btnRetirarse, !estado);
        }

        /// <summary>
        /// Carga una carta recibida como cadena
        /// </summary>
        /// <param name="respuesta">Carta.</param>
        private void cargarCarta(string respuesta)
        {
            if (respuesta.Split(Clave.Separador)[1] == Clave.Jugador)
            {
                cargarCartaJugador(respuesta);
            }
            else
            {
                cargarCartaCrupier(respuesta);
            }
        }

        /// <summary>
        /// Carga una carta recibida como cadena
        /// </summary>
        /// <param name="respuesta">Carta.</param>
        private void cargarCartaJugador(string respuesta)
        {
            if (cartaJugador1.Image == null)
            {
                posXJugador = cartaJugador1.Location.X;
                Invoke(imagenPictureBox, cartaJugador1, respuesta.Split(Clave.Separador)[2] + Clave.Separador + respuesta.Split(Clave.Separador)[3]);
            }
            else
            {
                PictureBox pb = new PictureBox();
                posXJugador += separaciónCartas;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Size = cartaJugador1.Size;
                Invoke(imagenPictureBox, pb, respuesta.Split(Clave.Separador)[2] + Clave.Separador + respuesta.Split(Clave.Separador)[3]);
                pb.Location = new System.Drawing.Point(posXJugador, cartaJugador1.Location.Y);
                Invoke(controles, pb, true);
            }
        }

        /// <summary>
        /// Carga una carta recibida como cadena
        /// </summary>
        /// <param name="respuesta">Carta.</param>
        private void cargarCartaCrupier(string respuesta)
        {
            if (cartaCrupier1.Image == null)
            {
                posXCrupier = cartaCrupier1.Location.X;
                Invoke(imagenPictureBox, cartaCrupier1, respuesta.Split(Clave.Separador)[2] + Clave.Separador + respuesta.Split(Clave.Separador)[3]);
            }
            else
            {
                PictureBox pb = new PictureBox();
                posXCrupier += separaciónCartas;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Size = cartaCrupier1.Size;
                Invoke(imagenPictureBox, pb, respuesta.Split(Clave.Separador)[2] + Clave.Separador + respuesta.Split(Clave.Separador)[3]);
                pb.Location = new System.Drawing.Point(posXCrupier, cartaCrupier1.Location.Y);
                Invoke(controles, pb, true);
            }
        }

        /// <summary>
        /// Actualiza el texto del valor del jugador o del crupier
        /// segun una respuesta recibida 
        /// </summary>
        /// <param name="respuesta">Respuesta.</param>
        private void actualizarValor(string respuesta)
        {
            if (respuesta.Split(Clave.Separador)[1] == Clave.Jugador)
            {
                Invoke(textoLabel, lblValorJugador, respuesta.Split(Clave.Separador)[2]);
            }
            else
            {
                Invoke(textoLabel, lblValorCrupier, respuesta.Split(Clave.Separador)[2]);
            }
        }

        /// <summary>
        /// Gestiona las respuestas del servidor recibidas como parametro
        /// </summary>
        /// <param name="respuesta">Respuesta.</param>
        private void actualizarEstado(string respuesta)
        {           
            switch (respuesta.Split(Clave.Separador)[0])
            {
                case Clave.Carta:
                    cargarCarta(respuesta);
                    break;
                case Clave.Saldo:
                    saldo = Convert.ToInt32(respuesta.Split(Clave.Separador)[1]);
                    Invoke(textoLabel, lblSaldo, saldo.ToString());
                    comprobarSaldo();
                    break;
                case Clave.Valor:
                    actualizarValor(respuesta);
                    break;
                case Clave.FinMano:
                    Invoke(textoLabel, lblResultado, respuesta.Split(Clave.Separador)[1]);
                    finMano = !finMano;
                    reset();
                    break;
                case Clave.FinEnvio:
                    bRecogerRespuesta = false;
                    break;
            }
        }

        /// <summary>
        /// Devuelve el juego al estado inicial de partida
        /// </summary>
        private void reset()
        {
            Thread.Sleep(500);
            lock (l)
            {
                Invoke(textoLabel, lblResultado, "");
                Invoke(textoLabel, lblValorCrupier, "");
                Invoke(textoLabel, lblValorJugador, "");
                cambiarVisibilidadFichas(true);
                for (int i = Controls.Count - 1; i > 0; i--)
                {
                    if (Controls[i].GetType() == typeof(PictureBox) && Controls[i] != cartaCrupier1 && Controls[i] != cartaJugador1)
                    {
                        Invoke(controles, Controls[i], false);
                    }
                }
                cartaCrupier1.Image = null;
                cartaJugador1.Image = null;
                comprobarSaldo();
                finMano = !finMano;
            }
        }

        /// <summary>
        /// Comprueba el saldo actual activando o desactivando las fichas si se tiene el suficiente o no.
        /// </summary>
        private void comprobarSaldo()
        {
            Invoke(activo, btnFichaUno, Convert.ToInt32(btnFichaUno.Tag) > saldo);
            Invoke(activo, btnFichaDos, Convert.ToInt32(btnFichaDos.Tag) > saldo);
            Invoke(activo, btnFichaTres, Convert.ToInt32(btnFichaTres.Tag) > saldo);
        }

        /// <summary>
        /// Añade o elimina un control recibido como parametro segun el parametro recibido como accion
        /// </summary>
        /// <param name="c">Control a tratar.</param>
        /// <param name="accion">Si esta definida como <c>true</c> añade si no elimina.</param>
        private void ModificarListadoControls(Control c, bool accion)
        {
            if (accion)
            {
                Controls.Add(c);
            }
            else
            {
                Controls.Remove(c);
            }
        }

        /// <summary>
        /// Cambia la imagen de un picturebox recibido como parametro segun una cadena recibida como parametro
        /// </summary>
        /// <param name="pb">Picture box a modificar.</param>
        /// <param name="text">Clave de busqueda para nueva imagen.</param>
        private void cambiarImagenCarta(PictureBox pb, string text)
        {
            pb.Image = Carta.GetMap()[text];
        }

        /// <summary>
        /// Cambia el texto de un label recibido como parametro por una cadena tambien
        /// recibida como parametro
        /// </summary>
        /// <param name="l">Label a modificar.</param>
        /// <param name="s">Cadena a insertar.</param>
        private void TextoLabel(Label l, string s)
        {
            l.Text = s;
        }

        /// <summary>
        /// Cambia el estado de "Enable" de un control recibido segun un parametro
        /// booleano tambien recibido 
        /// </summary>
        /// <param name="c">C.</param>
        /// <param name="activo">Si ha de desactivarse <c>true</c> si no <c>false</c>.</param>
        private void CambiarActivo(Control c, bool activo)
        {            
                c.Enabled = !activo;
        }

        /// <summary>
        /// Cambia la visibilidad de un control recibido como parametro
        /// al valor recibido tambien como parametro
        /// </summary>
        /// <param name="c">Control a modificar.</param>
        /// <param name="visibilidad">Si es <c>true</c> hace visible al control , si no lo oculta.</param>
        private void CambiarVisibilidad(Control c, bool visibilidad)
        {
            c.Visible = visibilidad;
        }

        /// <summary>
        /// Si la variable de clase es correcta , lee una respuesta del servidor y la manda
        /// al metodo de actualizarEstado
        /// </summary>
        private void HiloComunicacion()
        {
            while (true)
            {
                if (bRecogerRespuesta)
                {
                    actualizarEstado(conexion.recibirMensaje());
                }
            }
        }

        /// <summary>
        /// Gestiona el evento FormClosing del formulario.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="FormClosingEventArgs"/> que contiene los datos del evento.</param>
        private void SalaBlackJack_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuPrincipal.MenuPrincipal_FormClosing(sender,e);
        }
    }
}
