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
    public partial class Login : Form
    {
        public bool loginValido = false;
        private int centroHorizontal;
        private ConexionServidor conexion;

        public Login(ConexionServidor conexion)
        {
            InitializeComponent();
            this.conexion = conexion;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string credenciales;
            credenciales = txtCorreo.Text + "," + txtContraseña.Text;
            Console.WriteLine("Mando a la conexion las credenciales");
            conexion.abrirConexion();
            conexion.enviarMensaje(credenciales);
            string respuesta = conexion.recibirMensaje();
            if (respuesta == "Login - Invalido")
            {
                lblError.Visible = true;
            }
            else
            {
                loginValido = true;
                DialogResult = DialogResult.OK;
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
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
