using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cliente_Poker
{
    /// <summary>
    /// Formulario de registro
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Registro : Form
    {
        /// <summary>
        /// Conexion al servidor
        /// </summary>
        ConexionServidor conexion;

        /// <summary>
        /// Posicion en pixel del centro del area del formulario
        /// </summary>
        int centroHorizontal;

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="Registro"/> .
        /// </summary>
        /// <param name="con">Conexion al servidor.</param>
        public Registro(ConexionServidor con)
        {
            InitializeComponent();
            conexion = con;
        }

        /// <summary>
        /// Gestiona el evento Click de un control recibido como parametro
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Click_Listener(object sender, EventArgs e)
        {
            if (camposVacios())
            {
                mostrarError("Todos los campos tienen que estar cubiertos");
            }
            else if (!comprobarContraseñas(tbContraseñaUno.Text, tbContraseñaDos.Text))
            {
                mostrarError("Las contraseñas han de coincidir");
            }
            else
            {
                lblError.Visible = false;
                conexion.abrirConexion();
                conexion.enviarMensaje(Clave.Registro + Clave.Separador + tbCorreo.Text + Clave.SeparadorCredenciales + tbContraseñaDos.Text);
                if (conexion.recibirMensaje() == Clave.RegistroInvalido)
                {
                    mostrarError("Usuario ya registrado");
                }
                else
                {
                    MessageBox.Show("Registro completado");
                }
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
        /// Comprueba si los campos de registro estan vacios
        /// </summary>
        /// <returns>Devuelve true si alguno esta vacio , de otra forma false</returns>
        private bool camposVacios()
        {
            bool resultado = false;
            if (tbContraseñaUno.Text == "" || tbContraseñaDos.Text == "" || tbCorreo.Text == "")
            {
                resultado = true; ;
            }
            return resultado;
        }

        /// <summary>
        /// Comprueba si dos cadenas son iguales , devolviendo tru si es asi , de otra manera false.
        /// </summary>
        /// <param name="pass1">Cadena uno.</param>
        /// <param name="pass2">Cadena dos.</param>
        /// <returns></returns>
        private bool comprobarContraseñas(string pass1, string pass2)
        {
            bool resultado = false;
            if (pass1 == pass2)
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
        /// Gestiona el evento Load del formulario
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de  <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Registro_Load(object sender, EventArgs e)
        {
            centroHorizontal = ClientSize.Width / 2;
            centrarControl(lblCorreo);
            centrarControl(lblContraseña);
            centrarControl(lblError);
            centrarControl(tbCorreo);
            centrarControl(tbContraseñaUno);
            centrarControl(tbContraseñaDos);
            centrarControl(btnRegistro);
        }
    }
}
