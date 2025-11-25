using System;

namespace Rouba_Monte.Classes
{
    public class Jogador
    {
        private string _nome;
        private int _posicao;
        private int _quantidadeCartas;
        private Queue<int> _posicoes;

        public string Nome { get => _nome; set => _nome = value; }
        public int Posicao { get => _posicao; set => _posicao = value; }
        public int QuantidadeCartas { get => _quantidadeCartas; set => _quantidadeCartas = value; }
        public Queue<int> Posicoes { get => _posicoes; set => _posicoes = value; }

        // INFO: atributo para gestão do jogo
        public Stack<Carta> Monte { get; set; }

        public Jogador(string nome)
        {
            _nome = nome;
            _quantidadeCartas = 0;
            _posicoes = new Queue<int>();
            _posicao = -1;
            Monte = new Stack<Carta>();
        }

        public Jogador(
            string nome,
            int posicao,
            int quantidadeCartas,
            int[] posicoes
        )
        { 
            _nome = nome;
            _posicao = posicao;
            _quantidadeCartas = quantidadeCartas;
            _posicoes = new Queue<int>();
            foreach (int pos in posicoes)
            {
                _posicoes.Enqueue(pos);
            }
            Monte = new Stack<Carta>();
        }

        public Carta? OlharTopoDoMonte()
        {
            if(Monte.Count > 0)
            {
                return Monte.Peek();
            }
            return null;
        }

        internal void RoubarMonte(Jogador jogador)
        {
            Stack<Carta> aux = new Stack<Carta>();
            while(jogador.Monte.Count > 0) {
                aux.Push(jogador.Monte.Pop());
            }
            while (aux.Count > 0)
            {
                this.Monte.Push(aux.Pop());
            }
        }
    }
}
