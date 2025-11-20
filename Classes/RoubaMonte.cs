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

        public RoubaMonte(List<Jogador> jogadores, int numCartas, List<Carta> cartas)
        {
            // TODO: Implementar uma lista duplamente encadeada para gerenciar as rodadas
            _jogadores = new LinkedList<Jogador>(jogadores);
            Jogadores = _jogadores;

            _cartas = new List<Carta>();

            int numBaralhos = (int)Math.Ceiling(54.0 / numCartas);

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
            if (Finalizado)
            {
                return;
            }

            CartaDaVez = MonteDeCompra.Pop();
            JogadorAtual = ObjerJogadorAtual();
            // TODO: Implementar jogada baseada no Jogador

            foreach (Jogador jogador in Jogadores)
            {
                if(jogador != JogadorAtual)
                {
                    if (CartaDaVez.CompararCartas(jogador.OlharTopoDoMonte()))
                    {
                        // TODO: Implementar ação para roubar o monte
                        // TODO: Checar se tem mais de um monte
                    }
                }
            }

            foreach (Carta carta in AreaDeDescarte)
            {
                if (carta.CompararCartas(CartaDaVez))
                {
                    // TODO: Implementar ação para remover
                    // TODO: Checar se tem mais de um monte
                }
            }

            if (CartaDaVez.CompararCartas(JogadorAtual.OlharTopoDoMonte()))
            {
                // TODO: Implementar ação 
            }

            Finalizado = MonteDeCompra.Count == 0;
        }

        private Jogador ObjerJogadorAtual()
        {
            // TODO: Implementar corretamente
            return Jogadores.First();
        }
    }
}
