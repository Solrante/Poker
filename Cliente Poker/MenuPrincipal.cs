﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cliente_Poker
{
    /// <summary>
    /// Formulario principal del cliente
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MenuPrincipal : Form
    {
        /// <summary>
        /// Instancia del formulario de login del cliente
        /// </summary>
        Login login;

        /// <summary>
        /// Instancia del formulario del juego blackJack
        /// </summary>
        SalaBlackJack blackJack;

        /// <summary>
        /// Instancia del gestor de conexion al servidor
        /// </summary>
        public ConexionServidor conexion;

        /// <summary>
        /// Listado de salas
        /// </summary>
        List<Sala> salas = new List<Sala>();

        /// <summary>
        /// Listado de botones de salas
        /// </summary>
        List<Button> botonesSalas = new List<Button>();

        /// <summary>
        /// Posicion Y inicial para la colocación de botones de salas
        /// </summary>
        int oriY = 50;

        /// <summary>
        /// Posicion en pixel del centro del area del formulario
        /// </summary>
        int centroHorizontal;

        /// <summary>
        /// Estructura para mostrar información de usuario
        /// </summary>
        string platillaInformacion = "Usuario : {0} , Saldo : {1}";

        /// <summary>
        /// Cantidad en pixels entre botones de salas en vertical
        /// </summary>
        private int separaciónVerticalBotones = 50;

        /// <summary>
        /// Cantidad en pixels entre el botón y su etiqueta descriptiva
        /// </summary>
        private int separacionBotonLabel = 20;

        /// <summary>
        /// Tamaño para botones
        /// </summary>
        private Size tamañoBoton = new Size(100, 40);

        /// <summary>
        /// Usuario del juego
        /// </summary>
        public Usuario usuario;

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="MenuPrincipal"/> .
        /// </summary>
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gestiona el evento Load del Form1.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            lblUsuario.Text = platillaInformacion;
            centroHorizontal = ClientSize.Width / 2;
            conexion = new ConexionServidor();
            login = new Login(conexion);
            DialogResult res;
            while (!login.loginValido)
            {
                res = login.ShowDialog();
                if (res == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                }
            }
            recibirUsuario(conexion.recibirMensaje());
            recibirSalas(conexion.recibirMensaje());
        }

        /// <summary>
        /// Carga la información del usuario de un mensaje recibido como parametro
        /// </summary>
        /// <param name="respuesta">Datos del usuario.</param>
        public void recibirUsuario(string respuesta)
        {
            usuario = new Usuario(respuesta);
            lblUsuario.Text = String.Format(platillaInformacion, usuario.Correo, usuario.Saldo);
        }

        /// <summary>
        /// Recibe y genera salas segun los datos recibidos como parametro
        /// </summary>
        /// <param name="respuesta">Información de salas.</param>
        private void recibirSalas(string respuesta)
        {
            int numSalas = Convert.ToInt32(respuesta);
            ClientSize = new Size(ClientSize.Width, 100 + 70 * numSalas);
            for (int i = 0; i < numSalas; i++)
            {
                salas.Add(new Sala(conexion.recibirMensaje()));
            }
            generarBotonesSalas();
            btnSalir.Location = new Point(btnSalir.Location.X, ClientSize.Height - btnSalir.Height - 10);
            lblUsuario.Location = new Point(lblUsuario.Location.X, ClientSize.Height - lblUsuario.Height - 10);
        }

        /// <summary>
        /// Genera botones según las salas contenidas por el listado de salas de la instancia".
        /// </summary>
        private void generarBotonesSalas()
        {
            Button btn;
            Label lbl;
            foreach (Sala sala in salas)
            {
                btn = new Button();
                btn.Size = tamañoBoton;
                btn.Click += new EventHandler(btnSala_Click);
                switch (sala.Tipo)
                {
                    case eSala.BLACKJACK:
                        btn.Text = "" + eSala.BLACKJACK + Clave.Separador + (sala.NumSala + 1);
                        break;
                    case eSala.POKER:
                        btn.Text = "" + eSala.POKER + Clave.Separador + (sala.NumSala + 1);
                        break;
                }
                btn.Tag = Clave.Sala + Clave.Separador + sala.NumSala + "";
                btn.Location = new Point(centroHorizontal - btn.Width / 2, oriY);
                //Desactivo el boton si la sala ya esta ocupada
                btn.Enabled = comprobarEntrada(sala);
                btn.BackColor = Color.Sienna;
                btn.ForeColor = Color.White;
                botonesSalas.Add(btn);
                Controls.Add(btn);
                if (sala.Tipo == eSala.POKER)
                {
                    lbl = new Label();
                    lbl.Location = new Point(btn.Location.X + separacionBotonLabel + btn.Width, btn.Location.Y + btn.Size.Height / 2 - lbl.Size.Height / 2);
                    lbl.Text = sala.ApuestaMinima + "/" + sala.CuotaEntrada;
                    Controls.Add(lbl);
                }
                oriY += separaciónVerticalBotones;
            }
        }

        /// <summary>
        /// Genera un resultado booleano segun si un usuario puede o no acceder a una sala
        /// </summary>
        /// <param name="sala">Sala de referencia</param>
        /// <returns>Devuelve true si el usuario cumple los requisitos , de otra manera false</returns>
        private bool comprobarEntrada(Sala sala)
        {
            bool res = !sala.Llena;
            if (res && sala.CuotaEntrada > usuario.Saldo)
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Gestiona el evento Click del control btnSalir.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            MenuPrincipal_FormClosing(null, null);
        }

        /// <summary>
        /// Gestiona el evento Click del control multiples botones de las salas.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void btnSala_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int indiceSala = Convert.ToInt32(((string)btn.Tag).Split(Clave.Separador)[1]);
            if (usuario.Saldo > salas[indiceSala].CuotaEntrada)
            {
                conexion.enviarMensaje((string)btn.Tag);
                if (conexion.recibirMensaje() != Clave.SalaLlena)
                {
                    Hide();
                    if (btn.Text.Split(Clave.Separador)[1].Trim() == eSala.POKER.ToString())
                    {
                        //Si se implementan salas de poker se abririán aqui.
                    }
                    else
                    {
                        blackJack = new SalaBlackJack(this);
                        blackJack.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Sala Llena , se actualizaran las salas");
                    actualizarSalas();
                }
            }
            else
            {
                MessageBox.Show("Saldo inferior a la cuotra de entrada de la sala");
            }

        }

        /// <summary>
        /// Actualiza el estado de las salas del cliente segun el estado del server
        /// </summary>
        public void actualizarSalas()
        {
            foreach (var sala in salas)
            {
                sala.actualizarDatos(conexion.recibirMensaje());
                //Si el nuevo estado de sala ha cambiado se ajustara el boton
                botonesSalas[salas.IndexOf(sala)].Enabled = comprobarEntrada(sala);
            }
        }

        /// <summary>
        /// Gestiona el evento SizeChanged del menu principal.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> contenedora de los datos del evento.</param>
        private void MenuPrincipal_SizeChanged(object sender, EventArgs e)
        {
            foreach (Button boton in botonesSalas)
            {
                boton.Location = new Point(ClientSize.Width / 2 - boton.Width / 2, boton.Location.Y);
            }
        }

        /// <summary>
        /// Gestiona el evento click del btnActualizarSalas
        /// </summary>
        /// <param name="sender">Origen.</param>
        /// <param name="e">Datos del evento.</param>
        private void btnActualizarSalas_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje(Clave.ListaSalas);
            actualizarSalas();
        }

        /// <summary>
        /// Gestiona el evengo FormClosing del formulario.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de  <see cref="FormClosingEventArgs"/> que contiene la información del evento.</param>
        public void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            conexion.enviarMensaje(Clave.Desconexion);
            Environment.Exit(0);

        }

        /// <summary>
        /// Gestiona el evento Tick del objeto timer del formulario
        /// </summary>
        /// <param name="sender">Timer del formulario</param>
        /// <param name="e">Instancia de EventArgs que contiene los datos sobre el evento</param>
        private void tmrActualizarSalas_Tick(object sender, EventArgs e)
        {
            btnActualizarSalas.PerformClick();
        }
    }
}
