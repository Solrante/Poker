using System;
using System.Threading;
using System.Windows.Forms;

namespace Cliente_Poker
{
    public partial class SalaBlackJack : Form
    {
        MenuPrincipal MenuPrincipal;
        ConexionServidor conexion;
        public SalaBlackJack(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            MenuPrincipal = menuPrincipal;
            conexion = MenuPrincipal.conexion;
        }

        private void btnSalirSala_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Volver");
            MenuPrincipal.recibirUsuario();
            MenuPrincipal.Show();
            Dispose();
        }

        private void btnFicha25_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 25");
            actualizarEstado(conexion.recibirMensaje());
            cambiarVisibilidadFichas(false);
        }

        private void btnFicha50_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 50");
            actualizarEstado(conexion.recibirMensaje());
            cambiarVisibilidadFichas(false);
        }

        private void btnFicha100_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 100");
            cambiarVisibilidadFichas(false);
            actualizarEstado(conexion.recibirMensaje());
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

        private void actualizarEstado(string respuesta)
        {
            MessageBox.Show("Server respondio : " + respuesta);
        }
    }
}
