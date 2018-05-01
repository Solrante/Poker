using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Servidor_Poker
{
    /// <summary>
    /// Clase principal del programa
    /// </summary>
    class Program
    {
        /// <summary>
        /// Objeto de tipo BaseDatos el cual gestiona la conexión a la base da datos del servidor
        /// </summary>
        static private BaseDatos bd;

        /// <summary>
        /// Listado de salas activas del servidor
        /// </summary>
        static private List<Sala> salas = new List<Sala>();

        /// <summary>
        /// Listado de usuario activos en el servidor
        /// </summary>
        static private List<string> usuariosConectados = new List<string>();

        /// <summary>
        /// Objeto para control de uso de elementos comunes
        /// </summary>
        static readonly private object l = new object();

        /// <summary>
        /// Puerto de conexion al servidor
        /// </summary>
        static private int puerto = 31416;

        /// <summary>
        /// Instancia de generador de numeros aleatorios
        /// </summary>
        static Random random;

        /// <summary>
        /// Gestiona la ejecucion del servidor.
        /// </summary>
        static bool enEjecucion = true;

        /// <summary>
        /// Bandera para la asignacion de un puerto valido
        /// </summary>
        static bool puertoInvalido = true;

        /// <summary>
        /// IE del servidor
        /// </summary>
        static IPEndPoint ie;

        /// <summary>
        /// Socket del servidor
        /// </summary>
        static Socket s;

        /// <summary>
        /// El punto de entrada del programa , donde el control del mismo empieza y acaba
        /// </summary>
        /// <param name="args">Los parametros de la linea de comandos.</param>
        static void Main(string[] args)
        {
            random = new Random();
            //Creamos conexion a la base da datos
            bd = new BaseDatos();
            //Creamos los objetos tipo sala y lanzamos un hilo para cada una
            crearSalas();
            lanzarSalas();
            inicializarEscuchaServer();
            Socket sCliente = null;
            Console.WriteLine("En espera");
            while (enEjecucion)
            {
                sCliente = s.Accept();
                new Thread(hiloLogin).Start(sCliente);
            }
            s.Close();
        }

        /// <summary>
        /// Genera las salas de juego y las añade al listado de salas activas
        /// </summary>
        static void crearSalas()
        {
            salas.Add(new Sala("0,0,0", eSala.BLACKJACK));
            salas.Add(new Sala("1,0,0", eSala.BLACKJACK));
            //salas.Add(new Sala("2,0,0", eSala.BLACKJACK));
        }

        /// <summary>
        /// Gestiona el login al servidor de un cliente recibido como parametro
        /// </summary>
        /// <param name="cliente">Cliente.</param>
        static void hiloLogin(object cliente)
        {
            string credenciales = "";
            Socket sCliente = (Socket)cliente;
            IPEndPoint endPoint = (IPEndPoint)sCliente.RemoteEndPoint;
            NetworkStream ns = new NetworkStream(sCliente);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            try
            {
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
            if (bd.usuarioRegistrado(credenciales) && !usuariosConectados.Contains(credenciales))
            {
                sw.WriteLine(Clave.LoginValido);
                sw.Flush();
                lock (l)
                {
                    usuariosConectados.Add(credenciales);
                    new Thread(hiloSalaEspera).Start(new Usuario(sCliente, bd.leerUsuarioCompleto(credenciales)));
                }

            }
            else
            {                
                sw.WriteLine(Clave.LoginInvalido);
                sw.Flush();
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
                if (sCliente != null)
                {
                    sCliente.Close();
                }
            }
        }

        /// <summary>
        /// Gestiona la interaccion del usuario recibido como parametro en el menu principal del juego
        /// </summary>
        /// <param name="usu">Usuario.</param>
        static void hiloSalaEspera(object usu)
        {
            Usuario usuario = usu as Usuario;
            usuario.mandarMensaje(usuario.ToString());
            usuario.mandarMensaje(salas.Count() + "");
            foreach (Sala sala in salas)
            {
                usuario.mandarMensaje(sala.ToString());
            }
            while (usuario.Mensaje != Clave.Desconexion)
            {
                if (!usuario.Jugando)
                {
                    usuario.leerMensaje();
                    if (usuario.Mensaje == Clave.Desconexion)
                    {
                        lock (l)
                        {
                            bd.actualizarDatos(usuario.ToString());
                            usuariosConectados.Remove(usuario.getCredenciales());
                            usuario.cerrarSesion();
                        }
                    }
                    else if (usuario.Mensaje.Split(Clave.Separador)[0] == Clave.Sala)
                    {
                        usuario.Jugando = true;
                        int numSala = Convert.ToInt32(usuario.Mensaje.Split(Clave.Separador)[1]);
                        lock (l)
                        {
                            salas[numSala].Usuarios.Add(usuario);
                            salas[numSala].Llena = true;
                        }
                    }
                    if (usuario.Mensaje == Clave.ListaSalas)
                    {
                        lock (l)
                        {
                            foreach (Sala sl in salas)
                            {
                                usuario.mandarMensaje(sl.ToString());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Inicializa la escucha del socket del servidor
        /// </summary>
        static void inicializarEscuchaServer()
        {
            ie = new IPEndPoint(IPAddress.Any, puerto);
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Mientras no tengamos un puerto disponible seguira buscando hasta encontrar uno.
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
        }

        /// <summary>
        /// Lanza los hilos ligados a cada sala de juego
        /// </summary>
        static void lanzarSalas()
        {
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
        }

        /// <summary>
        /// Gestiona la sala recibida como parametro
        /// </summary>
        /// <param name="s">Sala.</param>
        static void salaPoker(object s)
        {
            Sala sala = s as Sala;
        }

        /// <summary>
        /// Gestiona la sala recibida como parametro
        /// </summary>
        /// <param name="s">Sala.</param>
        static void salaBlackJack(object s)
        {
            Sala sala = s as Sala; // Sala
            GestorJuegoBlackJack gestor = null;
            Usuario usuario = null; //Usuario en juego
            while (enEjecucion)
            {
                if (sala.Usuarios.Count != 0)
                {
                    //Se comprueba si el usuario para jugar es nulo , de ser así se guarda el usuario de la lista
                    if (usuario == null)
                    {
                        usuario = sala.Usuarios[0];
                    }
                    if (gestor == null)
                    {
                        gestor = new GestorJuegoBlackJack(usuario);
                    }
                    usuario.leerMensaje();
                    if (usuario.Mensaje != Clave.Volver)
                    {
                        gestor.ActualizarEstado();
                    }
                    else
                    {
                        //Si se elige volver al menu principal
                        usuario.Saldo = gestor.getSaldo();
                        usuario.Jugando = false;
                        sala.Llena = false;
                        bd.actualizarDatos(usuario.ToString());
                        //Mandamos al cliente la nueva información del usuario
                        usuario.mandarMensaje(usuario.ToString());
                        //Eliminamos al usuario de la sala
                        sala.Usuarios.Clear();
                        usuario = null;
                        gestor = null;
                    }
                }
            }
        }
    }
}
