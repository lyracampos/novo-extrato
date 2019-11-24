using GL.NovoExtrato.Lancamento.Domain.Entities;

namespace GL.NovoExtrato.Lancamento.Models
{
    public static class ClienteMapping
    {
        public static Cliente ModeloParaEntidade(ClienteModel model)
        {
            return new Cliente();
        }

        public static ClienteModel EntidadeParaModelo(Cliente cliente)
        {
            return new ClienteModel(cliente);
        }
    }
}