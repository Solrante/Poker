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
            if (bd.baseDeDatosActiva)
            {
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
            Console.WriteLine("No se pudo abrir la BD , se detiende la ejecución");
            Console.ReadKey();
        }

        /// <summary>
        /// Genera las salas de juego y las añade al listado de salas activas
        /// </summary>
        static void crearSalas()
        {
            salas.Add(new Sala("0,0,0", eSala.BLACKJACK));
            salas.Add(new Sala("1,0,0", eSala.BLACKJACK));
            salas.Add(new Sala("2,0,0", eSala.BLACKJACK));
        }

        /// <summary>
        /// Gestiona el login al servidor de un cliente recibido como parametro
        /// </summary>
        /// <param name="cliente">Cliente.</param>
        static void hiloLogin(object cliente)
        {
            string mensaje = "";
            Socket sCliente = (Socket)cliente;
            IPEndPoint endPoint = (IPEndPoint)sCliente.RemoteEndPoint;
            NetworkStream ns = new NetworkStream(sCliente);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            bool finConexion = true;

            mensaje = leerMensaje(sr);

            string accion = mensaje.Split(Clave.Separador)[0];
            Console.WriteLine("Accion : " + accion);
            string credenciales = mensaje.Split(Clave.Separador)[1];
            Console.WriteLine("Credenciales : " + credenciales);
            bool usuarioRegistrado = bd.usuarioRegistrado(credenciales);
            Console.WriteLine("Usuario registrado : " + usuarioRegistrado);
            bool usuarioLogeado = usuariosConectados.Contains(credenciales);
            Console.WriteLine("Usuario logeado : " + usuarioLogeado);

            if (accion == Clave.Registro && usuarioRegistrado)
            {
                mandarMensaje(sw, Clave.RegistroInvalido);
            }
            if (accion == Clave.Registro && !usuarioRegistrado)
            {
                bd.registrarUsuario(credenciales);
                mandarMensaje(sw, Clave.RegistroValido);
            }
            if (accion == Clave.Login && usuarioRegistrado && !usuarioLogeado)
            {
                finConexion = false;
                mandarMensaje(sw, Clave.LoginValido);
                lock (l)
                {
                    usuariosConectados.Add(credenciales);
                    new Thread(hiloSalaEspera).Start(new Usuario(sCliente, bd.leerUsuarioCompleto(credenciales)));
                }
            }
            if (accion == Clave.Login && usuarioRegistrado && usuarioLogeado || accion == Clave.Login && !usuarioRegistrado)
            {
                mandarMensaje(sw, Clave.LoginInvalido);
            }

            if (finConexion)
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
                if (sCliente != null)
                {
                    sCliente.Close();
                }
            }
        }

        /// <summary>
        /// Dado un flujo de escritura manda a través de este
        /// un mensaje tambien recibido como parametro.
        /// </summary>
        /// <param name="sw">Flujo de escritura.</param>
        /// <param name="mensaje">Mensaje a enviar.</param>
        static private void mandarMensaje(StreamWriter sw, string mensaje)
        {
            sw.WriteLine(mensaje);
            sw.Flush();
        }

        /// <summary>
        /// Dado un flujo de lectura recoge un dato del mismo y lo devuelve
        /// </summary>
        /// <param name="sr">Flujo para leer</param>
        /// <returns>Mensaje recibido</returns>
        static private string leerMensaje(StreamReader sr)
        {
            string mensaje = "";
            try
            {
                mensaje = sr.ReadLine();
            }
            catch (IOException)
            {
                mensaje = "";
            }
            if (mensaje == null)
            {
                mensaje = "";
            }
            return mensaje;
        }

        /// <summary>
        /// Gestiona la interaccion del usuario recibido como parametro en el menu principal del juego
        /// </summary>
        /// <param name="usu">Usuario.</param>
        static void hiloSalaEspera(object usu)
        {
            Usuario usuario = usu as Usuario;
            Console.WriteLine("usuario a string : " + usuario.ToString());
            usuario.mandarMensaje(usuario.ToString());
            usuario.mandarMensaje(salas.Count() + "");
            listarSalas(usuario);
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
                        int numSala = Convert.ToInt32(usuario.Mensaje.Split(Clave.Separador)[1]);
                        lock (l)
                        {
                            if (salas[numSala].Llena)
                            {
                                usuario.mandarMensaje(Clave.SalaLlena);
                                listarSalas(usuario);
                            }
                            else
                            {
                                usuario.mandarMensaje(Clave.SalaDisponible);
                                salas[numSala].Usuarios.Add(usuario);
                                salas[numSala].Llena = true;
                                usuario.Jugando = true;
                            }
                        }
                    }
                    if (usuario.Mensaje == Clave.ListaSalas)
                    {
                        listarSalas(usuario);
                    }
                }
            }
        }

        /// <summary>
        /// Manda a un usuario los datos de las salas actuales
        /// </summary>
        /// <param name="usuario">Usuario a quien mandar informacion</param>
        static public void listarSalas(Usuario usuario)
        {
            lock (l)
            {
                foreach (Sala sala in salas)
                {
                    usuario.mandarMensaje(sala.ToString());
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
