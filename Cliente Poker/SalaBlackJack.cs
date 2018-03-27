using System;
using System.Threading;
using System.Windows.Forms;

namespace Cliente_Poker
{
    public partial class SalaBlackJack : Form
    {
        MenuPrincipal MenuPrincipal;
        ConexionServidor conexion;
        string respuesta;
        bool bRecogerRespuesta;
        public SalaBlackJack(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            MenuPrincipal = menuPrincipal;
            conexion = MenuPrincipal.conexion;
            new Thread(HiloComunicacion).Start();
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
            //cambiarVisibilidadFichas(false);
            bRecogerRespuesta = true;
        }

        private void btnFicha50_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 50");
            //cambiarVisibilidadFichas(false);
            bRecogerRespuesta = true;
        }

        private void btnFicha100_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 100");
            //cambiarVisibilidadFichas(false);
            bRecogerRespuesta = true;
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
            switch (respuesta.Split('-')[0])
            {
                case "Carta":
                    switch (respuesta.Split('-')[1])
                    {
                        case "Jugador":
                            if (cartaJugador1.Text == "")
                            {
                                cartaJugador1.Text = respuesta.Split('-')[2] + respuesta.Split('-')[3];
                            }
                            else
                            {
                                Label lbl = new Label();
                                lbl.Text = respuesta.Split('-')[2] + respuesta.Split('-')[3];
                                lbl.Location = new System.Drawing.Point(cartaJugador1.Location.X + 100, cartaJugador1.Location.Y);
                                Controls.Add(lbl);
                            }
                            break;
                        case "Crupier":
                            if (cartaCrupier1.Text == "")
                            {
                                cartaCrupier1.Text = respuesta.Split('-')[3] + respuesta.Split('-')[4];
                            }
                            break;
                    }
                    break;
                case "Repartir cartas":
                    break;
                case "Fin envio":
                    bRecogerRespuesta = false;
                    break;
                default:
                    break;
            }

        }

        private void HiloComunicacion()
        {
            while (true)
            {
                if (bRecogerRespuesta)
                {
                    respuesta = conexion.recibirMensaje();
                    actualizarEstado(respuesta);
                }
            }
        }

    }
}
