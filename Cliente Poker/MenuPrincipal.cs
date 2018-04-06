using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Posicion Y inicial para la colocación de botones de salas
        /// </summary>
        int oriY = 30;

        /// <summary>
        /// Posicion en pixel del centro del area del formulario
        /// </summary>
        int centroHorizontal;

        /// <summary>
        /// Estructura para mostrar información de usuario
        /// </summary>
        string platillaInformacion = "{0} , Saldo : {1}";

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
            recibirUsuario();
            recibirSalas();
        }
        /// <summary>
        /// Lee datos sobre el usuario del servidor.
        /// </summary>
        public void recibirUsuario()
        {
            string[] usuario = conexion.recibirMensaje().Split(',');
            //Salta out of range exception al cerrar cliente
            lblUsuario.Text = String.Format(platillaInformacion, usuario[0], usuario[1]);
        }

        /// <summary>
        /// Genera instancias de salas según la información de salas recibida del servidor y ejecuta la generación de botones.
        /// </summary>
        private void recibirSalas()
        {
            int numSalas = Convert.ToInt32(conexion.recibirMensaje());
            Console.WriteLine("Numero de salas recibidas : " + numSalas);
            for (int i = 0; i < numSalas; i++)
            {
                salas.Add(new Sala(conexion.recibirMensaje()));
            }

            generarBotonesSalas();
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
                btn.Size = new Size(100, 40);
                lbl = new Label();
                btn.Click += new EventHandler(btnSala_Click);
                switch (sala.Tipo)
                {
                    case eSala.BLACKJACK:
                        btn.Text = "BlackJack - " + (sala.NumSala + 1);
                        break;
                    case eSala.POKER:
                        btn.Text = "Sala - " + (sala.NumSala + 1);
                        break;
                }
                btn.Tag = sala.NumSala + "";
                btn.Location = new Point(centroHorizontal - btn.Width, oriY);
                lbl.Location = new Point(btn.Location.X + 20 + btn.Width, btn.Location.Y + btn.Size.Height / 2 - lbl.Size.Height / 2);
                lbl.Text = sala.ApuestaMinima + "/" + sala.CuotaEntrada;
                Controls.Add(btn);
                Controls.Add(lbl);
                oriY += 50;
            }
        }

        /// <summary>
        /// Gestiona el evento Click del control btnSalir.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Desconexion");
            Environment.Exit(0);
        }

        /// <summary>
        /// Gestiona el evento Click del control multiples botones de las salas.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void btnSala_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            conexion.enviarMensaje("Sala - " + (string)btn.Tag);
            Hide();
            blackJack = new SalaBlackJack(this);
            blackJack.Show();

        }
    }
}
