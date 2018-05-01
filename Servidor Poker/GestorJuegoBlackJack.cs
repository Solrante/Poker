using System;
namespace Servidor_Poker
{
    /// <summary>
    /// Gestor juego black jack.
    /// </summary>
    class GestorJuegoBlackJack
    {
        /// <summary>
        /// Baraja con la que se jugará.
        /// </summary>
        private Baraja bar = new Baraja();

        /// <summary>
        /// Objeto para control de uso de elementos comunes
        /// </summary>
        static readonly private object l = new object();

        /// <summary>
        /// Valor de referencia para que el crupier saque cartas
        /// </summary>
        private int limiteCrupier = 17;

        /// <summary>
        /// Conjunto de cartas del jugador.
        /// </summary>
        private Mano manoJugador = new Mano();

        /// <summary>
        /// Conjunto de cartas del crupier.
        /// </summary>
        private Mano manoCrupier = new Mano();

        /// <summary>
        /// Resultado de la partida.
        /// </summary>
        private Resultado resultado = null;

        /// <summary>
        /// Saldo del usuario actual.
        /// </summary>
        private double saldoDisponible = 0;

        /// <summary>
        /// Apuesta realizada.
        /// </summary>
        private int apuesta = 0;

        /// <summary>
        /// Usuario en juego.
        /// </summary>
        private Usuario usuario;

        /// <summary>
        /// Indica si el usuario se ha retirado
        /// </summary>
        private bool retirado = false;

        /// <summary>
        /// 
        /// </summary>
        private bool partidaEmpezada = false;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:Servidor_Poker.GestorJuegoBlackJack"/> .
        /// </summary>
        /// <param name="user">User.</param>
        public GestorJuegoBlackJack(Usuario user)
        {
            usuario = user;
            saldoDisponible = usuario.Saldo;
            resultado = new Resultado();
        }

        /// <summary>
        /// Actualiza el estado de la partida
        /// </summary>
        public void ActualizarEstado()
        {
            switch (usuario.Mensaje.Split(Clave.Separador)[0].Trim())
            {
                case Clave.Ficha:
                    if (!partidaEmpezada)
                    {
                        apuesta = Convert.ToInt32(usuario.Mensaje.Split(Clave.Separador)[1].Trim());
                        saldoDisponible -= apuesta;
                        generarCartasIniciales();
                        usuario.mandarMensaje(Clave.ValorJugador + manoJugador.valorNumerico());
                        usuario.mandarMensaje(Clave.ValorCrupier + manoCrupier.valorNumerico());
                        usuario.mandarMensaje(Clave.Saldo + Clave.Separador + saldoDisponible);
                        partidaEmpezada = true;
                    }
                    break;
                case Clave.Plantarse:
                    if (partidaEmpezada)
                    {
                        Console.WriteLine("Crupier juega");
                        crupierJuega();
                    }
                    break;
                case Clave.Doblar:
                    if (partidaEmpezada)
                    {
                        saldoDisponible -= apuesta;
                        apuesta = apuesta * 2;
                        usuario.mandarMensaje(Clave.Saldo + Clave.Separador + saldoDisponible);
                    }
                    break;
                case Clave.Retirarse:
                    if (partidaEmpezada)
                    {
                        retirado = true;
                        finMano();
                    }
                    break;
                case Clave.Pedir:
                    if (partidaEmpezada)
                    {
                        Carta nCarta = bar.sacarCarta();
                        manoJugador.añadirCarta(nCarta);
                        usuario.mandarMensaje(Clave.CartaJugador + nCarta);
                        usuario.mandarMensaje(Clave.ValorJugador + manoJugador.valorNumerico());
                        if (resultado.comprobarManos(manoCrupier, manoJugador))
                        {
                            finMano();
                        }
                    }
                    break;
            }
            usuario.mandarMensaje(Clave.FinEnvio);

        }

        /// <summary>
        /// Gestiona las acciones tras acabar la partida
        /// </summary>
        private void finMano()
        {
            lock (l)
            {
                if (partidaEmpezada)
                {
                    if (retirado)
                    {
                        saldoDisponible += apuesta / 2;
                    }
                    else
                    {
                        saldoDisponible += resultado.calcularGanancia(apuesta);
                    }
                    usuario.mandarMensaje(Clave.FinMano + Clave.Separador + resultado.Valor);
                    usuario.mandarMensaje(Clave.Saldo + Clave.Separador + saldoDisponible);
                    manoCrupier.vaciarMano();
                    manoJugador.vaciarMano();
                    usuario.Mensaje = "";
                    retirado = false;
                    partidaEmpezada = false;
                }
            }
        }

        /// <summary>
        /// El crupier saca cartas hasta su limite
        /// </summary>
        private void crupierJuega()
        {
            while (manoCrupier.valorNumerico() < limiteCrupier)
            {
                Carta carta = bar.sacarCarta();
                usuario.mandarMensaje(Clave.CartaCrupier + carta);
                manoCrupier.añadirCarta(carta);
                usuario.mandarMensaje(Clave.ValorCrupier + manoCrupier.valorNumerico());
                if (resultado.comprobarManos(manoCrupier, manoJugador))
                {
                    finMano();
                }
            }
            //Una vez sacas las cartas y no haber resultado por pasarse o blackjack le indicamos al resultado que fuerce el calculo del resultado
            if (resultado.comprobarManos(manoCrupier, manoJugador, true))
            {
                finMano();
            }

        }

        /// <summary>
        /// Genera las cartas iniciales de la partida y las envia al usuario
        /// </summary>
        private void generarCartasIniciales()
        {
            manoJugador.añadirCarta(bar.sacarCarta());
            manoJugador.añadirCarta(bar.sacarCarta());
            manoCrupier.añadirCarta(bar.sacarCarta());
            foreach (Carta carta in manoJugador.cartas)
            {
                usuario.mandarMensaje(Clave.CartaJugador + carta);
            }
            foreach (Carta carta in manoCrupier.cartas)
            {
                usuario.mandarMensaje(Clave.CartaCrupier + carta);
            }

            if (manoJugador.valorNumerico() == Resultado.limitePuntuacion)
            {
                resultado.comprobarManos(manoCrupier, manoJugador);
                finMano();
            }
        }

        /// <summary>
        /// Devuelve el saldo actual
        /// </summary>
        /// <returns>Saldo.</returns>
        public double getSaldo()
        {
            return saldoDisponible;
        }
    }
}
