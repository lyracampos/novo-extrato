using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Controllers;
using GL.NovoExtrato.Lancamento.Domain.Entities;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using MediatR;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using GL.NovoExtrato.Lancamento.Commands;
using GL.NovoExtrato.Shared.Entities;
using System.Threading;
using GL.NovoExtrato.Shared.Enums;

namespace GL.NovoExtrato.Lancamento.Tests.UnitTests.Controllers
{
    public class ContaCorrenteControllerTests
    {
        private CancellationToken _cancellationToken = CancellationToken.None;
        private readonly Mock<IMediator> _bus;
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;
        private readonly ContaCorrenteController _controller;

        public ContaCorrenteControllerTests()
        {
            _bus = new Mock<IMediator>();
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _controller = new ContaCorrenteController(_bus.Object, _clienteRepositorio.Object);
        }

        [Fact(DisplayName = "UT. ContaCorrenteController Get - deve retornar uma lista de clientes e contas correntes")]
        public async Task DeveRetornarListaDeClientes_Quando_HouverClientesNaBase()
        {
            //arrange
            var listaDeClientes = new List<Cliente>
            {
                new Cliente(),
                new Cliente(),
                new Cliente()
            };
            _clienteRepositorio.Setup(p => p.ObterTodosAsync()).ReturnsAsync(listaDeClientes);

            //act
            var resultado = await _controller.Get();

            //assert
            Assert.IsType<OkObjectResult>(resultado);
            _clienteRepositorio.Verify(p => p.ObterTodosAsync(), Times.Once);
        }

        [Fact(DisplayName = "UT. ContaCorrenteController Post (novo-cliente) - deve retornar status ok quando criar cliente")]
        public async Task DeveRetornarOk_Quando_CriarClienteComSucesso()
        {
            //arrange
            var command = new CriarCliente();
            var response = new Resultado(true, "Cliente criado com sucesso.");
            _bus.Setup(p => p.Send(command, _cancellationToken)).ReturnsAsync(new Resultado(true, "Cliente criado com sucesso."));

            //act
            var resultado = await _controller.Post(command, _cancellationToken);

            //assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact(DisplayName = "UT. ContaCorrenteController Post  (novo-cliente) - deve retornar status bad request quando não criar cliente")]
        public async Task DeveRetornarBadRequest_Quando_CriarClienteComSucesso()
        {
            //arrange
            var command = new CriarCliente();
            _bus.Setup(p => p.Send(command, _cancellationToken)).ReturnsAsync(new Resultado(false, "Ocorreu um erro."));

            //act
            var resultado = await _controller.Post(command, _cancellationToken);

            //assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact(DisplayName = "UT. ContaCorrenteController Post (novo-lancamento) - deve retornar status ok quando gerar novo lançamento")]
        public async Task DeveRetornarOk_Quando_CriarLancamentoComSucesso()
        {
            //arrange
            var clienteOrigem = new Cliente();
            var clienteDestino = new Cliente();
            var model = new CriarLancamento(clienteOrigem.ContaCorrente.Numero, clienteDestino.ContaCorrente.Numero, LancamentoTipo.Transferencia, 1500);

            _bus.Setup(p => p.Send(model, _cancellationToken)).ReturnsAsync(new Resultado(true, "sucesso"));

            //act
            var resultado = await _controller.Post(model, _cancellationToken);

            //assert
            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact(DisplayName = "UT. ContaCorrenteController Post (novo-lancamento) - deve retornar status notfound quando não encontrar as contas no banco")]
        public async Task DeveRetornarNotFound_Quando_NaoEncontrarContasNoBanco()
        {
            //arrange
            var clienteOrigem = new Cliente();
            var clienteDestino = new Cliente();
            var model = new CriarLancamento(clienteOrigem.ContaCorrente.Numero, clienteDestino.ContaCorrente.Numero, LancamentoTipo.Transferencia, 1500);

            _bus.Setup(p => p.Send(model, _cancellationToken)).ReturnsAsync(new Resultado(false, "conta não encontrada"));

            //act
            var resultado = await _controller.Post(model, _cancellationToken);

            //assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact(DisplayName = "UT. ContaCorrenteController Post (novo-lancamento) - deve retornar status badrequest quando o request for inválido")]
        public async Task DeveRetornarBadRequest_Quando_NaoCriarLancamento()
        {
            //arrange
            var clienteOrigem = new Cliente();
            var clienteDestino = new Cliente();
            var model = new CriarLancamento(clienteOrigem.ContaCorrente.Numero, clienteDestino.ContaCorrente.Numero, LancamentoTipo.Transferencia, 1500);
            _bus.Setup(p => p.Send(model, _cancellationToken)).ReturnsAsync(new Resultado(false, "request inválido"));
            _controller.ModelState.AddModelError("Error", "Modelo inválido");

            //act
            var resultado = await _controller.Post(model, _cancellationToken);

            //assert
            Assert.IsType<BadRequestResult>(resultado);
        }
    }
}
