using System;
using System.Linq;

namespace GL.NovoExtrato.Lancamento.Domain.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            CodigoAleatorio = GerarCodigoAleatorio();
            GerarNome();
            CriarConta(1500.00m);
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public virtual ContaCorrente ContaCorrente { get; set; }
        public string CodigoAleatorio { get; }
        public void GerarNome()
        {
            Nome = $"Nome do Cliente {CodigoAleatorio}";
        }
        public void CriarConta(decimal saldo)
        {
            ContaCorrente = new ContaCorrente(Id, CodigoAleatorio, saldo);
        }

        private Random random = new Random();

        public string GerarCodigoAleatorio()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 4)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
