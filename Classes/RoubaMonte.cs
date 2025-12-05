using System;
using System.Runtime;

namespace Rouba_Monte.Classes
{
    internal class RoubaMonte
    {
        // INFO: Rebeca e Daniel, vou deixar algumas regions pra organizar o código.
        // INFO: Quando formos entregar nós removemos

        // TODO: Implementar lógica do jogo

        #region Inicialização do jogo
        // TODO: Implementar uma lista duplamente encadeada para gerenciar as rodadas
        private LinkedList<Jogador> _jogadores;
        private List<Carta> _cartas;
        public LinkedList<Jogador> Jogadores { get => _jogadores; set => _jogadores = value; }
        #endregion

        #region Gestão do jogo
        public Carta CartaDaVez { get; set; }
        public Jogador JogadorAtual {  get; set; }
        public Stack<Carta> MonteDeCompra {  get; set; }
        public List<Carta> AreaDeDescarte { get; set; }
        public bool Finalizado { get; set; }
        #endregion

        public RoubaMonte(List<Jogador> jogadores, int numCartas)
        {
            // TODO: Implementar uma lista duplamente encadeada para gerenciar as rodadas
            _jogadores = new LinkedList<Jogador>(jogadores);
            Jogadores = _jogadores;

            JogadorAtual = _jogadores.First();
            _cartas = new List<Carta>();

            int numBaralhos = (int)Math.Ceiling(numCartas/52.0);

            for (int i = 0; i < numBaralhos; i++)
            {
                Baralho baralho = new Baralho();
                baralho.Embaralhar();
                _cartas.AddRange(baralho.Cartas);
            }

            // TODO: Adicionar log da criação

            // INFO Populando montes
            MonteDeCompra = new Stack<Carta>(_cartas.Slice(0, numCartas));
            AreaDeDescarte = new List<Carta>();
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
            Finalizado = MonteDeCompra.Count == 0;
            Console.WriteLine($"É a vez de {JogadorAtual.Nome}");
        }

        private bool RoubarCartaDaVez()
        {
            if (CartaDaVez.CompararCartas(JogadorAtual.OlharTopoDoMonte()))
            {
                JogadorAtual.Monte.Push(CartaDaVez);
                Console.WriteLine($"{JogadorAtual.Nome} rouba a carta da vez!");

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

                    Console.WriteLine($"{JogadorAtual.Nome} rouba uma carta da área de descarte!");
                    return true;
                }
            }

            return false;
        }

        private bool RoubarMaiorMonteJogador()
        {
            Dictionary<Jogador, int> jogadoresRoubaveis = new Dictionary<Jogador, int>();
            foreach (Jogador jogador in Jogadores)
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
            Console.WriteLine($"Vez de {JogadorAtual.Nome}");
            Console.WriteLine("\n");
            Console.WriteLine("Carta da vez:");
            Console.WriteLine(CartaDaVez);
            Console.WriteLine("\n");
            Console.WriteLine("Area de Descarte");
            foreach (Carta carta in AreaDeDescarte)
            {
                Console.WriteLine(carta);
            }

            Console.WriteLine("");
            Console.WriteLine("Montes:\n");
            foreach (Jogador jogador in _jogadores)
            {
                Console.WriteLine($"Monte de {jogador.Nome}\n" + jogador.OlharTopoDoMonte() + (jogador.Monte.Count > 0 ? $" + {jogador.Monte.Count} cartas" : " 0 cartas\n\n"));
            }
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
