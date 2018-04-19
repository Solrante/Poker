using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cliente_Poker
{
    /// <summary>
    /// Formulario para el acceso al servicio online
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Login : Form
    {
        /// <summary>
        /// Contiene el resultado de comprobar las credenciales introducidas
        /// </summary>
        public bool loginValido = false;

        /// <summary>
        /// Variable númerica que contendra el valor en pixel del centro del formulario
        /// </summary>
        private int centroHorizontal;

        /// <summary>
        /// Instancia de la clase gestora con la conexion al servidor
        /// </summary>
        private ConexionServidor conexion;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Login"/> .
        /// </summary>
        /// <param name="conexion">Instancia del gestor de conexion.</param>
        public Login(ConexionServidor conexion)
        {
            InitializeComponent();
            this.conexion = conexion;
        }
        /// <summary>
        /// Gestiona el evento Click del control btnEntrar.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //Se abre y se mandan dichos datos a través de la conexión al servidor
            conexion.abrirConexion();
            conexion.enviarMensaje(txtCorreo.Text + "," + txtContraseña.Text);
            //Se comprueba la respuesta del servidor a las credenciales mandadas , avisando de información erronea en la misma
            //o accediendo al sistema de ser correcta.
            if (conexion.recibirMensaje() == ClaveComunicacion.Login + ClaveComunicacion.Separador + ClaveComunicacion.Invalido)
            {
                lblError.Visible = true;
            }
            else
            {
                loginValido = true;
                DialogResult = DialogResult.OK;
            }

        }
        /// <summary>
        /// Gestiona el evento Load del formulario.
        /// </summary>
        /// <param name="sender">El origen del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> contenedora de la información del evento.</param>
        private void Login_Load(object sender, EventArgs e)
        {
            //Centro todos los elementos al medio de manera horizontal del formulario según su tamaño.
            centroHorizontal = ClientSize.Width / 2;
            lblCorreo.Location = new Point(centroHorizontal - lblCorreo.Width / 2, lblCorreo.Location.Y);
            lblContraseña.Location = new Point(centroHorizontal - lblContraseña.Width / 2, lblContraseña.Location.Y);
            txtCorreo.Location = new Point(centroHorizontal - txtCorreo.Width / 2, txtCorreo.Location.Y);
            txtContraseña.Location = new Point(centroHorizontal - txtContraseña.Width / 2, txtContraseña.Location.Y);
            btnEntrar.Location = new Point(centroHorizontal - btnEntrar.Width / 2, btnEntrar.Location.Y);
            lblError.Location = new Point(centroHorizontal - lblError.Width / 2, lblError.Location.Y);
        }
    }
}
