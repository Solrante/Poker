using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    class Usuario
    {
        private string correo;
        public string Correo
        {
            set
            {
                correo = value;
            }
            get
            {
                return correo;
            }
        }

        private string contraseña;
        public string Contraseña
        {
            set
            {
                contraseña = value;
            }
            get
            {
                return contraseña;
            }
        }

        private float saldo;
        public float Saldo
        {
            set
            {
                saldo = value;
            }
            get
            {
                return saldo;
            }
        }
    }
}
