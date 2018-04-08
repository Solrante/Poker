using System;
using System.Threading;
using System.Windows.Forms;

namespace Cliente_Poker
{
    public partial class SalaBlackJack : Form
    {
        MenuPrincipal MenuPrincipal;
        ConexionServidor conexion;
        private delegate void CambiarText(PictureBox p, string s);
        private delegate void AñadirControl(Control c);
        private CambiarText c;
        string respuesta;
        bool bRecogerRespuesta;
        int x;


        public SalaBlackJack(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            MenuPrincipal = menuPrincipal; c = new CambiarText(cambiarImagenCarta);
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
            bRecogerRespuesta = true;
        }

        private void btnFicha50_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 50");
            bRecogerRespuesta = true;
        }

        private void btnFicha100_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Ficha - 100");
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
            conexion.enviarMensaje("Pedir");
            bRecogerRespuesta = true;
        }

        private void actualizarEstado(string respuesta)
        {
            switch (respuesta.Split('-')[0])
            {
                case "Carta":
                    switch (respuesta.Split('-')[1])
                    {
                        case "Jugador":
                            if (cartaJugador1.Image == null)
                            {
                                CambiarText c = new CambiarText(cambiarImagenCarta);
                                x = cartaJugador1.Location.X;
                                Invoke(c, cartaJugador1, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                            }
                            else
                            {
                                PictureBox pb = new PictureBox();
                                x += 100;
                                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                                pb.Size = cartaJugador1.Size;
                                Invoke(c, pb, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                                pb.Location = new System.Drawing.Point(x, cartaJugador1.Location.Y);
                                AñadirControl añadir = new AñadirControl(añadirControlNuevo);
                                Invoke(añadir, pb);
                            }
                            break;
                        case "Crupier":
                            if (cartaCrupier1.Image == null)
                            {
                                CambiarText c = new CambiarText(cambiarImagenCarta);
                                Invoke(c, cartaCrupier1, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                            }
                            break;
                    }
                    break;              
                case "Fin envio":
                    bRecogerRespuesta = false;
                    break;
            }            
        }


        private void añadirControlNuevo(Control c)
        {
            Controls.Add(c);
        }

        private void cambiarImagenCarta(PictureBox lbl, string text)
        {
            string palo = text.Split('-')[0];
            string numCarta = text.Split('-')[1];
            switch (palo)
            {
                case "TREBOLES":
                    switch (numCarta)
                    {
                        case "AS":
                            lbl.Image = Properties.Resources.cardClubsA;
                            break;
                        case "DOS":
                            lbl.Image = Properties.Resources.cardClubs2;
                            break;
                        case "TRES":
                            lbl.Image = Properties.Resources.cardClubs3;
                            break;
                        case "CUATRO":
                            lbl.Image = Properties.Resources.cardClubs4;
                            break;
                        case "CINCO":
                            lbl.Image = Properties.Resources.cardClubs5;
                            break;
                        case "SEIS":
                            lbl.Image = Properties.Resources.cardClubs6;
                            break;
                        case "SIETE":
                            lbl.Image = Properties.Resources.cardClubs7;
                            break;
                        case "OCHO":
                            lbl.Image = Properties.Resources.cardClubs8;
                            break;
                        case "NUEVE":
                            lbl.Image = Properties.Resources.cardClubs9;
                            break;
                        case "DIEZ":
                            lbl.Image = Properties.Resources.cardClubs10;
                            break;
                        case "J":
                            lbl.Image = Properties.Resources.cardClubsJ;
                            break;
                        case "Q":
                            lbl.Image = Properties.Resources.cardClubsQ;
                            break;
                        case "K":
                            lbl.Image = Properties.Resources.cardClubsK;
                            break;
                    }
                    break;
                case "DIAMANTES":
                    switch (numCarta)
                    {
                        case "AS":
                            lbl.Image = Properties.Resources.cardDiamondsA;
                            break;
                        case "DOS":
                            lbl.Image = Properties.Resources.cardDiamonds2;
                            break;
                        case "TRES":
                            lbl.Image = Properties.Resources.cardDiamonds3;
                            break;
                        case "CUATRO":
                            lbl.Image = Properties.Resources.cardDiamonds4;
                            break;
                        case "CINCO":
                            lbl.Image = Properties.Resources.cardDiamonds5;
                            break;
                        case "SEIS":
                            lbl.Image = Properties.Resources.cardDiamonds6;
                            break;
                        case "SIETE":
                            lbl.Image = Properties.Resources.cardDiamonds7;
                            break;
                        case "OCHO":
                            lbl.Image = Properties.Resources.cardDiamonds8;
                            break;
                        case "NUEVE":
                            lbl.Image = Properties.Resources.cardDiamonds9;
                            break;
                        case "DIEZ":
                            lbl.Image = Properties.Resources.cardDiamonds10;
                            break;
                        case "J":
                            lbl.Image = Properties.Resources.cardDiamondsJ;
                            break;
                        case "Q":
                            lbl.Image = Properties.Resources.cardDiamondsQ;
                            break;
                        case "K":
                            lbl.Image = Properties.Resources.cardDiamondsK;
                            break;
                    }
                    break;
                case "CORAZONES":
                    switch (numCarta)
                    {
                        case "AS":
                            lbl.Image = Properties.Resources.cardHeartsA;
                            break;
                        case "DOS":
                            lbl.Image = Properties.Resources.cardHearts2;
                            break;
                        case "TRES":
                            lbl.Image = Properties.Resources.cardHearts3;
                            break;
                        case "CUATRO":
                            lbl.Image = Properties.Resources.cardHearts4;
                            break;
                        case "CINCO":
                            lbl.Image = Properties.Resources.cardHearts5;
                            break;
                        case "SEIS":
                            lbl.Image = Properties.Resources.cardHearts6;
                            break;
                        case "SIETE":
                            lbl.Image = Properties.Resources.cardHearts7;
                            break;
                        case "OCHO":
                            lbl.Image = Properties.Resources.cardHearts8;
                            break;
                        case "NUEVE":
                            lbl.Image = Properties.Resources.cardHearts9;
                            break;
                        case "DIEZ":
                            lbl.Image = Properties.Resources.cardHearts10;
                            break;
                        case "J":
                            lbl.Image = Properties.Resources.cardHeartsJ;
                            break;
                        case "Q":
                            lbl.Image = Properties.Resources.cardHeartsQ;
                            break;
                        case "K":
                            lbl.Image = Properties.Resources.cardHeartsK;
                            break;
                    }
                    break;
                case "PICAS":
                    switch (numCarta)
                    {
                        case "AS":
                            lbl.Image = Properties.Resources.cardSpadesA;
                            break;
                        case "DOS":
                            lbl.Image = Properties.Resources.cardSpades2;
                            break;
                        case "TRES":
                            lbl.Image = Properties.Resources.cardSpades3;
                            break;
                        case "CUATRO":
                            lbl.Image = Properties.Resources.cardSpades4;
                            break;
                        case "CINCO":
                            lbl.Image = Properties.Resources.cardSpades5;
                            break;
                        case "SEIS":
                            lbl.Image = Properties.Resources.cardSpades6;
                            break;
                        case "SIETE":
                            lbl.Image = Properties.Resources.cardSpades7;
                            break;
                        case "OCHO":
                            lbl.Image = Properties.Resources.cardSpades8;
                            break;
                        case "NUEVE":
                            lbl.Image = Properties.Resources.cardSpades9;
                            break;
                        case "DIEZ":
                            lbl.Image = Properties.Resources.cardSpades10;
                            break;
                        case "J":
                            lbl.Image = Properties.Resources.cardSpadesJ;
                            break;
                        case "Q":
                            lbl.Image = Properties.Resources.cardSpadesQ;
                            break;
                        case "K":
                            lbl.Image = Properties.Resources.cardSpadesK;
                            break;
                    }
                    break;
            }
        }

        private void HiloComunicacion()
        {
            while (true)
            {
                if (bRecogerRespuesta)
                {                    
                    actualizarEstado(conexion.recibirMensaje());
                }
            }
        }
    }
}
