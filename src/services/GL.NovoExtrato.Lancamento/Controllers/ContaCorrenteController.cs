using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Commands;
using GL.NovoExtrato.Lancamento.Domain.Entities;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using GL.NovoExtrato.Lancamento.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace GL.NovoExtrato.Lancamento.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _bus;
        private readonly IClienteRepositorio _clienteRepositorio;
        public ContaCorrenteController(
            IMediator bus,
            IClienteRepositorio clienteRepositorio)
        {
            _bus = bus;
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _clienteRepositorio.ObterTodosAsync();
            return Ok(clientes.Select(c => new ClienteModel(c)));
        }

        [HttpPost]
        [Route("novo-cliente")]
        public async Task<IActionResult> Post([FromBody]  CriarCliente model, CancellationToken cancellationToken)
        {
            var resultado = await _bus.Send(model, cancellationToken);
            if (resultado.Sucesso)
                return Ok(resultado.Mensagem);
            return BadRequest(resultado.Mensagem);
        }

        [HttpPost]
        [Route("novo-lancamento")]
        public async Task<IActionResult> Post([FromBody]CriarLancamento model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _bus.Send(model, cancellationToken);
                if (resultado.Sucesso)
                    return Ok(resultado.Mensagem);

                return NotFound(resultado.Mensagem);
            }
            return BadRequest();
        }
    }
}