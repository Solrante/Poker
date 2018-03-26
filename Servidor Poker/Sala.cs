using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    enum eSala
    {
        BLACKJACK, POKER
    }
    class Sala
    {
        public List<Usuario> Usuarios = new List<Usuario>();
        public Sala(string datos, eSala tipo)
        {
            string[] info = datos.Split(',');
            NumSala = Convert.ToInt32(info[0]);
            ApuestaMinima = Convert.ToInt32(info[1]);
            CuotaEntrada = Convert.ToInt32(info[2]);
            Tipo = tipo;

        }
        public eSala Tipo { get; set; }
        private int numSala;
        public int NumSala
        {
            set
            {
                numSala = value;
            }
            get
            {
                return numSala;
            }
        }

        private int apuestaMinima;
        public int ApuestaMinima
        {
            set
            {
                apuestaMinima = value;
            }
            get
            {
                return apuestaMinima;
            }
        }

        private int cuotaEntrada;
        public int CuotaEntrada
        {
            set
            {
                cuotaEntrada = value;
            }
            get
            {
                return cuotaEntrada;
            }
        }

        public override string ToString()
        {            
            return string.Format("{0},{1},{2},{3}", NumSala, ApuestaMinima, CuotaEntrada, (int)Tipo);
        }
    }
}
