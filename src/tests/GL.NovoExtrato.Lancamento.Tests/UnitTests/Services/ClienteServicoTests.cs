using System;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Entities;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using GL.NovoExtrato.Lancamento.Services;
using Moq;
using Xunit;

namespace GL.NovoExtrato.Lancamento.Tests.UnitTests.Services
{
    public class ClienteServicoTests
    {
        private readonly Mock<IClienteRepositorio> _clienteRepositorio;
        private readonly ClienteServico _clienteServico;
        private Cliente _cliente;
        public ClienteServicoTests()
        {
            _clienteRepositorio = new Mock<IClienteRepositorio>();
            _clienteServico = new ClienteServico(_clienteRepositorio.Object);
        }

        [Fact(DisplayName = "UT. ClienteServico SalvarAsync - deve atualizar dados do cliente quando existir no banco.")]
        public async Task DeveAtualizarDadosDoCliente_Quando_ExistirNoBancoDeDados()
        {
            //arrange
            _cliente = new Cliente();
            _clienteRepositorio.Setup(p => p.ObterPorIdAsync(_cliente.Id)).ReturnsAsync(_cliente);

            //act
            await _clienteServico.SalvarAsync(_cliente);

            //assert
            _clienteRepositorio.Verify(p => p.AtualizarAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact(DisplayName = "UT. ClienteServico SalvarAsync - deve não atualizar dados do cliente quando não existir no banco.")]
        public async Task DeveNaoAtualizarDadosDoCliente_Quando_NaoExistirNoBancoDeDados()
        {
            //arrange
            _cliente = new Cliente();
            Cliente clienteDb = null;
            _clienteRepositorio.Setup(p => p.ObterPorIdAsync(3)).ReturnsAsync(clienteDb);

            //act
            await _clienteServico.SalvarAsync(_cliente);

            //assert
            _clienteRepositorio.Verify(p => p.AtualizarAsync(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
