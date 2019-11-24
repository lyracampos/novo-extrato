using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Entities;

namespace GL.NovoExtrato.Lancamento.Domain.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<Cliente> ObterPorIdAsync(int clienteId);
        Task<Cliente> ObterPorContaAsync(string numeroDaConta);
        Task<Cliente> AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
    }
}