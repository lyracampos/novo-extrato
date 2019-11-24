using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Site.Models;

namespace GL.NovoExtrato.Site.Services
{
    public interface IConsultaServicoApi
    {
        Task<IEnumerable<ExtratoModel>> ObterAsync(int clienteId);
    }
}
