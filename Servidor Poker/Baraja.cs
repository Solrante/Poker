using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    class Baraja
    {
        /// <summary>
        /// Generador de numeros pseudoaleatorio de la clase
        /// </summary>
        private Random aleatorio;

        /// <summary>
        /// Conjuto de cartas que conforman la bajara
        /// </summary>
        private Carta[,] contenido = new Carta[4, 13];

        /// <summary>
        /// Lista de cartas ya repartidas
        /// </summary>
        private List<Carta> cartasRepartidas = new List<Carta>();

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="Baraja"/>.
        /// </summary>
        public Baraja()
        {
            aleatorio = new Random();
            definirContenido();
        }

        /// <summary>
        /// Llena el contenido de la bajara con cartas.
        /// </summary>
        private void definirContenido()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    contenido[i, j] = new Carta((ePalo)i, (eCarta)j);
                }
            }
        }

        /// <summary>
        /// Devuelve una carta no repartida de la baraja.
        /// </summary>
        /// <returns>Carta a repartir</returns>
        public Carta sacarCarta()
        {
            Carta a = null;
            a = contenido[aleatorio.Next(0, 3), aleatorio.Next(0, 12)];
            while (cartasRepartidas.Contains(a))
            {
                Console.WriteLine("Carta Repetida");
                a = contenido[aleatorio.Next(0, 3), aleatorio.Next(0, 12)];
            }
            cartasRepartidas.Add(a);
            return a;
        }

        /// <summary>
        /// Deja la instancia en su estado original
        /// </summary>
        public void reiniciar()
        {
            cartasRepartidas.Clear();
        }
    }
}
