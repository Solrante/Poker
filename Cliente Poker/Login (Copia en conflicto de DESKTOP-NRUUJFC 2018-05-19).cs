using System;
using System.Diagnostics;
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
            if (!camposVacios())
            {
                //Se abre y se mandan dichos datos a través de la conexión al servidor
                conexion.abrirConexion();
                conexion.enviarMensaje(Clave.Login + Clave.Separador + txtCorreo.Text + Clave.SeparadorCredenciales + txtContraseña.Text);
                //Se comprueba la respuesta del servidor a las credenciales mandadas , avisando de información erronea en la misma
                //o accediendo al sistema de ser correcta.
                string respuesta = conexion.recibirMensaje();
                if (respuesta == Clave.LoginInvalido)
                {
                    mostrarError("Login invalido");
                }
                else if (respuesta == Clave.LoginValido)
                {
                    loginValido = true;
                    DialogResult = DialogResult.OK;
                }                
            }
            else
            {
                mostrarError("Has de llenar ambos campos");
            }
        }

        /// <summary>
        /// Activa la etiqueta descriptiva de errores , con un mensaje recibido
        /// </summary>
        /// <param name="error">Mensaje de error a mostrar</param>
        private void mostrarError(string error)
        {
            lblError.Text = error;
            centrarControl(lblError);
            lblError.Visible = true;
        }

        /// <summary>
        /// Comprueba si los campos del usuario contiene datos       
        /// </summary>
        /// <returns>True si alguno esta vacio , de otra manera false</returns>
        private bool camposVacios()
        {
            bool resultado = false;
            if (txtCorreo.Text == "" || txtContraseña.Text == "")
            {
                return true;
            }
            return resultado;
        }

        /// <summary>
        /// Centra un componente de manera horizontal en el formulario.
        /// </summary>
        /// <param name="c">Control a centrar.</param>
        private void centrarControl(Control c)
        {
            c.Location = new Point(centroHorizontal - c.Width / 2, c.Location.Y);
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
            centrarControl(lblCorreo);
            centrarControl(lblContraseña);
            centrarControl(txtCorreo);
            centrarControl(txtContraseña);
            centrarControl(btnEntrar);
            centrarControl(lblError);
        }

        /// <summary>
        /// Gestiona el evento click del btnRegistrarse
        /// </summary>
        /// <param name="sender">Origen.</param>
        /// <param name="e">Datos del evento.</param>
        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro(conexion);
            Hide();
            registro.ShowDialog();
            Show();
        }
    }
}
