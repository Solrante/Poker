using System.Collections.Generic;

namespace Servidor_Poker
{
    /// <summary>
    /// Definicion de las cartas en juego para una instancia
    /// </summary>
    class Mano
    {
        /// <summary>
        /// Listado de cartas de la mano
        /// </summary>
        public List<Carta> cartas = new List<Carta>();

        /// <summary>
        /// Valor númerico del primer as de la mano
        /// </summary>
        private const int valorPrimerAs = 11;

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="Mano"/>.
        /// </summary>
        public Mano() { }

        /// <summary>
        /// Devuelve la mano a su estado inicial para una nueva partida.
        /// </summary>
        public void vaciarMano()
        {
            cartas.Clear();
        }

        /// <summary>
        /// Añade una carta recibida como parametro a las cartas de la mano .
        /// </summary>
        /// <param name="carta">Carta recibida.</param>
        public void añadirCarta(Carta carta)
        {
            cartas.Add(carta);
        }

        /// <summary>
        /// Devuelve el la suma del valor de las cartas en la mano.
        /// </summary>
        /// <returns>Valor númerico del total de cartas en la mano</returns>
        public int valorNumerico()
        {
            int valor = 0;
            bool primerAs = true;
            foreach (Carta carta in cartas)
            {
                if (carta.Valor == eCarta.AS && primerAs)
                {
                    valor += valorPrimerAs;
                    primerAs = false;
                }
                else
                {
                    valor += Carta.getCardValueMap()[carta.Valor];
                }
            }
            return valor;
        }

        public bool esPrimeraMano()
        {
            if (cartas.Count == 2)
            {
                return true;
            }
            return false;
        }
    }
}
