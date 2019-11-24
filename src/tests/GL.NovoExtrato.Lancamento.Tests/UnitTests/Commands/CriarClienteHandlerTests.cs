using System;
using System.Threading;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Commands;
using GL.NovoExtrato.Lancamento.Domain.Entities;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using GL.NovoExtrato.Lancamento.Events;
using GL.NovoExtrato.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GL.NovoExtrato.Lancamento.Tests.UnitTests.Commands
{
    public class CriarClienteHandlerTests
    {
        private CancellationToken _cancellationToken = CancellationToken.None;
        private readonly Mock<ILogger<CriarClienteHandler>> _logger;
        private readonly Mock<IClienteServico> _clienteServico;
        private readonly Mock<IMediator> _bus;
        private readonly CriarClienteHandler _handler;

        public CriarClienteHandlerTests()
        {
            _logger = new Mock<ILogger<CriarClienteHandler>>();
            _clienteServico = new Mock<IClienteServico>();
            _bus = new Mock<IMediator>();
            _handler = new CriarClienteHandler(_logger.Object, _clienteServico.Object, _bus.Object);
        }

        [Fact(DisplayName = "UT. CriarClienteHandler Handler - retorna sucesso quando cria cliente e conta corrente.")]
        public async Task DeveRetornarSucesso_Quando_ClienteCriado()
        {
            //given
            var cliente = new Cliente();
            _clienteServico.Setup(p => p.AdicionarAsync()).ReturnsAsync(cliente);

            var lancamentoCriado = new LancamentoCriado(cliente.Id, LancamentoTipo.DepositoInicial, cliente.ContaCorrente.Saldo,
                                        cliente.ContaCorrente.Numero, cliente.Nome, cliente.ContaCorrente.Numero,
                                        cliente.Nome, cliente.ContaCorrente.Saldo, TransacaoTipo.Credito);
            _bus
                .Setup(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>()))
                .Callback<LancamentoCriado, CancellationToken>((re, ca) => { lancamentoCriado = re; _cancellationToken = ca; });

            //when
            var resultado = await _handler.Handle(new CriarCliente(), _cancellationToken);

            //then
            Assert.True(resultado.Sucesso);
        }

        [Fact(DisplayName = "UT. CriarClienteHandler Handler - retorna erro quando criar o cliente mas não realizar o lançamento inicial.")]
        public async Task DeveRetornarErro_Quando_LancamentoInicialFalhar()
        {
            //arrange
            var cliente = new Cliente();
            _clienteServico.Setup(p => p.AdicionarAsync()).ReturnsAsync(cliente);

            var lancamentoCriado = new LancamentoCriado(cliente.Id, LancamentoTipo.DepositoInicial, cliente.ContaCorrente.Saldo,
                                        cliente.ContaCorrente.Numero, cliente.Nome, cliente.ContaCorrente.Numero,
                                        cliente.Nome, cliente.ContaCorrente.Saldo, TransacaoTipo.Credito);

            _bus.Setup(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //act
            var resultado = await _handler.Handle(new CriarCliente(), _cancellationToken);

            //assert
            Assert.False(resultado.Sucesso);
        }
    }
}
