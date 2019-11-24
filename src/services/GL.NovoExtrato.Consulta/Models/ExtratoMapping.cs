using GL.NovoExtrato.Consulta.Domain.Entities;

namespace GL.NovoExtrato.Consulta.Models
{
    public static class ExtratoMapping
    {
        public static Extrato ModeloParaEntidade(ExtratoModel model)
        {
            return new Extrato(model.ClienteId, model.CriadoEm, model.Tipo, model.ValorTransacao, model.Descricao, 
                                model.ContaOrigem, model.NomeClienteOrigem, 
                                model.ContaDestino, model.NomeClienteDestino, model.Saldo, model.TransacaoTipo);
        }

        public static ExtratoModel EntidadeParaModelo(Extrato extrato)
        {
            return new ExtratoModel(extrato.Id, extrato.ClienteId, extrato.CriadoEm, extrato.Tipo, extrato.ValorTransacao, extrato.Descricao, 
                                extrato.ContaOrigem, extrato.NomeClienteOrigem, 
                                extrato.ContaDestino, extrato.NomeClienteDestino, extrato.Saldo, extrato.TransacaoTipo);
        }
    }
}