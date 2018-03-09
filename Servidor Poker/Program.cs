using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    class Program
    {
        static private BaseDatos bd;
        static private List<Usuario> usuariosRegistrados = new List<Usuario>();

        static void Main(string[] args)
        {
            bd = new BaseDatos();
            usuariosRegistrados = bd.cargarUsuarios();
            Console.ReadKey();
        }
    }
}
