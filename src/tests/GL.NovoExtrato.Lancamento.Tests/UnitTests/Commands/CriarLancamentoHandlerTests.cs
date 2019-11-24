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
    public class CriarLancamentoHandlerTests
    {
        private CancellationToken _cancellationToken = CancellationToken.None;
        private readonly Mock<ILogger<CriarLancamentoHandler>> _logger;
        private readonly Mock<IClienteServico> _clienteServico;
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;
        private readonly Mock<IMediator> _bus;
        private readonly CriarLancamentoHandler _handler;
        private CriarLancamento _command;
        private Cliente _clienteOrigem;
        private Cliente _clienteDestino;
        private LancamentoCriado _lancamentoCriadoOrigem;
        private LancamentoCriado _lancamentoCriadoDestino;
        public CriarLancamentoHandlerTests()
        {
            _logger = new Mock<ILogger<CriarLancamentoHandler>>();
            _clienteServico = new Mock<IClienteServico>();
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _bus = new Mock<IMediator>();
            _handler = new CriarLancamentoHandler(_logger.Object, _clienteServico.Object, _clienteRepositorio.Object, _bus.Object);
        }

        [Fact(DisplayName = "UT. CriarLancamentoHandler Handler - retorna sucesso quando os lançamentos forem realizado com sucesso entre as contas.")]
        public async Task DeveRetornarSucesso_Quando_LancamentoForGerado()
        {
            //Given
            _clienteOrigem = new Cliente();
            _clienteDestino = new Cliente();
            _command = new CriarLancamento(_clienteOrigem.ContaCorrente.Numero, _clienteDestino.ContaCorrente.Numero, LancamentoTipo.Transferencia, 500);

            _clienteRepositorio.Setup(p => p.ObterPorContaAsync(_command.NumeroContaOrigem)).ReturnsAsync(_clienteOrigem);
            _clienteRepositorio.Setup(p => p.ObterPorContaAsync(_command.NumeroContaDestino)).ReturnsAsync(_clienteDestino);
            _clienteServico.Setup(p => p.SalvarAsync(_clienteOrigem));
            _clienteServico.Setup(p => p.SalvarAsync(_clienteDestino));

            _bus.Setup(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>()))
                .Callback<LancamentoCriado, CancellationToken>((re, ca) => { _lancamentoCriadoOrigem = re; _cancellationToken = ca; });

            _bus.Setup(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>()))
                .Callback<LancamentoCriado, CancellationToken>((re, ca) => { _lancamentoCriadoDestino = re; _cancellationToken = ca; });

            //When
            var resultado = await _handler.Handle(_command, _cancellationToken);

            //Then
            Assert.True(resultado.Sucesso);
            Assert.Equal("Lançamento gerado com sucesso.", resultado.Mensagem);
            decimal origemSaldoAtual = 1000;
            Assert.Equal(_clienteOrigem.ContaCorrente.Saldo, origemSaldoAtual);
            decimal destinoSaldoAtual = 2000;
            Assert.Equal(_clienteDestino.ContaCorrente.Saldo, destinoSaldoAtual);
            _clienteServico.Verify(p => p.SalvarAsync(It.IsAny<Cliente>()), Times.Exactly(2));
            _bus.Verify(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact(DisplayName = "UT. CriarLancamentoHandler Handler - retorna erro quando não encontrar conta origem.")]
        public async Task DeveRetornarErro_Quando_ContaOrigemNaoForEncontrada()
        {
            //Given
            _clienteDestino = new Cliente();
            _command = new CriarLancamento("ASX", _clienteDestino.ContaCorrente.Numero, LancamentoTipo.Transferencia, 500);
            _clienteRepositorio.Setup(p => p.ObterPorContaAsync(_command.NumeroContaOrigem)).ReturnsAsync(_clienteOrigem);

            //When
            var resultado = await _handler.Handle(_command, _cancellationToken);

            //Then
            Assert.False(resultado.Sucesso);
            Assert.Equal("Cliente de conta Origem não encontrado.", resultado.Mensagem);
            _clienteServico.Verify(p => p.SalvarAsync(It.IsAny<Cliente>()), Times.Never);
            _bus.Verify(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "UT. CriarLancamentoHandler Handler  - retorna erro quando não encontrar conta destino.")]
        public async Task DeveRetornarErro_Quando_ContaDestinoNaoForEncontrada()
        {
            //Given
            _clienteOrigem = new Cliente();
            _command = new CriarLancamento(_clienteOrigem.ContaCorrente.Numero, "XAS", LancamentoTipo.Transferencia, 500);
            _clienteRepositorio.Setup(p => p.ObterPorContaAsync(_command.NumeroContaDestino)).ReturnsAsync(_clienteDestino);

            //When
            var resultado = await _handler.Handle(_command, _cancellationToken);

            //Then
            Assert.False(resultado.Sucesso);
            Assert.Equal("Cliente de conta Origem não encontrado.", resultado.Mensagem);
            _clienteServico.Verify(p => p.SalvarAsync(It.IsAny<Cliente>()), Times.Never);
            _bus.Verify(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "UT. CriarLancamentoHandler Handler  - retorna erro quando comunicaçao entre os serviços falhar.")]
        public async Task DeveRetornarErro_Quando_FalharComunicacaoEntreServicos()
        {
            //Given
            _clienteOrigem = new Cliente();
            _clienteDestino = new Cliente();
            _command = new CriarLancamento(_clienteOrigem.ContaCorrente.Numero, _clienteDestino.ContaCorrente.Numero, LancamentoTipo.Transferencia, 500);

            _clienteRepositorio.Setup(p => p.ObterPorContaAsync(_command.NumeroContaOrigem)).ReturnsAsync(_clienteOrigem);
            _clienteRepositorio.Setup(p => p.ObterPorContaAsync(_command.NumeroContaDestino)).ReturnsAsync(_clienteDestino);
            _clienteServico.Setup(p => p.SalvarAsync(_clienteOrigem));
            _clienteServico.Setup(p => p.SalvarAsync(_clienteDestino));

            _bus.Setup(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            _bus.Setup(p => p.Publish(It.IsAny<LancamentoCriado>(), It.IsAny<CancellationToken>())).Throws<Exception>();

            //When
            var resultado = await _handler.Handle(_command, _cancellationToken);

            //Then
            Assert.False(resultado.Sucesso);
            Assert.Equal("Ocorreu uma falha ao enviar o lancamento gerado.", resultado.Mensagem);
            _clienteServico.Verify(p => p.SalvarAsync(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
