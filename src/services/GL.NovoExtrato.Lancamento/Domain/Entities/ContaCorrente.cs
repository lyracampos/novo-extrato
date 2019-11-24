using System.Collections.Generic;

namespace GL.NovoExtrato.Lancamento.Domain.Entities
{
    public class ContaCorrente
    {
        protected ContaCorrente()
        {
        }
        public ContaCorrente(int clienteId, string numero, decimal saldo)
        {
            ClienteId = clienteId;
            Saldo = saldo;
            Numero = numero;
        }
        public int Id { get; private set; }
        public int ClienteId { get; private set; }
        public virtual Cliente Cliente { get; set; }
        public string Numero { get; private set; }
        public decimal Saldo { get; private set; }

        public void SetSaldo(decimal valor)
        {
            Saldo = valor;
        }
        public void AumentarSaldo(decimal valor)
        {
            if (valor == 0) return;
            Saldo += valor;
        }
        public void DiminuirSaldo(decimal valor)
        {
            if (valor == 0) return;
            Saldo -= valor;
        }
    }
}