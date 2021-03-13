using System;

namespace bank.transfer
{
    public class Conta
    {
        private TipoConta tipoConta { get; set; }
        private double saldo { get; set; }
        private double credito { get; set; }
        private string nome { get; set; }

        public Conta(TipoConta tipoConta, double saldo, double credito, string nome)
        {
            this.tipoConta = tipoConta;
            this.saldo = saldo;
            this.credito = credito;
            this.nome = nome;
        }

        public bool Sacar(double valorSaque)
        {
            //Verifica se possui saldo suficiente
            if((this.saldo - valorSaque) < (this.credito * -1)){
                Console.WriteLine("Saldo insuficiente para saque.");

                return false;
            }

            this.saldo -= valorSaque;

            Console.WriteLine("Cliente: {0} | Saldo: {1:0.00}", this.nome, this.saldo);

            return true;
        }

        public void Depositar(double valorDeposito)
        {
            this.saldo += valorDeposito;

            Console.WriteLine("Cliente: {0} | Saldo: {1:0.00}", this.nome, this.saldo);
        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if(this.Sacar(valorTransferencia)){
                contaDestino.Depositar(valorTransferencia);
            }
        }

        public override string ToString()
        {
            string retorno = "";

            retorno += "Tipo Conta: " + this.tipoConta + " | ";
            retorno += "Nome: " + this.nome + " | ";
            retorno += "Saldo (R$): " + string.Format("{0:0.00}", this.saldo) + " | ";
            retorno += "Crédito (R$): " + string.Format("{0:0.00}",this.credito);

            return retorno;
        }
    }
}