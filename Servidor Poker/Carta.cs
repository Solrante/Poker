using System;

namespace Servidor_Poker
{
    /// <summary>
    /// Determina el palo de la carta
    /// </summary>
    enum ePalo
    {
        PICAS,
        TREBOLES,
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
            //Console.WriteLine((int)Palo + "-" + (int)Valor);
            return Palo + "-" + Valor;
        }
    }
}
