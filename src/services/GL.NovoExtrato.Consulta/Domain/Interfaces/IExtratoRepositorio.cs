using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Consulta.Domain.Entities;
namespace GL.NovoExtrato.Consulta.Domain.Interfaces
{
    public interface IExtratoRepositorio
    {
        Task<IEnumerable<Extrato>> ObterPorCliente(int clienteId);
        Task AdicionarAsync(Extrato extrato);
        Task RemoverAsync(int id);
    }
}