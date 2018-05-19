using System;
using System.Collections.Generic;

namespace Servidor_Poker
{
    /// <summary>
    /// Definicion de la baraja de juego
    /// </summary>
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
        private List<Carta> barajaJuego = new List<Carta>();

        /// <summary>
        /// Inicializa una instancia de la clase <see cref="Baraja"/>.
        /// </summary>
        public Baraja()
        {
            aleatorio = new Random();
            definirContenido();
            reiniciar();
        }

        /// <summary>
        /// Llena el contenido de la bajara con cartas.
        /// </summary>
        private void definirContenido(bool reinicio = false)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (!reinicio)
                    {
                        contenido[i, j] = new Carta((ePalo)i, (eCarta)j);
                    }
                    else
                    {
                        barajaJuego.Add(new Carta((ePalo)i, (eCarta)j));
                    }
                }
            }
        }

        /// <summary>
        /// Devuelve una carta no repartida de la baraja.
        /// </summary>
        /// <returns>Carta a repartir</returns>
        public Carta sacarCarta()
        {
            Carta carta = null;
            int tamaño = barajaJuego.Count;
            int posicion = aleatorio.Next(0, barajaJuego.Count - 1);
            carta = barajaJuego[posicion];
            barajaJuego.RemoveAt(posicion);
            return carta;
        }

        /// <summary>
        /// Deja la instancia en su estado original
        /// </summary>
        public void reiniciar()
        {
            barajaJuego.Clear();
            definirContenido(true);
        }
    }
}
