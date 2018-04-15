using System;
using System.Threading;
using System.Windows.Forms;

namespace Cliente_Poker
{
    public partial class SalaBlackJack : Form
    {
        MenuPrincipal MenuPrincipal;
        ConexionServidor conexion;
        private delegate void CambiarImagenPictureBox(PictureBox p, string s);
        private delegate void CambiarTextoLabel(Label l, string s);
        private delegate void CambiarVisibilidadControl(Control c, bool b);
        private delegate void ModificarControls(Control c,bool b);
        private CambiarImagenPictureBox imagenPictureBox;
        private CambiarTextoLabel textoLabel;
        private CambiarVisibilidadControl visibilidad;
        private ModificarControls controles;
        bool bRecogerRespuesta;
        int posXJugador;
        int posXCrupier;


        public SalaBlackJack(MenuPrincipal menuPrincipal)
        {
            InitializeComponent();
            MenuPrincipal = menuPrincipal;
            imagenPictureBox = new CambiarImagenPictureBox(cambiarImagenCarta);
            textoLabel = new CambiarTextoLabel(TextoLabel);
            visibilidad = new CambiarVisibilidadControl(CambiarVisibilidad);
            controles = new ModificarControls(ModificarListadoControls);
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

        private void ClickListener(object sender, EventArgs e)
        {
            string mensaje = "";
            bool ficha = false;
            Button btn = sender as Button;
            if (btn == btnFicha25)
            {
                mensaje = "Ficha - 25";
                ficha = true;
            }
            if (btn == btnFicha50)
            {
                mensaje = "Ficha - 50";
                ficha = true;
            }
            if (btn == btnFicha100)
            {
                mensaje = "Ficha - 100";
                ficha = true;
            }
            if (btn == btnPedir)
            {
                mensaje = "Pedir";
            }
            if (btn == btnPlantarse)
            {
                mensaje = "Plantarse";
            }
            if (ficha)
            {
                cambiarVisibilidadFichas(false);
            }
            conexion.enviarMensaje(mensaje);
            bRecogerRespuesta = true;
        }

        private void cambiarVisibilidadFichas(bool estado)
        {
            Invoke(visibilidad, btnFicha100, estado);
            Invoke(visibilidad, btnFicha50, estado);
            Invoke(visibilidad, btnFicha25, estado);
            Invoke(visibilidad, btnPedir, !estado);
            Invoke(visibilidad, btnPlantarse, !estado);
        }

        private void actualizarEstado(string respuesta)
        {
            Console.WriteLine(respuesta);
            switch (respuesta.Split('-')[0])
            {
                case "Carta":
                    switch (respuesta.Split('-')[1])
                    {
                        case "Jugador":
                            if (cartaJugador1.Image == null)
                            {
                                CambiarImagenPictureBox c = new CambiarImagenPictureBox(cambiarImagenCarta);
                                posXJugador = cartaJugador1.Location.X;
                                Invoke(c, cartaJugador1, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                            }
                            else
                            {
                                PictureBox pb = new PictureBox();
                                posXJugador += 60;
                                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                                pb.Size = cartaJugador1.Size;
                                Invoke(imagenPictureBox, pb, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                                pb.Location = new System.Drawing.Point(posXJugador, cartaJugador1.Location.Y);
                                Invoke(controles, pb, true);
                            }
                            break;
                        case "Crupier":
                            if (cartaCrupier1.Image == null)
                            {
                                posXCrupier = cartaCrupier1.Location.X;
                                CambiarImagenPictureBox c = new CambiarImagenPictureBox(cambiarImagenCarta);
                                Invoke(c, cartaCrupier1, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                            }
                            else
                            {
                                PictureBox pb = new PictureBox();
                                posXCrupier += 60;
                                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                                pb.Size = cartaCrupier1.Size;
                                Invoke(imagenPictureBox, pb, respuesta.Split('-')[2] + "-" + respuesta.Split('-')[3]);
                                pb.Location = new System.Drawing.Point(posXJugador, cartaCrupier1.Location.Y);                                
                                Invoke(controles, pb,true);
                            }
                            break;
                    }
                    break;
                case "Fin Mano":
                    Console.WriteLine("Me llega fin de mano");
                    switch (respuesta.Split('-')[1])
                    {
                        case "Gana Jugador":
                            Invoke(textoLabel, lblResultado, "Gana Jugador");
                            break;
                        case "Gana Crupier":
                            Invoke(textoLabel, lblResultado, "Gana Crupier");
                            break;
                        case "Empate":
                            Invoke(textoLabel, lblResultado, "Empate");
                            break;
                    }
                    reset();
                    break;
                case "Fin envio":
                    bRecogerRespuesta = false;
                    break;
            }
        }

        private void reset()
        {
            Thread.Sleep(2000);
            Invoke(textoLabel, lblResultado, "");
            cambiarVisibilidadFichas(true);
            for (int i = Controls.Count - 1; i > 0; i--)
            {                
                if (Controls[i].GetType() == typeof(PictureBox) && Controls[i] != cartaCrupier1 && Controls[i] != cartaJugador1)
                {
                    Console.WriteLine("Picturebox eliminado");
                    Invoke(controles, Controls[i], false);
                }
            }
            cartaCrupier1.Image = null;
            cartaJugador1.Image = null;
        }

        private void ModificarListadoControls(Control c,bool accion)
        {
            if (accion)
            {
                Controls.Add(c);
            }
            else
            {
                Controls.Remove(c);
            }
            
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

        private void TextoLabel(Label l, string s)
        {
            l.Text = s;
        }

        private void CambiarVisibilidad(Control c, bool visibilidad)
        {
            c.Visible = visibilidad;
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
