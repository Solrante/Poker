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
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public BaseDatos()
        {
            InicializarConexion();
        }

        private void InicializarConexion()
        {
            server = "localhost";
            database = "poker";
            uid = "root";
            password = "";
            connection = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";");
        }

        public Boolean usuarioRegistrado(Usuario usuario)
        {
            return false;//
        }
    }
}
