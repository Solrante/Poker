using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Inicializa una instancia de la clase <see cref="Mano"/>.
        /// </summary>
        public Mano()
        {

        }

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

        public int valorNumerico()
        {
            int valor = 0;
            foreach (Carta carta in cartas)
            {
                switch (carta.Valor)
                {
                    case eCarta.DOS:
                        valor += 2;
                        break;
                    case eCarta.TRES:
                        valor += 3;
                        break;
                    case eCarta.CUATRO:
                        valor += 4;
                        break;
                    case eCarta.CINCO:
                        valor += 5;
                        break;
                    case eCarta.SEIS:
                        valor += 6;
                        break;
                    case eCarta.SIETE:
                        valor += 7;
                        break;
                    case eCarta.OCHO:
                        valor += 8;
                        break;
                    case eCarta.NUEVE:
                        valor += 9;
                        break;
                    case eCarta.DIEZ:
                        valor += 10;
                        break;
                    case eCarta.J:
                        valor += 10;
                        break;
                    case eCarta.Q:
                        valor += 10;
                        break;
                    case eCarta.K:
                        valor += 10;
                        break;
                    case eCarta.AS:
                        if (cartas.IndexOf(carta) == 0)
                        {
                            valor += 10;
                        }
                        else
                        {
                            valor += 1;
                        }
                        break;
                }
            }
            return valor;
        }
    }
}
