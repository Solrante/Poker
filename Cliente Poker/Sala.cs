using System;

namespace Cliente_Poker
{
    /// <summary>
    /// Determina el tipo de sala
    /// </summary>
    enum eSala
    {
        /// <summary>
        /// Sala de tipo blackjack
        /// </summary>
        BLACKJACK,
        /// <summary>
        /// Sala de tipo poker
        /// </summary>
        POKER
    }

    /// <summary>
    /// Definición de una sala de juego
    /// </summary>
    class Sala
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Sala"/> .
        /// </summary>
        /// <param name="datos">Informacion formateada.</param>
        public Sala(string datos)
        {
            string[] info = datos.Split(',');
            NumSala = Convert.ToInt32(info[0]);
            ApuestaMinima = Convert.ToInt32(info[1]);
            CuotaEntrada = Convert.ToInt32(info[2]);
            Tipo = (eSala)Convert.ToInt32(info[3]);
        }
        
        /// <summary>
        /// Gets or sets el tipo.
        /// </summary>
        /// <value>
        /// Tipo de sala.
        /// </value>
        public eSala Tipo { get; set; }

        /// <summary>
        /// Gets or sets el número de sala.
        /// </summary>
        /// <value>
        /// Numero de la sala.
        /// </value>        

        public int NumSala { get; set; }
        /// <summary>
        /// Gets or sets la apuesta minima.
        /// </summary>
        /// <value>
        /// La apuesta minima.
        /// </value>

        public int ApuestaMinima { get; set; }
        /// <summary>
        /// Gets or sets la cuota entrada.
        /// </summary>
        /// <value>
        /// La cuota entrada.
        /// </value>

        public int CuotaEntrada { get; set; }

        /// <summary>
        /// Devuelve un <see cref="System.String" /> que representa esta instancia.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", NumSala, ApuestaMinima, CuotaEntrada, Tipo);
        }
    }
}
