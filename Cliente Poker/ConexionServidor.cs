using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Cliente_Poker
{
    /// <summary>
    /// Conexion servidor.
    /// </summary>
    public class ConexionServidor
    {
        /// <summary>
        /// Indica si la inicialización de la conexion con el servidor se ha completado
        /// </summary>
        private bool conexionInicializada = false;

        /// <summary>
        /// IP de conexión al servidor.
        /// </summary>
        private string IP_SERVIDOR = "127.0.0.1";

        /// <summary>
        /// Puerto de conexion al servidor
        /// </summary>
        private int puerto = 31416;

        /// <summary>
        /// EndPoint del servidor
        /// </summary>
        private IPEndPoint servidor = null;

        /// <summary>
        /// Socket del servidor
        /// </summary>
        private Socket sServidor = null;

        /// <summary>
        /// Flujo de conexión con el servidor
        /// </summary>
        private NetworkStream ns = null;

        /// <summary>
        /// Flujo de lectura del servidor
        /// </summary>
        private StreamReader sr = null;

        /// <summary>
        /// Flujo de escritura hacia el servidor
        /// </summary>
        private StreamWriter sw = null;

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="T:Cliente_Poker.ConexionServidor"/>.
        /// </summary>
        public ConexionServidor() { }

        /// <summary>
        /// Manda un mensaje recibido como parametro al servidor.
        /// </summary>
        /// <param name="mensaje">Mensaje.</param>
        public void enviarMensaje(string mensaje)
        {
            if (conexionInicializada)
            {
                sw.WriteLine(mensaje);
                Console.WriteLine("<--" + mensaje);
                sw.Flush();
            }
        }

        /// <summary>
        /// Lee un mensaje del servidor
        /// </summary>
        /// <returns>The mensaje.</returns>
        public string recibirMensaje()
        {
            string mensaje = ""; 
            if (conexionInicializada)
            {
                mensaje = sr.ReadLine();
            }
            Console.WriteLine("-->" + mensaje);
            return mensaje;
        }

        /// <summary>
        /// Inicializa las variables orientadas a la conexión y comunicación con el servidor
        /// </summary>
        public void abrirConexion()
        {
            try
            {
                servidor = new IPEndPoint(IPAddress.Parse(IP_SERVIDOR), puerto);
                sServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sServidor.Connect(servidor);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Error de conexión con servidor: {0}\nCódigo de error: {1}({2})",
                                  ex.Message, (SocketError)ex.ErrorCode, ex.ErrorCode);
            }
            catch (OutOfMemoryException) { }
            catch (ArgumentOutOfRangeException) { }
            if (sServidor != null)
            {
                ns = new NetworkStream(sServidor);
            }
            if (ns != null)
            {
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);
                conexionInicializada = true;
            }
        }

        /// <summary>
        /// Cierra todos los flujos y demas variables destinadas a la conexión
        /// </summary>
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
