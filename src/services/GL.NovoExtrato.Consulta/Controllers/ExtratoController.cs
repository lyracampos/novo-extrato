using System.Threading.Tasks;
using GL.NovoExtrato.Consulta.Domain.Interfaces;
using GL.NovoExtrato.Consulta.Models;
using Microsoft.AspNetCore.Mvc;
namespace GL.NovoExtrato.Consulta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ExtratoController : ControllerBase
    {
        private readonly IExtratoServico _servico;

        public ExtratoController(IExtratoServico servico)
        {
            _servico = servico;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var extrato = await _servico.ObterPorClienteAsync(id);
            return Ok(extrato);
        }
        [HttpPost("novo")]
        public async Task<IActionResult> Post([FromBody] ExtratoModel model)
        {
            if(ModelState.IsValid)
            {
                var resultado = await _servico.AdicionarAsync(model);
                if(resultado.Sucesso)
                {
                    return Ok(resultado);
                }
                return BadRequest(resultado.Mensagem);
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                await _servico.RemoverAsync(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
