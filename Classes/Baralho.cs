using System;

namespace Rouba_Monte.Classes
{
    public class Baralho
    {
        private List<Carta> _cartas;
        public List<Carta> Cartas {get => _cartas; set { _cartas = value; } }

        public Baralho()
        {
            _cartas = new List<Carta>();
            int[] valores = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            foreach (int valor in valores)
            {
                foreach(NaipeEnum naipe in Enum.GetValues(typeof(NaipeEnum)))
                {
                    _cartas.Add(new Carta(valor, naipe));
                }
            }

            Cartas = _cartas;
        }

        public void Embaralhar()
        {
            List<Carta> newCartaList = new List<Carta>();
            Carta[] cartasArray = Cartas.ToArray();
            Random.Shared.Shuffle(cartasArray);

            foreach(Carta carta in cartasArray)
            {
                newCartaList.Add(carta);
            }

            _cartas = newCartaList;
        }
    }
}
