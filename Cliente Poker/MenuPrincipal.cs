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
    public partial class MenuPrincipal : Form
    {
        Login login;
        ConexionServidor conexion;
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
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
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            conexion.cerrarConexion();
        }
    }
}
