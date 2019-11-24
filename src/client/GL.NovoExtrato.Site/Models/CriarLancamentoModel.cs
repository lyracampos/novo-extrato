using System.ComponentModel.DataAnnotations;

namespace GL.NovoExtrato.Site.Models
{
    public class CriarLancamentoModel
    {
        [Required(ErrorMessage = "Conta origem é obrigatório")]
        public string NumeroContaOrigem { get; set; }
        [Required(ErrorMessage = "Conta destino é obrigatório")]
        public string NumeroContaDestino { get; set; }
        [Required(ErrorMessage = "Tipo do lançamento é obrigatório")]
        public int Tipo { get; set; }
        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor de transferência deve ser maior que 0")]
        public decimal Valor { get; set; }
    }
}
