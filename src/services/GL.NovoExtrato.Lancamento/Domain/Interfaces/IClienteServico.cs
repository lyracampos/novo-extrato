using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Entities;
namespace GL.NovoExtrato.Lancamento.Domain.Interfaces
{
    public interface IClienteServico
    {
        Task SalvarAsync(Cliente cliente);
        Task<Cliente> AdicionarAsync();
    }
}