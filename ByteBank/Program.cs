using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank
{
    class Program
    {
        static void Main(string[] args)
        {
            //Contas();
            Leitor();
            Console.ReadLine();
        }

        private static void Leitor()
        {
            LeitorDeArquivo leitor = new LeitorDeArquivo("contas.txt");

            try
            {
                leitor.LerProximaLinha();
                leitor.LerProximaLinha();
                leitor.LerProximaLinha();
            }
            catch(IOException ex)
            {
                Console.WriteLine($"IOException capturada e tratada.");
            }
            finally
            {
                leitor.Fechar();
            }
        }

        private static void Contas()
        {
            try
            {
                ContaCorrente conta = new ContaCorrente(1, 1);
                ContaCorrente conta2 = new ContaCorrente(2, 2);

                conta.Depositar(50);
                Console.WriteLine($"Saldo inicial conta 1: {conta.Saldo}");
                Console.WriteLine($"Saldo inicial conta 2: {conta2.Saldo}");

                conta.Transferir(522, conta2);
                Console.WriteLine($"Saldo final conta 1: {conta.Saldo}");
                Console.WriteLine($"Saldo final conta 2: {conta2.Saldo}");
            }
            catch (OperacaoFinanceiraException ex)
            {
                Console.WriteLine($"Erro no parâmetro {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                Console.WriteLine(ex.InnerException.Message);
                Console.WriteLine(ex.InnerException.StackTrace);
            }
        }
    }
}
