using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Rouba_Monte.Classes
{
    public static class CartaUtils
    {
        public static string CardListToString(List<Carta> cartas)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] allCards = new string[6];
            foreach (Carta carta in cartas)
            {
                string[] splitted = carta.ToString().Split("\n");
                for(int i = 0; i < allCards.Length; i++)
                {
                    allCards[i] += splitted[i];
                    allCards[i] += " ";
                }
            }

            foreach(string cardString in allCards)
            {
                stringBuilder.Append(cardString + "\n");
            }

            return stringBuilder.ToString();
        }

        public static string ImprimirMontesDosJogadores(LinkedList<Jogador> jogadores)
        {

            StringBuilder stringBuilder = new StringBuilder();
            string[] allCards = new string[7];


            foreach (Jogador jog in jogadores)
            {
                allCards[0] += jog.Nome + " " + jog.Monte.Count +" | ";

                if (jog.OlharTopoDoMonte() != null)
                {
                    string[] splitted = jog.OlharTopoDoMonte().ToString().Split("\n");
                    for (int i = 0; i < allCards.Length - 1; i++)
                    {
                        allCards[i+1] += splitted[i];
                        allCards[i+1] += " ";
                    }
                } else
                {
                    string[] splitted = new Carta().ToString().Split("\n");
                    for (int i = 0; i < allCards.Length - 1; i++)
                    {
                        allCards[i + 1] += splitted[i];
                        allCards[i + 1] += " ";
                    }
                }
            }

            foreach (string cardString in allCards)
            {
                stringBuilder.Append(cardString + "\n");
            }

            return stringBuilder.ToString();
        }
    }
}
