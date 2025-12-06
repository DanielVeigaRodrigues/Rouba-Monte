using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rouba_Monte.Classes;
public static class Log
{

    private static StreamWriter? _writer;

    public static void Iniciar(string nomeArquivo)
    {
        _writer = new StreamWriter(nomeArquivo, append: true);
        _writer.AutoFlush = true;
        Escrever($"Log iniciado em {DateTime.Now}");
        Escrever("---------------------------------------------");
    }


    public static void Encerrar()
    {
        Escrever("---------------------------------------------");
        Escrever($"Log finalizado em {DateTime.Now}");
        _writer?.Close();
        _writer = null;
    }

    public static void Escrever(string mensagem)
    {
        if (_writer != null)
        {
            _writer.WriteLine(mensagem);
        }
    }

}
