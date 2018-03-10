using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente_Poker
{
    public class ConexionServidor
    {
        private string IP_SERVIDOR = "127.0.0.1";
        private int puerto = 31416;
        IPEndPoint servidor = null;
        Socket sServidor = null;
        NetworkStream ns = null;
        StreamReader sr = null;
        StreamWriter sw = null;

        public ConexionServidor()
        {
            try
            {
                servidor = new IPEndPoint(IPAddress.Parse(IP_SERVIDOR), puerto);
                sServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sServidor.Connect(servidor);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(
               "Error de conexión con servidor: {0}\nCódigo de error: {1}({2})", ex.Message, (SocketError)ex.ErrorCode, ex.ErrorCode);
            }
            catch (OutOfMemoryException)
            {
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            ns = new NetworkStream(sServidor);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
        }

        public void enviarMensaje(string mensaje)
        {
            sw.WriteLine(mensaje);
            sw.Flush();
        }
        public string recibirMensaje()
        {
            return sr.ReadLine();
        }

        public void cerrarConexion()
        {
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
            sServidor.Close();
        }
    }
}
