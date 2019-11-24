using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Consulta.Domain.Entities;
using GL.NovoExtrato.Consulta.Models;
using GL.NovoExtrato.Shared.Entities;
namespace GL.NovoExtrato.Consulta.Domain.Interfaces
{
    public interface IExtratoServico
    {
         Task<Resultado> AdicionarAsync(ExtratoModel model);
         Task<IEnumerable<ExtratoModel>> ObterPorClienteAsync(int clienteId);
         Task RemoverAsync(int id);
    }
}