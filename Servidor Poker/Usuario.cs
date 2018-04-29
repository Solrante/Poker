using System;
using System.IO;
using System.Net.Sockets;

namespace Servidor_Poker
{
    /// <summary>
    /// Definicion de usuario
    /// </summary>
    class Usuario
    {
        /// <summary>
        /// Conexion del usuario
        /// </summary>
        private Socket socket;

        /// <summary>
        /// Flujo de comunicación
        /// </summary>
        private NetworkStream ns;

        /// <summary>
        /// Flujo de escritura
        /// </summary>
        private StreamWriter sw;

        /// <summary>
        /// Flujo de lectura
        /// </summary>
        private StreamReader sr;

        /// <summary>
        /// Gets or sets el ID del usuario.
        /// </summary>
        /// <value>
        /// ID.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets la dirección de correo.
        /// </summary>
        /// <value>
        /// El correo.
        /// </value>
        public string Correo { set; get; }

        /// <summary>
        /// Gets or sets la contraseña.
        /// </summary>
        /// <value>
        /// La contraseña.
        /// </value>
        public string Contraseña { set; get; }

        /// <summary>
        /// Gets or sets el saldo.
        /// </summary>
        /// <value>
        /// El saldo.
        /// </value>
        public double Saldo { set; get; }

        /// <summary>
        /// Gets or sets el mensaje.
        /// </summary>
        /// <value>
        /// El mensaje.
        /// </value>
        public string Mensaje { set; get; }

        /// <summary>
        /// Gets or sets un valor indicando si este <see cref="Usuario"/> esta jugando.
        /// </summary>
        /// <value>
        ///   <c>true</c> si esta jugando; de otra manera, <c>false</c>.
        /// </value>
        public bool Jugando { get; set; }

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="Usuario"/>.
        /// </summary>
        /// <param name="socket">Socket de conexion.</param>
        /// <param name="datos">Credenciales del usuario.</param>
        public Usuario(Socket socket, string datos)
        {
            this.socket = socket;
            guardarDatos(datos);
            generarFlujos();
        }

        /// <summary>
        /// Guarda en variables de clase información recogida por parametro.
        /// </summary>
        /// <param name="datos">Información recibida.</param>
        private void guardarDatos(string datos)
        {
            ID = Convert.ToInt32(datos.Split(Clave.SeparadorCredenciales)[0]);
            Correo = datos.Split(Clave.SeparadorCredenciales)[1];
            Contraseña = datos.Split(Clave.SeparadorCredenciales)[2];
            Saldo = Convert.ToDouble(datos.Split(Clave.SeparadorCredenciales)[3]);

        }

        /// <summary>
        /// Inicializa los flujos del usuario de su Socket
        /// </summary>
        private void generarFlujos()
        {
            try
            {
                ns = new NetworkStream(socket);
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);
            }
            catch (IOException) { }
        }

        /// <summary>
        /// Devuelve las credenciales.
        /// </summary>
        /// <returns></returns>
        public string getCredenciales()
        {
            return Correo + Clave.SeparadorCredenciales + Contraseña;
        }


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

        /// <summary>
        /// Lee información desde el flujo de entrada del usuario y lo almacena en su variable Mensaje.
        /// </summary>
        public void leerMensaje()
        {
            string mensaje = Clave.Desconexion;
            try
            {
                mensaje = sr.ReadLine();
            }
            catch (IOException)
            {
                mensaje = Clave.Desconexion;
            }
            catch (ObjectDisposedException)
            {
                mensaje = Clave.Desconexion;
            }
            if (mensaje == null)
            {
                mensaje = Clave.Desconexion;
            }
            Mensaje = mensaje;
        }

        /// <summary>
        /// Devuelve un <see cref="System.String" /> que representa esta instancia.
        /// </summary>
        /// <returns>
        /// Un <see cref="System.String" /> que presesenta esta instancia.
        /// </returns>
        public override string ToString()
        {
            return Correo + Clave.SeparadorCredenciales + Saldo;
        }

        /// <summary>
        /// Cierra los flujos y conexion de la sesion del usuario.
        /// </summary>
        /// <returns></returns>
        public void cerrarSesion()
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
            if (socket != null)
            {
                socket.Close();
            }
        }
    }
}
