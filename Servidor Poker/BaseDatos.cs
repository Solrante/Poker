using MySql.Data.MySqlClient;
using System;

namespace Servidor_Poker
{
    /// <summary>
    /// Definicion de conexion a la base de datos
    /// </summary>
    class BaseDatos
    {
        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        private MySqlConnection conexionLectura;

        /// <summary>
        /// Conexion a la base de datos
        /// </summary>
        private MySqlConnection conexionEscritura;

        /// <summary>
        /// Nombre del servidor
        /// </summary>
        private const string server = "localhost";

        /// <summary>
        /// Nombre de la base de datos
        /// </summary>
        private const string database = "poker";

        /// <summary>
        /// Usuario de acceso
        /// </summary>
        private const string uid = "root";

        /// <summary>
        /// Contraseña de acceso
        /// </summary>
        private const string password = "";

        /// <summary>
        /// Plantilla para actualizar saldo de usuario
        /// </summary>
        private const string queryActualizarSaldo = "update usuarios set saldo = {1} where correo = \"{0}\"";

        /// <summary>
        /// Plantilla para leer usuario
        /// </summary>
        private const string queryLeerUsuario = "select * from usuarios where correo = \"{0}\" and contraseña = \"{1}\"";

        /// <summary>
        /// Platinlla para registrar usuario
        /// </summary>
        private const string queryRegistrarUsuario = "insert into usuarios (correo , contraseña) values ('{0}','{1}')";

        /// <summary>
        /// Indica si las conexiones de abrieron correctamente.
        /// </summary>
        public bool baseDeDatosActiva = false;

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="BaseDatos"/>.
        /// </summary>
        public BaseDatos()
        {
            InicializarConexion();
        }

        /// <summary>
        /// Inicializa la instancia de la variable de clase conexion.
        /// </summary>
        private void InicializarConexion()
        {
            try
            {
                conexionLectura = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";");
                conexionEscritura = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";");
                baseDeDatosActiva = true;
            }
            catch (MySqlException)
            {
                baseDeDatosActiva = false;
            }

        }

        /// <summary>
        /// Actualiza los datos del usuario recibido por parametro en la base da datos.
        /// </summary>
        /// <param name="datos">Usuario recibido.</param>
        public void actualizarDatos(string datos)
        {
            string consulta = string.Format(queryActualizarSaldo,
                datos.Split(Clave.SeparadorCredenciales)[0],
                datos.Split(Clave.SeparadorCredenciales)[1]);

            conexionEscritura.Open();
            MySqlCommand cmd = new MySqlCommand(consulta, conexionEscritura);
            cmd.ExecuteNonQuery();
            conexionEscritura.Close();
        }

        /// <summary>
        /// Registra un usuario en la base de datos
        /// </summary>
        /// <param name="credenciales">Datos del usuario a registrar.</param>
        public void registrarUsuario(string credenciales)
        {
            if (credenciales.Contains(Clave.SeparadorCredenciales.ToString()))
            {
                string consulta = string.Format(queryRegistrarUsuario,
                    credenciales.Split(Clave.SeparadorCredenciales)[0],
                    credenciales.Split(Clave.SeparadorCredenciales)[1]);
                conexionEscritura.Open();
                MySqlCommand cmd = new MySqlCommand(consulta, conexionEscritura);
                cmd.ExecuteNonQuery();
                conexionEscritura.Close();
            }
        }

        /// <summary>
        /// Comprueba si un usuario esta registrado en la plataforma.
        /// </summary>
        /// <param name="credenciales">Credenciales a comprobar.</param>
        /// <returns> <c>true</c> si esta registado; de otra manera, <c>false</c>.</returns>
        public bool usuarioRegistrado(string credenciales)
        {
            bool resultado = false;
            if (credenciales.Contains(Clave.SeparadorCredenciales.ToString()))
            {
                string consulta = string.Format(queryLeerUsuario,
                    credenciales.Split(Clave.SeparadorCredenciales)[0],
                    credenciales.Split(Clave.SeparadorCredenciales)[1]);

                MySqlCommand cmd = new MySqlCommand(consulta, conexionLectura);
                conexionLectura.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    resultado = true;
                }
                reader.Close();
                conexionLectura.Close();
            }
            return resultado;
        }

        /// <summary>
        /// Devuelve en formato de cadena de texto toda la información del usuario de la base de datos.
        /// </summary>
        /// <param name="credenciales">Credenciales de busqueda.</param>
        /// <returns>Información del usuario</returns>
        public string leerUsuarioCompleto(string credenciales)
        {
            string datos = "";
            string consulta = string.Format(queryLeerUsuario,
                credenciales.Split(Clave.SeparadorCredenciales)[0],
                credenciales.Split(Clave.SeparadorCredenciales)[1]);

            MySqlCommand cmd = new MySqlCommand(consulta, conexionLectura);
            conexionLectura.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                datos += reader.GetInt32("id");
                datos += Clave.SeparadorCredenciales;
                datos += reader.GetString("correo") + Clave.SeparadorCredenciales;
                datos += reader.GetString(2) + Clave.SeparadorCredenciales;
                datos += reader.GetFloat("saldo");
            }
            reader.Close();
            conexionLectura.Close();

            return datos;
        }
    }
}