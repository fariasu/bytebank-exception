using System;

namespace ByteBank
{
    public class ContaCorrente
    {
        public Cliente Titular { get; set; }

        public static int TotalDeContasCriadas { get; private set; }
        public static int SaquesNaoPermitidos { get; private set; }
        public static int TransferenciasNaoPermitidas { get; private set; }


        public int Agencia { get; }
        public int Numero { get; }

        private double _saldo = 100;

        public double Saldo
        {
            get
            {
                return _saldo;
            }
            private set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }


        public ContaCorrente(int agencia, int numero)
        {
            Agencia = agencia;
            Numero = numero;

            if(agencia <= 0)
            {
                throw new ArgumentException("A agência não pode ser 0!", nameof(agencia));
            }
            if (numero <= 0) {
                throw new ArgumentException("O número não pode ser 0!", nameof(numero));

            }

            TotalDeContasCriadas++;
        }


        public void Sacar(double valor)
        {
            if (_saldo < valor)
            {
                SaquesNaoPermitidos++;
                throw new SaldoInsuficienteException($"Saldo insuficiente. Não foi possível realizar o saque de R${valor}.");
            }

            _saldo -= valor;
            
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
        }


        public void Transferir(double valor, ContaCorrente contaDestino)
        {
            try
            {
                Sacar(valor);
                contaDestino.Depositar(valor);
            }
            catch(SaldoInsuficienteException ex)
            {
                TransferenciasNaoPermitidas++;
                throw new OperacaoFinanceiraException($"Não foi possível realizar a transferência de R${valor}.", ex);
            }

            
        }
    }
}
