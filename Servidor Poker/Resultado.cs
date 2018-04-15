using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
    class Resultado
    {
        private string jugador = "Gana Jugador";
        private string crupier = "Gana Crupier";
        private string empate = "Empate";

        public Resultado(Mano manoCrupier, Mano manoJugador, string modificador)
        {
            if (modificador == "se paso")
            {
                if (manoCrupier.valorNumerico() > 21)
                {
                    Valor = jugador;
                }
                if (manoJugador.valorNumerico() > 21)
                {
                    Valor = crupier;
                }
            }
            else if (modificador == "blackjack")
            {
                if (manoCrupier.valorNumerico() == 21 && manoJugador.valorNumerico() == 21)
                {
                    Valor = empate;
                }
                else if (manoJugador.valorNumerico() == 21)
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

        public string Valor { get; set; }
    }
}
