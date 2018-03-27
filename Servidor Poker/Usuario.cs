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

        public Usuario(Socket socket, string datos)
        {
            this.socket = socket;

            guardarDatos(datos);
            generarFlujos();
        }

        private void guardarDatos(string datos)
        {
            ID = Convert.ToInt32(datos.Split(',')[0]);
            Correo = datos.Split(',')[1];
            Contraseña = datos.Split(',')[2];
            Saldo = Convert.ToDouble(datos.Split(',')[3]);

        }
        private void generarFlujos()
        {
            ns = new NetworkStream(socket);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
        }

        public int ID { get; set; }

        public string Correo { set; get; }

        public string Contraseña { set; get; }

        public double Saldo { set; get; }

        public string Mensaje { set; get; }

        public bool Jugando { get; set; }

        /// <summary>
        /// Envia un mensaje al usuario a través de su flujo de datos
        /// </summary>
        /// <param name="mensaje">Mensaje a enviar</param>
        public void mandarMensaje(string mensaje)
        {
            sw.WriteLine(mensaje);
            sw.Flush();
            //TODO salta excepcion al salir un cliente
        }

        public void leerMensaje()
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
            catch (ObjectDisposedException)
            {
                mensaje = "Desconexion";
            }
            if (mensaje == null)
            {
                mensaje = "Desconexion";
            }
            Mensaje = mensaje;
        }

        public override string ToString()
        {
            return Correo + "," + Saldo;
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
