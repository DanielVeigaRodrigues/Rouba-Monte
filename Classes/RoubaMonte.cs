using System;
using System.Runtime;
using System.Text;

namespace Rouba_Monte.Classes
{
    internal class RoubaMonte
    {
        private LinkedList<Jogador> _jogadores;
        private List<Carta> _cartas;

        public Carta CartaDaVez { get; set; }
        public Jogador JogadorAtual {  get; set; }
        public Stack<Carta> MonteDeCompra {  get; set; }
        public List<Carta> AreaDeDescarte { get; set; }
        public bool Finalizado { get; set; }

        public RoubaMonte(List<Jogador> jogadores, int numCartas)
        {
            _jogadores = new LinkedList<Jogador>(jogadores);

            JogadorAtual = _jogadores.First();
            _cartas = new List<Carta>();

            int numBaralhos = (int)Math.Ceiling(numCartas/52.0);

            for (int i = 0; i < numBaralhos; i++)
            {
                Baralho baralho = new Baralho();
                baralho.Embaralhar();
                _cartas.AddRange(baralho.Cartas);
            }

            Console.WriteLine($"O baralho foi criado com {_cartas.Count} cartas");
            Console.WriteLine(
                $"Jogadores da partida: " +
                $"{String.Join(",", _jogadores.Select(jogador => jogador.Nome))}");

            MonteDeCompra = new Stack<Carta>(_cartas.Slice(0, numCartas));
            AreaDeDescarte = new List<Carta>();
            CartaDaVez = new Carta(2, NaipeEnum.Espadas);
        }

        public void Jogar()
        {
            Finalizado = MonteDeCompra.Count == 0;

            if (Finalizado)
            {
                Jogador vencedor = _jogadores.FirstOrDefault(jogador => jogador.Monte.Count == _jogadores.Max(p => p.Monte.Count));
                Console.WriteLine("\n-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-");
                Console.WriteLine($"{vencedor.Nome} venceu o jogo!");
                Console.WriteLine("-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-\n");

                vencedor.QuantidadeCartas = vencedor.Monte.Count;
                return;
            }

            CartaDaVez = MonteDeCompra.Pop();

            Imprimir();

            if (RoubarMaiorMonteJogador())
                return;
            if (RoubarMonteAreaDeDescarte())
                return;
            if (RoubarCartaDaVez())
                return;

            AreaDeDescarte.Add(CartaDaVez);
            JogadorAtual = ObterProximoJogador();
            Console.WriteLine($"\nÉ a vez de {JogadorAtual.Nome}\n");
            
        }

        private bool RoubarCartaDaVez()
        {
            if (CartaDaVez.CompararCartas(JogadorAtual.OlharTopoDoMonte()))
            {
                JogadorAtual.Monte.Push(CartaDaVez);
                Console.WriteLine($"\n{JogadorAtual.Nome} rouba a carta da vez!\n");

                return true;
            }

            return false;
        }

        private bool RoubarMonteAreaDeDescarte()
        {
            foreach (Carta carta in AreaDeDescarte)
            {
                if (carta.CompararCartas(CartaDaVez))
                {
                    AreaDeDescarte.Remove(carta);
                    JogadorAtual.Monte.Push(carta);
                    JogadorAtual.Monte.Push(CartaDaVez);

                    Console.WriteLine($"\n{JogadorAtual.Nome} rouba o {CartaDaVez.Nome()} da área de descarte!\n");
                    return true;
                }
            }

            return false;
        }

        private bool RoubarMaiorMonteJogador()
        {
            Dictionary<Jogador, int> jogadoresRoubaveis = new Dictionary<Jogador, int>();
            foreach (Jogador jogador in _jogadores)
            {
                if (jogador != JogadorAtual)
                {
                    if (CartaDaVez.CompararCartas(jogador.OlharTopoDoMonte()))
                    {
                        jogadoresRoubaveis.Add(jogador, jogador.Monte.Count);
                    }
                }
            }

            if(jogadoresRoubaveis.Count == 1)
            {
                JogadorAtual.RoubarMonte(jogadoresRoubaveis.Keys.FirstOrDefault());
                return true;
            } 
            else if(jogadoresRoubaveis.Count > 1) 
            {
                JogadorAtual.RoubarMonte(
                    jogadoresRoubaveis.Keys.FirstOrDefault(
                        jogador => jogador.Monte.Count == jogadoresRoubaveis.Values.Max()
                        )
                    );
                return true;
            }

            return false;
        }

        public void Imprimir()
        {
            Console.WriteLine("Carta da vez:");
            Console.WriteLine(CartaDaVez);
            Console.WriteLine("_____________________________\n");
            Console.WriteLine("Area de Descarte");
            Console.WriteLine(CartaUtils.CardListToString(AreaDeDescarte));
            Console.WriteLine("");
            Console.WriteLine("Montes:\n");
            Console.WriteLine(CartaUtils.ImprimirMontesDosJogadores(_jogadores));
        }



        private Jogador ObterProximoJogador()
        {
            if(_jogadores.Find(JogadorAtual).Next != null)
            {
                return _jogadores.Find(JogadorAtual).Next.Value;
            }
            return _jogadores.First();
        }

    }
}
