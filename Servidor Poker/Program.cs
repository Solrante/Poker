using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    class Program
    {
        static private BaseDatos bd;
        static private List<Usuario> usuariosOnline = new List<Usuario>();
        static List<StreamWriter> salidasUsuarios = new List<StreamWriter>();
        static readonly private object l = new object();
        static int puerto = 31416;
        static void Main(string[] args)
        {
            bd = new BaseDatos();
            bool enEjecucion = true;
            bool puertoInvalido = true;
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, puerto);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            while (puertoInvalido)
            {
                try
                {
                    s.Bind(ie);
                    puertoInvalido = false;
                }
                catch (SocketException)
                {
                    Console.WriteLine("Error asignando puerto {0} , probando el siguiente ", puerto);
                    puerto++;
                    ie = new IPEndPoint(IPAddress.Any, puerto);
                }
            }
            s.Listen(10);
            Socket sCliente = null;
            Console.WriteLine("En espera");
            while (enEjecucion)
            {
                sCliente = s.Accept();
                Thread hilo = new Thread(hiloLogin);
                hilo.Start(sCliente);

            }
            s.Close();
        }

        static void hiloLogin(object cliente)
        {
            Console.WriteLine("Entra usuario en login");
            string credenciales = "";
            Socket sCliente = (Socket)cliente;
            IPEndPoint endPoint = (IPEndPoint)sCliente.RemoteEndPoint;
            NetworkStream ns = new NetworkStream(sCliente);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            while (true)
            {

                try
                {
                    Console.WriteLine("Intento leer credenciales");
                    credenciales = sr.ReadLine();
                }
                catch (IOException)
                {
                    credenciales = "";
                }
                if (credenciales == null)
                {
                    credenciales = "";
                }

                Console.WriteLine("Credenciales recibidas : " + credenciales);
                if (bd.usuarioRegistrado(credenciales))
                {
                    sw.WriteLine("Login - Valido");
                    sw.Flush();
                    new Thread(hiloSalaEspera).Start(sCliente);
                    break;
                }
                else
                {
                    sw.WriteLine("Login - Invalido");
                    sw.Flush();
                } 
            }
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
            //sCliente.Close();

        }

        static void hiloSalaEspera(object cliente)
        {

            Socket sCliente = (Socket)cliente;
            IPEndPoint endPoint = (IPEndPoint)sCliente.RemoteEndPoint;
            Console.WriteLine("Cliente en sala de espera : " + endPoint.Address);
            NetworkStream ns = new NetworkStream(sCliente);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            while (true)
            {

            }
        }
    }
}
