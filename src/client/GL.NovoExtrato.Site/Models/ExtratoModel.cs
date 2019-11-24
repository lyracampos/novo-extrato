using System;
namespace GL.NovoExtrato.Site.Models
{
    public class ExtratoModel
    {
        public int ClienteId { get; set; }
        public DateTime CriadoEm { get; set; }
        public int Tipo { get; set; }
        public string TipoDescricao
        {
            get
            {
                if (Tipo == 1)
                    return "Transferência";
                else if (Tipo == 2)
                    return "TED";
                else if (Tipo == 4)
                    return "DOC";
                else if (Tipo == 8)
                    return "Depósito";
                return "Desconhecido";
            }
        }
        public decimal ValorTransacao { get; set; }
        public string Descricao { get; set; }
        public string ContaOrigem { get; set; }
        public string NomeClienteOrigem { get; set; }
        public string ContaDestino { get; set; }
        public string NomeClienteDestino { get; set; }
        public decimal Saldo { get; set; }
        public int TransacaoTipo { get; set; }
        public string TransacaoTipoDescricao
        {
            get
            {
                if (TransacaoTipo == 1)
                    return "Credito";
                else if (TransacaoTipo == 2)
                    return "Debito";
                else
                    return "Desconhecido";
            }
        }
    }
}
