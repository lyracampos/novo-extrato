using System;
using GL.NovoExtrato.Shared.Enums;

namespace GL.NovoExtrato.Consulta.Domain.Entities
{
    public class Extrato
    {
        public Extrato(
            int clienteId,
            DateTime criadoEm, 
            LancamentoTipo tipo, 
            decimal valorTransacao, 
            string descricao, 
            string contaOrigem, 
            string nomeClienteOrigem, 
            string contaDestino, 
            string nomeClienteDestino,
            decimal saldo,
            TransacaoTipo transacaoTipo)
        {
            this.ClienteId = clienteId;
            this.CriadoEm = criadoEm;
            this.Tipo = tipo;
            this.ValorTransacao = valorTransacao;
            this.Descricao = descricao;
            this.ContaOrigem = contaOrigem;
            this.NomeClienteOrigem = nomeClienteOrigem;
            this.ContaDestino = contaDestino;
            this.NomeClienteDestino = nomeClienteDestino;
            Saldo = saldo;
            TransacaoTipo = transacaoTipo;
        }
        public int Id { get; private set; }
        public int ClienteId { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public LancamentoTipo Tipo { get; private set; }
        public decimal ValorTransacao { get; private set; }
        public string Descricao { get; private set; }
        public string ContaOrigem { get; private set; }
        public string NomeClienteOrigem { get; private set; }
        public string ContaDestino { get; private set; }
        public string NomeClienteDestino { get; private set; }
        public decimal Saldo { get; private set; }
        public TransacaoTipo TransacaoTipo { get; private set; }
    }
}