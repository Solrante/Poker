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
    public partial class SalaBlackJack : Form
    {
        MenuPrincipal MenuPrincipal;
        public SalaBlackJack(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            MenuPrincipal = menuPrincipal;
        }

        private void btnSalirSala_Click(object sender, EventArgs e)
        {
            MenuPrincipal.conexion.enviarMensaje("Volver");
            MenuPrincipal.Show();
            Dispose();            
        }
    }
}
