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
        private int centroVertical;
        public Login()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string credenciales;
            credenciales = txtCorreo.Text + "," + txtContraseña.Text;
            MessageBox.Show(credenciales);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            centroVertical = ClientSize.Width / 2;
            lblCorreo.Location = new Point(centroVertical - lblCorreo.Width / 2, lblCorreo.Location.Y);
            lblContraseña.Location = new Point(centroVertical - lblContraseña.Width / 2, lblContraseña.Location.Y);
            txtCorreo.Location = new Point(centroVertical - txtCorreo.Width / 2, txtCorreo.Location.Y);
            txtContraseña.Location = new Point(centroVertical - txtContraseña.Width / 2, txtContraseña.Location.Y);
            btnEntrar.Location = new Point(centroVertical - btnEntrar.Width / 2, btnEntrar.Location.Y);
        }
    }
}
