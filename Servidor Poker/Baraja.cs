using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor_Poker
{
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
        /// Inicializa una instancia de la clase <see cref="Baraja"/>.
        /// </summary>
        public Baraja()
        {
            definirContenido();
        }

        /// <summary>
        /// Llena el contenido de la bajara con cartas.
        /// </summary>
        private void definirContenido()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {                    
                    contenido[i, j] = new Carta((ePalo)i, (eCarta)j);
                }
            }
        }
    }
}
