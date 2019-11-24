using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Site.Models;

namespace GL.NovoExtrato.Site.Services
{
    public interface IContaCorrenteServicoApi
    {
        Task CriarClienteAsync();
        Task<IEnumerable<ContaCorrenteModel>> ObterContas();
        Task CriarLancamento(CriarLancamentoModel model);
    }
}
