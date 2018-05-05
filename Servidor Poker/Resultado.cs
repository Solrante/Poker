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
        public const string rJugador = "Gana Jugador";

        /// <summary>
        /// Valor de respuesta favorable al crupier
        /// </summary>
        public const string rCrupier = "Gana Crupier";

        /// <summary>
        /// Valor de respuesta de empate
        /// </summary>
        public const string rEmpate = "Empate";

        /// <summary>
        /// Indica la cantidad de puntos limite
        /// </summary>
        public const int limitePuntuacion = 21;

        /// <summary>
        /// Modificador del resultado de la partida
        /// </summary>
        private eModificadorResultado modificador;

        /// <summary>
        /// Comprueba el estado de la partida según las manos recibidas como parametro y un condicionante
        /// de fin de partida como booleano
        /// </summary>
        /// <returns><c>true</c>, Si las manos indican fin de partida, <c>false</c> de otra manera.</returns>
        /// <param name="crupier">Crupier.</param>
        /// <param name="jugador">Jugador.</param>
        /// <param name="plantarse">Si el jugador eligio plantarse <c>true</c>.</param>
        public bool comprobarManos(Mano crupier, Mano jugador, bool plantarse = false)
        {
            if (plantarse)
            {
                calcularValor(crupier, jugador);
                return true;
            }
            else if (crupier.valorNumerico() >= limitePuntuacion || jugador.valorNumerico() >= limitePuntuacion)
            {
                calcularValor(crupier, jugador);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calcula el valor segun las manos recibidas como parametros
        /// </summary>
        /// <param name="crupier">Crupier.</param>
        /// <param name="jugador">Jugador.</param>
        private void calcularValor(Mano crupier, Mano jugador)
        {

            if (crupier.valorNumerico() > limitePuntuacion)
            {
                Valor = rJugador;
            }
            else if (jugador.valorNumerico() > limitePuntuacion)
            {
                Valor = rCrupier;
            }
            else if (crupier.valorNumerico() == jugador.valorNumerico())
            {
                Valor = rEmpate;
            }
            else if (crupier.valorNumerico() > jugador.valorNumerico())
            {
                Valor = rCrupier;
            }
            else if (crupier.valorNumerico() < jugador.valorNumerico())
            {
                Valor = rJugador;
            }
            else if (jugador.esPrimeraMano() && jugador.valorNumerico() == limitePuntuacion)
            {
                Valor = rJugador;
                modificador = eModificadorResultado.BLACKJACK;
            }
            else if (crupier.esPrimeraMano() && crupier.valorNumerico() == limitePuntuacion)
            {
                Valor = rCrupier;
                modificador = eModificadorResultado.BLACKJACK;
            }
        }

        /// <summary>
        /// Genera el valor de ganancia dado el resultado y una cantidad apostada
        /// </summary>
        /// <returns>Ganancia.</returns>
        /// <param name="apuesta">Apuesta.</param>
        public int calcularGanancia(int apuesta)
        {
            if (Valor == rJugador)
            {
                if (modificador == eModificadorResultado.BLACKJACK)
                {
                    return apuesta * 2 + apuesta / 2;
                }
                else
                {
                    return apuesta * 2;
                }

            }
            else if (Valor == rEmpate)
            {
                return apuesta;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets el valor del resultado.
        /// </summary>
        /// <value>The valor.</value>
        public string Valor { get; set; }
    }
}
