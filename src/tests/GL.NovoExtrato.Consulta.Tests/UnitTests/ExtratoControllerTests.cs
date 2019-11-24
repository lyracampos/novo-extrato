using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Consulta.Controllers;
using GL.NovoExtrato.Consulta.Domain.Entities;
using GL.NovoExtrato.Consulta.Domain.Interfaces;
using GL.NovoExtrato.Consulta.Models;
using GL.NovoExtrato.Shared.Entities;
using GL.NovoExtrato.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GL.NovoExtrato.Consulta.Tests.UnitTests
{
    public class ExtratoControllerTests
    {
        private readonly Mock<IExtratoServico> _servico;
        private readonly ExtratoController _extratoController;

        public ExtratoControllerTests()
        {
            _servico = new Mock<IExtratoServico>();
            _extratoController = new ExtratoController(_servico.Object);
        }

        [Fact(DisplayName = "UT. ExtratoController Get - deve retornar extrato de um cliente")]
        public async Task DeveRetornarExtratoDoCliente_Quando_HouverNoBanco()
        {
            //arrange
            var extratos = new List<ExtratoModel>
            {
                new ExtratoModel(123, 1, DateTime.UtcNow, LancamentoTipo.TED, 1500, "TED", "AXS", "Cliente 1", "ASD", "Cliente 2", 3000, TransacaoTipo.Debito)
            };
            _servico.Setup(p => p.ObterPorClienteAsync(1)).ReturnsAsync(extratos);

            //act
            var resultado = await _extratoController.Get(1);

            //assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact(DisplayName = "UT. ExtratoController Post (novo) - deve retornar status ok quando gerar novo extrato")]
        public async Task DeveRetornarOk_Quando_CriarLancamentoComSucesso()
        {
            //arrange
            var extrato = new ExtratoModel(123, 1, DateTime.UtcNow, LancamentoTipo.TED, 1500, "TED", "AXS", "Cliente 1", "ASD", "Cliente 2", 3000, TransacaoTipo.Debito);
            _servico.Setup(p => p.AdicionarAsync(extrato)).ReturnsAsync(new Resultado(true, "sucesso"));

            //act
            var resultado = await _extratoController.Post(extrato);

            //assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact(DisplayName = "UT. ExtratoController Post (novo) - deve retornar status badrequest quando o request for inválido")]
        public async Task DeveRetornarNotFound_Quando_NaoEncontrarContasNoBanco()
        {
            //arrange
            var extrato = new ExtratoModel(123, 1, DateTime.UtcNow, LancamentoTipo.TED, 1500, "", "AXS", "Cliente 1", "ASD", "Cliente 2", 3000, TransacaoTipo.Debito);
            _extratoController.ModelState.AddModelError("Error", "modelo inválido");

            //act
            var resultado = await _extratoController.Post(extrato);

            //assert
            Assert.IsType<BadRequestResult>(resultado);
        }

        [Fact(DisplayName = "UT. ExtratoController Post (novo) - deve retornar status badrequest quando houver quebra de regra")]
        public async Task DeveRetornarBadRequest_Quando_NaoCriarLancamento()
        {
            //arrange
            var extrato = new ExtratoModel(123, 1, DateTime.UtcNow, LancamentoTipo.TED, 1500, "", "AXS", "Cliente 1", "ASD", "Cliente 2", 3000, TransacaoTipo.Debito);
            _servico.Setup(p => p.AdicionarAsync(extrato)).ReturnsAsync(new Resultado(false, "falha"));

            //act
            var resultado = await _extratoController.Post(extrato);

            //assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }
    }
}
