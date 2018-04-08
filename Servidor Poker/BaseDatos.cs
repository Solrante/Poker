﻿using MySql.Data.MySqlClient;

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
        private MySqlConnection conexion;

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
            conexion = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";");
        }

        /// <summary>
        /// Actualiza los datos del usuario recibido por parametro en la base da datos.
        /// </summary>
        /// <param name="datos">Usuario recibido.</param>
        public void actualizarDatos(string datos)
        {
            //Actualizar el nuevo valor de saldo del usuario
        }

        /// <summary>
        /// Comprueba si un usuario esta registrado en la plataforma.
        /// </summary>
        /// <param name="credenciales">Credenciales a comprobar.</param>
        /// <returns> <c>true</c> si esta registado; de otra manera, <c>false</c>.</returns>
        public bool usuarioRegistrado(string credenciales)
        {
            string consulta = string.Format("select id from usuarios where correo = \"{0}\" and contraseña = \"{1}\"",
                credenciales.Split(',')[0], credenciales.Split(',')[1]);

            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            //Salta excepcion si el server esta off
            conexion.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                conexion.Close();
                return true;
            }
            else
            {
                conexion.Close();
                return false;
            }
        }

        /// <summary>
        /// Devuelve en formato de cadena de texto toda la información del usuario de la base de datos.
        /// </summary>
        /// <param name="credenciales">Credenciales de busqueda.</param>
        /// <returns>Información del usuario</returns>
        public string leerUsuarioCompleto(string credenciales)
        {
            string datos = "";
            string consulta = string.Format("select * from usuarios where correo = \"{0}\" and contraseña = \"{1}\"",
                credenciales.Split(',')[0], credenciales.Split(',')[1]);
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            conexion.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                datos += reader.GetInt32("id") + ",";
                datos += reader.GetString("correo") + ",";
                datos += reader.GetString(2) + ",";
                datos += reader.GetFloat("saldo");

            }
            return datos;
        }
    }
}
