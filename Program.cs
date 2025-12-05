using System;
using Rouba_Monte.Classes;

namespace Rouba_Monte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Jogador> jogadores = [new("Samuel"), new("Rebeca"), new("Daniel"), new("Ana Paula"), new("Jorge"), new("Luiz")];
            RoubaMonte jogo = new RoubaMonte(jogadores, 5 * 52);

            while (!jogo.Finalizado)
            {
                jogo.Jogar();
            }
        }
    }
}
