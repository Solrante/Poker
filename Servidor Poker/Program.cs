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
            //salas.Add(new Sala("1,0,0", eSala.BLACKJACK));
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

            if (bd.usuarioRegistrado(credenciales))
            {
                sw.WriteLine(ClaveComunicacion.Login + ClaveComunicacion.Separador + ClaveComunicacion.Valido);
                sw.Flush();
                new Thread(hiloSalaEspera).Start(new Usuario(sCliente, bd.leerUsuarioCompleto(credenciales)));
            }
            else
            {
                sw.WriteLine(ClaveComunicacion.Login + ClaveComunicacion.Separador + ClaveComunicacion.Invalido);
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
            while (usuario.Mensaje != ClaveComunicacion.Desconexion)
            {
                if (!usuario.Jugando)
                {
                    usuario.leerMensaje();
                    if (usuario.Mensaje == ClaveComunicacion.Desconexion)
                    {
                        lock (l)
                        {
                            usuario.cerrarSesion();
                        }
                    }
                    else if (usuario.Mensaje.Split(ClaveComunicacion.Separador)[0] == ClaveComunicacion.Sala)
                    {
                        usuario.Jugando = true;
                        int numSala = Convert.ToInt32(usuario.Mensaje.Split(ClaveComunicacion.Separador)[1]);
                        salas[numSala].Usuarios.Add(usuario);
                    }
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
            Baraja bar = new Baraja(); //Bajara con la que se jugará.
            Sala sala = s as Sala; // Sala
            Usuario usuario = null; //Usuario en juego
            Mano manoJugador = new Mano(); //Conjunto de cartas del jugador
            Mano manoCrupier = new Mano(); //Conjunto de cartas del crupier
            Resultado resultado = null; //Resultado de la partida
            double saldoDisponible = 0; //Saldo del usuario actual
            int apuesta = 0; //Apuesta realizada
            bool finMano = false; //Bandera para determinal si la partida ha finalizado
            while (true)
            {
                if (sala.Usuarios.Count != 0)
                {

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
                        switch (usuario.Mensaje.Split(ClaveComunicacion.Separador)[0].Trim())
                        {
                            case "Ficha":
                                apuesta = Convert.ToInt32(usuario.Mensaje.Split(ClaveComunicacion.Separador)[1].Trim());
                                saldoDisponible -= apuesta;
                                Console.WriteLine("Saldo actual : " + saldoDisponible);
                                manoJugador.añadirCarta(bar.sacarCarta());
                                manoJugador.añadirCarta(bar.sacarCarta());
                                manoCrupier.añadirCarta(bar.sacarCarta());
                                foreach (Carta carta in manoJugador.cartas)
                                {
                                    usuario.mandarMensaje(ClaveComunicacion.Carta + ClaveComunicacion.Separador + ClaveComunicacion.Jugador + ClaveComunicacion.Separador + carta);
                                }
                                foreach (Carta carta in manoCrupier.cartas)
                                {
                                    usuario.mandarMensaje(ClaveComunicacion.Carta + ClaveComunicacion.Separador + ClaveComunicacion.Crupier + ClaveComunicacion.Separador + carta);
                                }
                                break;
                            case "Plantarse":
                                while (manoCrupier.valorNumerico() < 17)
                                {
                                    
                                    Carta carta = bar.sacarCarta();
                                    Console.WriteLine(ClaveComunicacion.Carta + ClaveComunicacion.Separador + ClaveComunicacion.Crupier + ClaveComunicacion.Separador + carta);
                                    Console.WriteLine(manoCrupier.valorNumerico());
                                    usuario.mandarMensaje(ClaveComunicacion.Carta + ClaveComunicacion.Separador + ClaveComunicacion.Crupier + ClaveComunicacion.Separador + carta);
                                    manoCrupier.añadirCarta(carta);
                                }
                                if (manoCrupier.valorNumerico() > 21)
                                {
                                    resultado = new Resultado(manoCrupier, manoJugador, eModificadorResultado.SEPASO);
                                }
                                else
                                {
                                    resultado = new Resultado(manoCrupier, manoJugador, eModificadorResultado.SINMODIFICADOR);
                                }

                                finMano = true;
                                break;
                            case "Pedir":
                                Carta nCarta = bar.sacarCarta();
                                manoJugador.añadirCarta(nCarta);
                                usuario.mandarMensaje(ClaveComunicacion.Carta + ClaveComunicacion.Separador + ClaveComunicacion.Jugador + ClaveComunicacion.Separador + nCarta);
                                if (manoJugador.valorNumerico() > 21)
                                {
                                    resultado = new Resultado(manoCrupier, manoJugador, eModificadorResultado.SEPASO);
                                    finMano = true;
                                }
                                if (manoJugador.valorNumerico() == 21)
                                {
                                    resultado = new Resultado(manoCrupier, manoJugador, eModificadorResultado.SINMODIFICADOR);
                                    finMano = true;
                                }
                                break;
                        }

                        //Si llegamos a un resultado se ejecutara la comprobacion del mismo
                        if (finMano)
                        {

                            if (resultado.Valor == "Gana Jugador")
                            {
                                saldoDisponible += apuesta * 2;
                            }
                            else if (resultado.Valor == "Empate")
                            {
                                saldoDisponible += apuesta;
                            }
                            usuario.mandarMensaje(ClaveComunicacion.FinMano + ClaveComunicacion.Separador + resultado.Valor);

                            manoCrupier.vaciarMano();
                            manoJugador.vaciarMano();
                            Console.WriteLine("Manos despues de final : " + manoCrupier.valorNumerico() + ":" + manoJugador.valorNumerico());
                            finMano = false;
                        }

                        usuario.mandarMensaje(ClaveComunicacion.FinEnvio);
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
