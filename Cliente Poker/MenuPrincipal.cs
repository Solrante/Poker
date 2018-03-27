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
        SalaBlackJack blackJack;
        public ConexionServidor conexion;
        List<Sala> salas = new List<Sala>();
        int oriY = 30;
        int centroHorizontal;
        string platillaInformacion = "{0} , Saldo : {1}";
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            lblUsuario.Text = platillaInformacion;
            centroHorizontal = ClientSize.Width / 2;
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
            recibirUsuario();
            recibirSalas();
        }
        public void recibirUsuario()
        {
            string[] usuario = conexion.recibirMensaje().Split(',');
            lblUsuario.Text = String.Format(platillaInformacion, usuario[0], usuario[1]);
        }

        private void recibirSalas()
        {
            int numSalas = Convert.ToInt32(conexion.recibirMensaje());
            Console.WriteLine("Numero de salas recibidas : " + numSalas);
            for (int i = 0; i < numSalas; i++)
            {
                salas.Add(new Sala(conexion.recibirMensaje()));
            }

            generarBotonesSalas();
        }

        private void generarBotonesSalas()
        {
            Button btn;
            Label lbl;
            foreach (Sala sala in salas)
            {
                btn = new Button();
                btn.Size = new Size(100, 40);
                lbl = new Label();
                btn.Click += new EventHandler(btnSala_Click);
                switch (sala.Tipo)
                {
                    case eSala.BLACKJACK:
                        btn.Text = "BlackJack - " + (sala.NumSala + 1);
                        break;
                    case eSala.POKER:
                        btn.Text = "Sala - " + (sala.NumSala + 1);
                        break;
                }
                btn.Tag = sala.NumSala + "";
                btn.Location = new Point(centroHorizontal - btn.Width, oriY);
                lbl.Location = new Point(btn.Location.X + 20 + btn.Width, btn.Location.Y + btn.Size.Height / 2 - lbl.Size.Height / 2);
                lbl.Text = sala.ApuestaMinima + "/" + sala.CuotaEntrada;
                Controls.Add(btn);
                Controls.Add(lbl);
                oriY += 50;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            conexion.enviarMensaje("Desconexion");
            Environment.Exit(0);
        }

        private void btnSala_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            conexion.enviarMensaje("Sala - " + (string)btn.Tag);
            Hide();
            blackJack = new SalaBlackJack(this);
            blackJack.Show();

        }
    }
}
