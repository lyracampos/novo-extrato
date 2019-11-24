using System;
using GL.NovoExtrato.Shared.Enums;
using GL.NovoExtrato.Shared.Events;
namespace GL.NovoExtrato.Lancamento.Events
{
    public class LancamentoCriado : IEvent
    {
        public LancamentoCriado(
            int clienteId,
            LancamentoTipo tipo,
            decimal valorTransacao,
            string contaOrigem,
            string nomeClienteOrigem,
            string contaDestino,
            string nomeClienteDestino,
            decimal saldo,
            TransacaoTipo transacaoTipo
            )
        {
            ClienteId = clienteId;
            CriadoEm = DateTime.UtcNow;
            Tipo = tipo;
            ValorTransacao = valorTransacao;
            SetDescricao();
            ContaOrigem = contaOrigem;
            NomeClienteOrigem = nomeClienteOrigem;
            ContaDestino = contaDestino;
            NomeClienteDestino = nomeClienteDestino;
            Saldo = saldo;
            TransacaoTipo = transacaoTipo;
        }
        public int ClienteId { get; }
        public DateTime CriadoEm { get; }
        public LancamentoTipo Tipo { get; }
        public decimal ValorTransacao { get; }
        public string Descricao { get; private set; }
        public string ContaOrigem { get; set; }
        public string NomeClienteOrigem { get; }
        public string ContaDestino { get; }
        public string NomeClienteDestino { get; }
        public decimal Saldo { get; }
        public TransacaoTipo TransacaoTipo { get; }

        private void SetDescricao()
        {
            switch (Tipo)
            {
                case LancamentoTipo.DOC:
                    Descricao = "Transferência DOC";
                    break;
                case LancamentoTipo.TED:
                    Descricao = "Transferência TED";
                    break;
                case LancamentoTipo.Transferencia:
                    Descricao = "Transferência padrão";
                    break;
                case LancamentoTipo.DepositoInicial:
                    Descricao = "Depósito Inicial";
                    break;
            }
        }
    }
}