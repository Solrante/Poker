using System.Collections.Generic;
using System.Drawing;

namespace Cliente_Poker
{
    /// <summary>
    /// Definicion de carta
    /// </summary>
    public class Carta
    {
        /// <summary>
        /// Conjunto de valores de imagen para las cartas
        /// </summary>
        private static Dictionary<string, Bitmap> cardMap;

        /// <summary>
        /// Devuelve el conjuto de valores de imagen de las cartas.
        /// </summary>
        /// <returns>Conjunto de cartas</returns>
        public static Dictionary<string, Bitmap> GetMap()
        {

            if (cardMap == null)
            {
                cardMap = new Dictionary<string, Bitmap>();

                //Añadimos picas
                cardMap.Add("PICAS-AS", Properties.Resources.cardSpadesA);
                cardMap.Add("PICAS-DOS", Properties.Resources.cardSpades2);
                cardMap.Add("PICAS-TRES", Properties.Resources.cardSpades3);
                cardMap.Add("PICAS-CUATRO", Properties.Resources.cardSpades4);
                cardMap.Add("PICAS-CINCO", Properties.Resources.cardSpades5);
                cardMap.Add("PICAS-SEIS", Properties.Resources.cardSpades6);
                cardMap.Add("PICAS-SIETE", Properties.Resources.cardSpades7);
                cardMap.Add("PICAS-OCHO", Properties.Resources.cardSpades8);
                cardMap.Add("PICAS-NUEVE", Properties.Resources.cardSpades9);
                cardMap.Add("PICAS-DIEZ", Properties.Resources.cardSpades10);
                cardMap.Add("PICAS-J", Properties.Resources.cardSpadesJ);
                cardMap.Add("PICAS-Q", Properties.Resources.cardSpadesQ);
                cardMap.Add("PICAS-K", Properties.Resources.cardSpadesK);

                //Añadimos treboles
                cardMap.Add("TREBOLES-AS", Properties.Resources.cardClubsA);
                cardMap.Add("TREBOLES-DOS", Properties.Resources.cardClubs2);
                cardMap.Add("TREBOLES-TRES", Properties.Resources.cardClubs3);
                cardMap.Add("TREBOLES-CUATRO", Properties.Resources.cardClubs4);
                cardMap.Add("TREBOLES-CINCO", Properties.Resources.cardClubs5);
                cardMap.Add("TREBOLES-SEIS", Properties.Resources.cardClubs6);
                cardMap.Add("TREBOLES-SIETE", Properties.Resources.cardClubs7);
                cardMap.Add("TREBOLES-OCHO", Properties.Resources.cardClubs8);
                cardMap.Add("TREBOLES-NUEVE", Properties.Resources.cardClubs9);
                cardMap.Add("TREBOLES-DIEZ", Properties.Resources.cardClubs10);
                cardMap.Add("TREBOLES-J", Properties.Resources.cardClubsJ);
                cardMap.Add("TREBOLES-Q", Properties.Resources.cardClubsQ);
                cardMap.Add("TREBOLES-K", Properties.Resources.cardClubsK);

                //Añadimos diamantes
                cardMap.Add("DIAMANTES-AS", Properties.Resources.cardDiamondsA);
                cardMap.Add("DIAMANTES-DOS", Properties.Resources.cardDiamonds2);
                cardMap.Add("DIAMANTES-TRES", Properties.Resources.cardDiamonds3);
                cardMap.Add("DIAMANTES-CUATRO", Properties.Resources.cardDiamonds4);
                cardMap.Add("DIAMANTES-CINCO", Properties.Resources.cardDiamonds5);
                cardMap.Add("DIAMANTES-SEIS", Properties.Resources.cardDiamonds6);
                cardMap.Add("DIAMANTES-SIETE", Properties.Resources.cardDiamonds7);
                cardMap.Add("DIAMANTES-OCHO", Properties.Resources.cardDiamonds8);
                cardMap.Add("DIAMANTES-NUEVE", Properties.Resources.cardDiamonds9);
                cardMap.Add("DIAMANTES-DIEZ", Properties.Resources.cardDiamonds10);
                cardMap.Add("DIAMANTES-J", Properties.Resources.cardDiamondsJ);
                cardMap.Add("DIAMANTES-Q", Properties.Resources.cardDiamondsQ);
                cardMap.Add("DIAMANTES-K", Properties.Resources.cardDiamondsK);

                //Añadimos corazones
                cardMap.Add("CORAZONES-AS", Properties.Resources.cardHeartsA);
                cardMap.Add("CORAZONES-DOS", Properties.Resources.cardHearts2);
                cardMap.Add("CORAZONES-TRES", Properties.Resources.cardHearts3);
                cardMap.Add("CORAZONES-CUATRO", Properties.Resources.cardHearts4);
                cardMap.Add("CORAZONES-CINCO", Properties.Resources.cardHearts5);
                cardMap.Add("CORAZONES-SEIS", Properties.Resources.cardHearts6);
                cardMap.Add("CORAZONES-SIETE", Properties.Resources.cardHearts7);
                cardMap.Add("CORAZONES-OCHO", Properties.Resources.cardHearts8);
                cardMap.Add("CORAZONES-NUEVE", Properties.Resources.cardHearts9);
                cardMap.Add("CORAZONES-DIEZ", Properties.Resources.cardHearts10);
                cardMap.Add("CORAZONES-J", Properties.Resources.cardHeartsJ);
                cardMap.Add("CORAZONES-Q", Properties.Resources.cardHeartsQ);
                cardMap.Add("CORAZONES-K", Properties.Resources.cardHeartsK);
            }

            return cardMap;
        }
    }
}
