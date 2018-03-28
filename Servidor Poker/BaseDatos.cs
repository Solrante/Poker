using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Servidor_Poker
{
    class BaseDatos
    {
        private MySqlConnection conexion;
        private const string server = "localhost";
        private const string database = "poker";
        private const string uid = "root";
        private const string password = "";

        public BaseDatos()
        {
            InicializarConexion();
        }

        private void InicializarConexion()
        {
            conexion = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";");
        }

        public void actualizarDatos(string datos)
        {
            //Actualizar el nuevo valor de saldo del usuario
        }

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
