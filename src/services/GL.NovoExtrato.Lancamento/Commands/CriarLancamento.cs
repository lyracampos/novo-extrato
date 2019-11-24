using System.ComponentModel.DataAnnotations;
using GL.NovoExtrato.Shared.Commands;
using GL.NovoExtrato.Shared.Enums;

namespace GL.NovoExtrato.Lancamento.Commands
{
    public class CriarLancamento : ICommand
    {
        public CriarLancamento(string numeroContaOrigem, string numeroContaDestino, LancamentoTipo tipo, decimal valor)
        {
            NumeroContaOrigem = numeroContaOrigem;
            NumeroContaDestino = numeroContaDestino;
            Tipo = tipo;
            Valor = valor;
        }
        [Required(ErrorMessage="Conta origem é obrigatório")]
        public string NumeroContaOrigem  { get; private set; }
        [Required(ErrorMessage="Conta destino é obrigatório")]
        public string NumeroContaDestino { get; private set; }
        [Required(ErrorMessage="Tipo do lançamento é obrigatório")]
        public LancamentoTipo Tipo { get; private set; }
        [Required(ErrorMessage="Valor é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor de transferência deve ser maior que 0")]
        public decimal Valor { get; private set; }

        public bool SolicitacaoDeTransferencia()
        {
            if (Tipo == LancamentoTipo.DOC ||
                Tipo == LancamentoTipo.TED ||
                Tipo == LancamentoTipo.Transferencia)
                return true;
            return false;
        }
    }
}