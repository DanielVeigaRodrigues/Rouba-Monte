using System;
using Rouba_Monte.Classes;

namespace Rouba_Monte
{
    internal class Program
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Rouba-Montes\n" +
                                "1. Novo Jogo\n"+
                                "2. Verificar histórico de jogador\n"+
                                "3. Sair\n");
        }



        static void Main(string[] args)
        {
            int option = 0;
            List<Jogador> jogadoresCadastrados = new List<Jogador>();

            while (option != 3)
            {
                PrintMenu();
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Quantos jogadores irão jogar?");
                        int numJogadores = int.Parse(Console.ReadLine());
                        List<Jogador> jogadores = new List<Jogador>(); 

                        for (int i = 0; i < numJogadores; i++)
                        {
                            Console.WriteLine($"Insira o nome do jogador {i+1}");
                            string nome = Console.ReadLine();
                            Jogador jogador = new Jogador(nome);

                            if (jogadoresCadastrados.Find(j => j.Nome == jogador.Nome) != null)
                            {
                                jogador = jogadoresCadastrados.Find(p => p.Nome == nome);
                            } else
                            {
                                jogadoresCadastrados.Add(jogador);
                            };

                            jogadores.Add(jogador);
                        }

                        Console.WriteLine("Quantas cartas irão usar?");
                        int numCartas = int.Parse(Console.ReadLine());
                        Console.WriteLine("Quem será o primeiro jogador?");
                        string primeiro = Console.ReadLine();

                        RoubaMonte jogo = new RoubaMonte(jogadores, numCartas, primeiro);

                        while (!jogo.Finalizado)
                        {
                            jogo.Jogar();
                        }

                        break;
                    case 2:
                        Console.WriteLine("Digite o nome do jogador que deseja consultar:");
                        string nomeJogador = Console.ReadLine();
                        Jogador jogadorCadastrado = new Jogador(nomeJogador);

                        if (jogadoresCadastrados.Find(j => j.Nome == jogadorCadastrado.Nome) != null)
                        {
                            jogadorCadastrado = jogadoresCadastrados.Find(p => p.Nome == nomeJogador);
                            Console.WriteLine(jogadorCadastrado);
                        } else
                        {
                            Console.WriteLine("Este jogador não possui cadastro!");
                        }
                        break;
                    default:
                        break;
                }
            }
        }


    }
}
