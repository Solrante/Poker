using System;
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

        private void btnFicha25_Click(object sender, EventArgs e)
        {
            MenuPrincipal.conexion.enviarMensaje("Ficha - 25");
            cambiarVisibilidadFichas(false);
        }

        private void btnFicha50_Click(object sender, EventArgs e)
        {
            MenuPrincipal.conexion.enviarMensaje("Ficha - 50");
            cambiarVisibilidadFichas(false);
        }

        private void btnFicha100_Click(object sender, EventArgs e)
        {
            MenuPrincipal.conexion.enviarMensaje("Ficha - 100");
            cambiarVisibilidadFichas(false);
        }

        private void cambiarVisibilidadFichas(bool estado)
        {
            btnFicha100.Visible = estado;
            btnFicha50.Visible = estado;
            btnFicha25.Visible = estado;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cambiarVisibilidadFichas(true);
        }
    }
}
