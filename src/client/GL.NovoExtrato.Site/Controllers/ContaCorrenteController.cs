using System.Diagnostics;
using System.Threading.Tasks;
using GL.NovoExtrato.Site.Models;
using GL.NovoExtrato.Site.Services;
using Microsoft.AspNetCore.Mvc;
namespace GL.NovoExtrato.Site.Controllers
{
    public class ContaCorrenteController : Controller
    {
        private readonly IContaCorrenteServicoApi _serviceApi;

        public ContaCorrenteController(IContaCorrenteServicoApi serviceApi)
        {
            _serviceApi = serviceApi;
        }

        public async Task<IActionResult> Index()
        {
            var contas = await _serviceApi.ObterContas();
            var model = new ContaCorrenteListaModel(contas);
            return View(model);
        }

        public IActionResult CriarClientes()
        {
            return View(new CriarClientesModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarClientes(CriarClientesModel model)
        {
            if (ModelState.IsValid)
            {
                for (var i = 0; i < model.Quantidade; i++)
                {
                    await _serviceApi.CriarClienteAsync();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult CriarLancamento()
        {
            return View(new CriarLancamentoModel());
        }

        [HttpPost]
        public async Task<IActionResult> CriarLancamento(CriarLancamentoModel model)
        {
            if (ModelState.IsValid)
            {
                await _serviceApi.CriarLancamento(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            if (HttpContext.Response.StatusCode == 500)
            {
                error.Error500();
            }
            return View(error);
        }
    }
}
