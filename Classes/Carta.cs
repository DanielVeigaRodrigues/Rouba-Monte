using System;

namespace Rouba_Monte.Classes
{
    public enum NaipeEnum 
    {
        Copas,
        Ouros,
        Paus,
        Espadas
    }
    public class Carta
    {
        private int _numero;

        private NaipeEnum _naipe;
        public int Numero {  get => _numero; set => _numero = value; }
        public NaipeEnum Naipe { get => _naipe; set => _naipe = value; }


        public Carta(int numero, NaipeEnum naipe)
        {
            _numero = numero;
            _naipe = naipe;
        }

        public override string ToString()
        {
            string numTexto = _numero switch
            {
                1 => "A ",
                10 => "10",
                11 => "Q ",
                12 => "J ",
                13 => "K ",
                _ => _numero.ToString() + " ",
            };

            string naipeTexto = _naipe switch
            {
                NaipeEnum.Paus => " ♣ ",
                NaipeEnum.Espadas => " ♠ ",
                NaipeEnum.Ouros => " ♦ ",
                NaipeEnum.Copas => " ♥ ",
                _ => ""
            };

            return "______"
                + $"\n|{numTexto}{naipeTexto}|"
                + $"\n| {naipeTexto} |"
                + $"\n|{naipeTexto}{numTexto}|"
                + "\n------\n";
        }

        public bool CompararCartas(Carta? carta)
        {
            // INFO: Igualdade simples no primeiro momento
            if(carta == null) {  return false; }
            return carta.Numero == _numero;
        }

    }
}
