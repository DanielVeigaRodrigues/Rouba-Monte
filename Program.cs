using System;
using Rouba_Monte.Classes;

namespace Rouba_Monte
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: Implementar interação com usuário
            List<Jogador> jogadores = [new("Samuel"), new("Luisa")];
            RoubaMonte jogo = new RoubaMonte(jogadores, 52);

            jogo.Jogar();
            Console.WriteLine(jogo.CartaDaVez);
        }
    }
}
