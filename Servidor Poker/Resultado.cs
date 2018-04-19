namespace Servidor_Poker
{

    /// <summary>
    /// Modificador del resultado
    /// </summary>
    public enum eModificadorResultado
    {
        /// <summary>
        /// Indica que se supero la cifra maxima
        /// </summary>
        SEPASO,
        /// <summary>
        /// Indica que se saco un 21
        /// </summary>
        BLACKJACK,
        /// <summary>
        /// Sin modificador
        /// </summary>
        SINMODIFICADOR
    }

    /// <summary>
    /// Definición del resultado de una partida
    /// </summary>
    class Resultado
    {
        /// <summary>
        /// Valor de respuesta favorable al jugador
        /// </summary>
        private const string jugador = "Gana Jugador";

        /// <summary>
        /// Valor de respuesta favorable al crupier
        /// </summary>
        private const string crupier = "Gana Crupier";

        /// <summary>
        /// Valor de respuesta de empate
        /// </summary>
        private const string empate = "Empate";

        /// <summary>
        /// Indica la cantidad de puntos limite
        /// </summary>
        private const int limitePuntuacion = 21;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:Servidor_Poker.Resultado"/> según
        /// parametros recibidos.
        /// </summary>
        /// <param name="manoCrupier">Mano crupier.</param>
        /// <param name="manoJugador">Mano jugador.</param>
        /// <param name="modificador">Modificador.</param>
        public Resultado(Mano manoCrupier, Mano manoJugador, eModificadorResultado modificador)
        {
            if (modificador == eModificadorResultado.SEPASO)
            {
                if (manoCrupier.valorNumerico() > limitePuntuacion)
                {
                    Valor = jugador;
                }
                if (manoJugador.valorNumerico() > limitePuntuacion)
                {
                    Valor = crupier;
                }
            }
            else if (modificador == eModificadorResultado.BLACKJACK)
            {
                if (manoCrupier.valorNumerico() == limitePuntuacion && manoJugador.valorNumerico() == limitePuntuacion)
                {
                    Valor = empate;
                }
                else if (manoJugador.valorNumerico() == limitePuntuacion)
                {
                    Valor = jugador;
                }
                else
                {
                    Valor = crupier;
                }
            }
            else
            {
                if (manoCrupier.valorNumerico() < manoJugador.valorNumerico())
                {
                    Valor = jugador;
                }
                if (manoCrupier.valorNumerico() > manoJugador.valorNumerico())
                {
                    Valor = crupier;
                }
                if (manoCrupier.valorNumerico() == manoJugador.valorNumerico())
                {
                    Valor = empate;
                }
            }

        }

        /// <summary>
        /// Gets or sets el valor del resultado.
        /// </summary>
        /// <value>The valor.</value>
        public string Valor { get; set; }
    }
}
