namespace GL.NovoExtrato.Site.Models
{
    public class ContaCorrenteModel
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string NumeroDaConta { get; set; }
        public decimal SaldoEmConta { get; set; }
    }
}
