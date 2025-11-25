using System;

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
        public Carta? CartaDaVez { get; set; }
        public Jogador? JogadorAtual {  get; set; }
        public Stack<Carta> MonteDeCompra {  get; set; }
        public List<Carta> AreaDeDescarte { get; set; }
        public bool Finalizado { get; set; }
        #endregion

        public RoubaMonte(List<Jogador> jogadores, int numCartas)
        {
            // TODO: Implementar uma lista duplamente encadeada para gerenciar as rodadas
            _jogadores = new LinkedList<Jogador>(jogadores);
            Jogadores = _jogadores;

            _cartas = new List<Carta>();

            int numBaralhos = (int)Math.Ceiling(52.0 / numCartas);

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

        public void LerTecla()
        {
            ConsoleKeyInfo tecla = Console.ReadKey();
            switch (tecla.Key)
            {
                // IMPLEMENTAR 
            }
        }

        public void MostrarMenu()
        {
            if (TesteRoubarMontesJogador())
            {
                Console.ReadKey();
                RoubarMontesJogador();
            }
            else if (TesteRoubarMonteAreaDeDescarte())
            {
                Console.ReadKey();
                RoubarMonteAreaDeDescarte();
            } 
            else if (TesteRoubarCartaDaVez())
            {
                Console.ReadKey();
                RoubarCartaDaVez();
            }
        }

        public void Jogar()
        {
            if (Finalizado)
            {
                return;
            }

            CartaDaVez = MonteDeCompra.Pop();
            JogadorAtual = ObterProximoJogador();

            RoubarMontesJogador();
            RoubarMonteAreaDeDescarte();
            RoubarCartaDaVez();

            AreaDeDescarte.Add(CartaDaVez);
            JogadorAtual = ObterProximoJogador();

            Finalizado = MonteDeCompra.Count == 0;
        }

        private bool TesteRoubarCartaDaVez()
        {
            if (CartaDaVez.CompararCartas(JogadorAtual.OlharTopoDoMonte()))
            {
                Console.WriteLine("A - Roubar carta da vez");
                return true;
            }
            return false;
        }

        private void RoubarCartaDaVez()
        {
            if (CartaDaVez.CompararCartas(JogadorAtual.OlharTopoDoMonte()))
            {
                JogadorAtual.Monte.Push(CartaDaVez);
                return;
            }
        }

        private bool TesteRoubarMonteAreaDeDescarte()
        {
            foreach (Carta carta in AreaDeDescarte)
            {
                if (carta.CompararCartas(CartaDaVez))
                {
                    Console.WriteLine("W - Roubar cartas do monte de descarte");
                    return true;
                }
            }
            return false;
        }

        private void RoubarMonteAreaDeDescarte()
        {
            foreach (Carta carta in AreaDeDescarte)
            {
                if (carta.CompararCartas(CartaDaVez))
                {
                    AreaDeDescarte.Remove(carta);
                    JogadorAtual.Monte.Push(carta);
                    JogadorAtual.Monte.Push(CartaDaVez);
                    return;
                }
            }
        }

        private void RoubarMontesJogador()
        {
            foreach (Jogador jogador in Jogadores)
            {
                if (jogador != JogadorAtual)
                {
                    if (CartaDaVez.CompararCartas(jogador.OlharTopoDoMonte()))
                    {
                        JogadorAtual.RoubarMonte(jogador);
                        return;
                    }
                }

            }
        }

        private bool TesteRoubarMontesJogador()
        {
            foreach (Jogador jogador in Jogadores)
            {
                if (jogador != JogadorAtual)
                {
                    if (CartaDaVez.CompararCartas(jogador.OlharTopoDoMonte()))
                    {
                        Console.WriteLine($"D - Roubar monte de {jogador.Nome}");
                        return true;
                    }
                }

            }
            return false;
        }

        public void Imprimir()
        {
            // TODO: Imprimir as cartas e os montes
        }

        private Jogador ObterProximoJogador()
        {
            // TODO: Implementar corretamente
            return Jogadores.First();
        }

    }
}
