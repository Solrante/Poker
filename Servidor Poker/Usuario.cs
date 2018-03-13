using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    class Usuario
    {
        private Socket socket;
        private NetworkStream ns;
        private StreamWriter sw;
        private StreamReader sr;

        public Usuario(Socket socket)
        {
            this.socket = socket;
            generarFlujos();
        }

        private void generarFlujos()
        {
            ns = new NetworkStream(socket);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
        }
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
        /// <summary>
        /// Envia un mensaje al usuario a través de su flujo de datos
        /// </summary>
        /// <param name="mensaje">Mensaje a enviar</param>
        public void mandarMensaje(string mensaje)
        {
            sw.WriteLine(mensaje);
            sw.Flush();
        }

        public string leerMensaje()
        {
            string mensaje = "Desconexion";

            try
            {
                mensaje = sr.ReadLine();
            }
            catch (IOException)
            {
                mensaje = "Desconexion";
            }
            if (mensaje == null)
            {
                mensaje = "Desconexion";
            }
            return mensaje;
        }

        public override string ToString()
        {
            return contraseña + "," + saldo;
        }

        public bool cerraSesion()
        {
            bool result = false;
            if (sr != null)
            {
                sr.Close();
            }
            if (sw != null)
            {
                sw.Close();
            }
            if (ns != null)
            {
                ns.Close();
            }
            socket.Close();
            return result;
        }
    }
}
