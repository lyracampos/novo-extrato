using GL.NovoExtrato.Lancamento.Domain.Entities;
namespace GL.NovoExtrato.Lancamento.Models
{
    public class ClienteModel
    {
        public ClienteModel(Cliente cliente)
        {
            ClienteId = cliente.Id;
            ClienteNome = cliente.Nome;
            if (cliente.ContaCorrente != null)
            {
                NumeroDaConta = cliente.ContaCorrente.Numero;
                SaldoEmConta = cliente.ContaCorrente.Saldo;
            }
        }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string NumeroDaConta { get; set; }
        public decimal SaldoEmConta { get; set; }
    }
}
