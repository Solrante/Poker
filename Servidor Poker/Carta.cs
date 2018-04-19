using System;
using System.Collections.Generic;

namespace Servidor_Poker
{
    /// <summary>
    /// Determina el palo de la carta
    /// </summary>
    enum ePalo
    {
        TREBOLES,
        PICAS,
        DIAMANTES,
        CORAZONES
    }

    /// <summary>
    /// Determina que carta es
    /// </summary>
    enum eCarta
    {
        DOS,
        TRES,
        CUATRO,
        CINCO,
        SEIS,
        SIETE,
        OCHO,
        NUEVE,
        DIEZ,
        J,
        Q,
        K,
        AS
    }
    /// <summary>
    /// Definicion de una carta del juego
    /// </summary>
    class Carta
    {
        /// <summary>
        /// Conjunto de valores numericos de las cartas
        /// </summary>
        private static Dictionary<eCarta, int> cardValueMap;


        /// <summary>
        /// Devuelve el conjunto de valores numericos de las cartas.
        /// </summary>
        /// <returns>Conjunto de valores numericos.</returns>
        public static Dictionary<eCarta, int> getCardValueMap(){

            if (cardValueMap==null)
            {
                cardValueMap = new Dictionary<eCarta, int>();

                cardValueMap.Add(eCarta.AS,1);
                cardValueMap.Add(eCarta.DOS, 2);
                cardValueMap.Add(eCarta.TRES, 3);
                cardValueMap.Add(eCarta.CUATRO, 4);
                cardValueMap.Add(eCarta.CINCO, 5);
                cardValueMap.Add(eCarta.SEIS, 6);
                cardValueMap.Add(eCarta.SIETE, 7);
                cardValueMap.Add(eCarta.OCHO, 8);
                cardValueMap.Add(eCarta.NUEVE, 9);
                cardValueMap.Add(eCarta.DIEZ, 10);
                cardValueMap.Add(eCarta.J, 10);
                cardValueMap.Add(eCarta.Q, 10);
                cardValueMap.Add(eCarta.K, 10);
            }
            return cardValueMap;
        }
 
        /// <summary>
        /// Inicializa una instancia de la clase  <see cref="Carta"/>.
        /// </summary>
        public Carta(ePalo palo, eCarta carta)
        {
            Palo = palo;
            Valor = carta;
        }

        /// <summary>
        /// Gets or sets el palo de la carta.
        /// </summary>
        /// <value>
        /// Palo.
        /// </value>
        public ePalo Palo { get; set; }

        /// <summary>
        /// Gets or sets el valor de la carta.
        /// </summary>
        /// <value>
        /// Valor.
        /// </value>
        public eCarta Valor { get; set; }
     
        /// <summary>
        /// Devuelve un <see cref="System.String" /> que representa esta instancia.
        /// </summary>
        /// <returns>
        /// Un <see cref="System.String" /> que representa esta instancia.
        /// </returns>
        public override string ToString()
        {
            return Palo + "-" + Valor;
        }
    }
}
