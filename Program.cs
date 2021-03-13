using System;
using System.Collections.Generic;
using System.Globalization;

namespace bank.transfer
{
    class Program
    {
        static List<Conta> listaContas = new List<Conta>();

        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario != "X"){
                switch(opcaoUsuario){
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        AbrirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Informe um dos itens do menu.");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Sistema finalizado.");
            Console.WriteLine();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();

            Console.WriteLine("SISTEMA BANCÁRIO");
            Console.WriteLine("Selecione um serviço:");

            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Abrir conta");
            Console.WriteLine("3 - Transferências");
            Console.WriteLine("4 - Saques");
            Console.WriteLine("5 - Depósitos");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");

            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            
            return opcaoUsuario;
        }

        private static void ListarContas()
        {
            Console.WriteLine("Lista - Contas");
            Console.WriteLine();

            if(listaContas.Count == 0){
                Console.WriteLine("Nenhuma conta cadastrada.");
                
                return;
            }

            for(int i = 0; i < listaContas.Count; i++){
                Conta conta = listaContas[i];

                Console.Write("#{0} - ", i);
                Console.WriteLine(conta);
            }
        }

        private static void AbrirConta()
        {
            string entrada;

            Console.WriteLine("Abertura de Conta");
            Console.WriteLine();

            int tipoConta;
            do{
                Console.Write("Tipo de Conta (1-Física | 2-Jurídica): ");
                entrada = Console.ReadLine();
            }
            while((!int.TryParse(entrada, out tipoConta)) || (!Enum.IsDefined(typeof(TipoConta), tipoConta)));
            
            string nome;
            do{
                Console.Write("Nome do Cliente: ");
                nome = Console.ReadLine();
            }
            while(nome == "");

            double saldoInicial;
            do{
                Console.Write("Saldo Inicial: ");
                entrada = Console.ReadLine().Replace(",", ".");
            }
            while(!double.TryParse(entrada, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out saldoInicial));

            double credito;
            do{
                Console.Write("Crédito: ");
                entrada = Console.ReadLine().Replace(",", ".");
            }
            while((!double.TryParse(entrada, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out credito)) || (credito < 0));

            Conta novaConta = new Conta(tipoConta: (TipoConta)tipoConta,
                                        saldo: saldoInicial,
                                        credito: credito,
                                        nome: nome);

            listaContas.Add(novaConta);
        }

        private static void Transferir()
        {
            string entrada;

            Console.WriteLine("Transferências");
            Console.WriteLine();

            int contaOrigem;
            do{
                Console.Write("Número da conta de Origem: ");
                entrada = Console.ReadLine();
            }
            while(!int.TryParse(entrada, out contaOrigem));

            if(!((contaOrigem >= 0) && (contaOrigem < listaContas.Count))){
                Console.WriteLine();
                Console.WriteLine("Conta de origem não encontrada.");

                return; 
            }

            int contaDestino;
            do{
                Console.Write("Número da conta de Destino: ");
                entrada = Console.ReadLine();
            }
            while(!int.TryParse(entrada, out contaDestino));

            if(!((contaDestino >= 0) && (contaDestino < listaContas.Count))){
                Console.WriteLine();
                Console.WriteLine("Conta de destino não encontrada.");

                return; 
            }

            double valorTransferencia;
            do{
                Console.Write("Valor da transferência: ");
                entrada = Console.ReadLine().Replace(",", ".");
            }
            while(!double.TryParse(entrada, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valorTransferencia));

            if(valorTransferencia <= 0){
                Console.WriteLine();
                Console.WriteLine("Valor de transferência inválido.");

                return;
            }

            Console.WriteLine();

            listaContas[contaOrigem].Transferir(valorTransferencia, listaContas[contaDestino]);
        }

        private static void Sacar()
        {
            string entrada;

            Console.WriteLine("Saques");
            Console.WriteLine();

            int numeroConta;
            do{
                Console.Write("Número da conta: ");
                entrada = Console.ReadLine();
            }
            while(!int.TryParse(entrada, out numeroConta));

            if(!((numeroConta >= 0) && (numeroConta < listaContas.Count))){
                Console.WriteLine();
                Console.WriteLine("Conta não encontrada.");

                return; 
            }

            double valorSaque;
            do{
                Console.Write("Valor do saque: ");
                entrada = Console.ReadLine().Replace(",", ".");
            }
            while(!double.TryParse(entrada, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valorSaque));

            if(valorSaque <= 0){
                Console.WriteLine();
                Console.WriteLine("Valor de saque inválido.");

                return;
            }

            Console.WriteLine();

            listaContas[numeroConta].Sacar(valorSaque);
        }

        private static void Depositar()
        {
            string entrada;

            Console.WriteLine("Depósitos");
            Console.WriteLine();

            int numeroConta;
            do{
                Console.Write("Número da conta: ");
                entrada = Console.ReadLine();
            }
            while(!int.TryParse(entrada, out numeroConta));

            if(!((numeroConta >= 0) && (numeroConta < listaContas.Count))){
                Console.WriteLine();
                Console.WriteLine("Conta não encontrada.");

                return; 
            }

            double valorDeposito;
            do{
                Console.Write("Valor do depósito: ");
                entrada = Console.ReadLine().Replace(",", ".");
            }
            while(!double.TryParse(entrada, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out valorDeposito));

            if(valorDeposito <= 0){
                Console.WriteLine();
                Console.WriteLine("Valor de depósito inválido.");

                return;
            }

            Console.WriteLine();

            listaContas[numeroConta].Depositar(valorDeposito);
        }
    }
}
