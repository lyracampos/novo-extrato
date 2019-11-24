using System.Linq;
using System.Threading.Tasks;
using GL.NovoExtrato.Site.Models;
using GL.NovoExtrato.Site.Services;
using Microsoft.AspNetCore.Mvc;
namespace GL.NovoExtrato.Site.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IConsultaServicoApi _serviceApi;

        public ConsultaController(IConsultaServicoApi serviceApi)
        {
            _serviceApi = serviceApi;
        }

        public async Task<IActionResult> Extrato(int id)
        {
            var extratos = await _serviceApi.ObterAsync(id);
            if (extratos != null)
                extratos = extratos.OrderByDescending(p => p.CriadoEm);
            var model = new ExtratoListaModel(extratos);
            return View(model);
        }
    }
}
