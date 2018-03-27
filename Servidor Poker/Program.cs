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
        /// <summary>
        /// Objeto de tipo BaseDatos el cual gestiona la conexión a la base da datos del servidor
        /// </summary>
        static private BaseDatos bd;
        static List<Sala> salas = new List<Sala>();
        static readonly private object l = new object();
        static int puerto = 31416;
        static int numeroSalas;
        static Random random;
        static void Main(string[] args)
        {
            random = new Random();
            //Creamos conexion a la base da datos
            bd = new BaseDatos();
            //Creamos los objetos tipo sala y lanzamos un hilo para cada una
            crearSalas();
            foreach (Sala sala in salas)
            {
                switch (sala.Tipo)
                {
                    case eSala.BLACKJACK:
                        new Thread(salaBlackJack).Start(sala);
                        break;
                    case eSala.POKER:
                        new Thread(salaPoker).Start(sala);
                        break;

                }

            }
            //Definimos los datos del servidor y lo ejecutamos
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
        static string generarCarta()
        {
            string carta = "";
            carta = random.Next(1, 4) + "-" + random.Next(1, 10);
            return carta;
        }

        static void crearSalas()
        {
            salas.Add(new Sala("0,2,10", eSala.BLACKJACK));
            numeroSalas = salas.Count();
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
                new Thread(hiloSalaEspera).Start(new Usuario(sCliente, bd.leerUsuarioCompleto(credenciales)));
            }
            else
            {
                sw.WriteLine("Login - Invalido");
                sw.Flush();
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
        }

        static void hiloSalaEspera(object usu)
        {
            Usuario usuario = usu as Usuario;
            Console.WriteLine("Mandando usuario : " + usuario.ToString());
            usuario.mandarMensaje(usuario.ToString());
            Console.WriteLine("Mandando numero de salas");
            usuario.mandarMensaje(numeroSalas + "");
            foreach (Sala sala in salas)
            {
                usuario.mandarMensaje(sala.ToString());
            }
            while (usuario.Mensaje != "Desconexion")
            {
                if (!usuario.Jugando)
                {
                    usuario.leerMensaje();
                    Console.WriteLine(usuario.Mensaje);
                    if (usuario.Mensaje == "Desconexion")
                    {
                        lock (l)
                        {
                            Console.WriteLine(usuario.Correo + " : Se desconecta");
                            usuario.cerraSesion();
                        }
                    }
                    else
                    {
                        switch (usuario.Mensaje.Split('-')[0].Trim())
                        {
                            case "Sala":
                                Console.WriteLine("METO A USUARIO EN SALA");
                                usuario.Jugando = true;
                                int numSala = Convert.ToInt32(usuario.Mensaje.Split('-')[1]);
                                salas[numSala].Usuarios.Add(usuario);
                                break;
                        }
                    }
                }
            }
        }

        static void salaPoker(object s)
        {
            Sala sala = s as Sala;
            Console.WriteLine("Sala Poker arrancada : " + sala.NumSala + "\n\rNumero de usuarios en sala : " + sala.Usuarios.Count);
            while (true)
            {
                Thread.Sleep(5000);
                Console.WriteLine("Sala Poker: " + sala.NumSala + "\n\rNumero de usuarios en sala : " + sala.Usuarios.Count);
            }
        }

        static void salaBlackJack(object s)
        {

            Sala sala = s as Sala;
            Usuario usuario = null;
            List<string> cartasUsuario = new List<string>();
            List<string> cartasCrupier = new List<string>();
            double saldoDisponible = 0;
            bool finMano = false;
            while (true)
            {
                if (sala.Usuarios.Count != 0)
                {
                    if (finMano)
                    {
                        cartasUsuario.Clear();
                        cartasCrupier.Clear();
                        finMano = false;
                    }
                    //Se comprueba si el usuario para jugar es nulo , de ser así se guarda el usuario de la lista
                    if (usuario == null)
                    {
                        usuario = sala.Usuarios[0];
                        saldoDisponible = usuario.Saldo;
                        Console.WriteLine("Saldo actual : " + saldoDisponible);
                    }
                    usuario.leerMensaje();
                    if (usuario.Mensaje != "Volver")
                    {
                        //Si la respuesta de cliente no es la de volver , se comprueba la cabecera del mensaje para ver que acción se ha realizado
                        switch (usuario.Mensaje.Split('-')[0].Trim())
                        {
                            case "Ficha":
                                saldoDisponible -= Convert.ToDouble(usuario.Mensaje.Split('-')[1].Trim());
                                Console.WriteLine("Saldo actual : " + saldoDisponible);
                                cartasUsuario.Add(generarCarta());
                                cartasUsuario.Add(generarCarta());
                                cartasCrupier.Add(generarCarta());
                                foreach (string carta in cartasUsuario)
                                {
                                    Console.WriteLine("Carta-Jugador-" + carta);
                                    usuario.mandarMensaje("Carta-Jugador-" + carta);
                                }
                                foreach (string carta in cartasCrupier)
                                {
                                    Console.WriteLine("Carta-Crupier-" + carta);
                                    usuario.mandarMensaje("Carta-Crupier-" + carta);
                                }
                                usuario.mandarMensaje("Fin envio");
                                break;
                            case "Plantarse":
                                break;
                            case "Pedir":
                                string nCarta = generarCarta();
                                cartasUsuario.Add(nCarta);
                                Console.WriteLine("Carta-Jugador-" + nCarta);
                                usuario.mandarMensaje("Carta-Jugador-" + nCarta);
                                usuario.mandarMensaje("Fin envio");
                                break;

                        }
                    }
                    else
                    {
                        //Si se elige volver al menu principal
                        usuario.Saldo = saldoDisponible;
                        usuario.Jugando = false;
                        //Insertamos nuevos valores en la bae da datos
                        bd.actualizarDatos(usuario.ToString());
                        //Mandamos al cliente la nueva información del usuario
                        usuario.mandarMensaje(usuario.ToString());
                        //Eliminamos al usuario de la sala
                        sala.Usuarios.Clear();
                        Console.WriteLine("Saldo actual : " + usuario.Saldo);
                        usuario = null;
                    }

                }
            }
        }
    }
}
