using System;
using System.Threading;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using GL.NovoExtrato.Lancamento.Events;
using GL.NovoExtrato.Shared.Entities;
using GL.NovoExtrato.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GL.NovoExtrato.Lancamento.Commands
{
    public class CriarClienteHandler : IRequestHandler<CriarCliente, Resultado>
    {
        private readonly ILogger _logger;
        private readonly IClienteServico _clienteServico;
        private readonly IMediator _bus;
        public CriarClienteHandler(
            ILogger<CriarClienteHandler> logger,
            IClienteServico clienteServico,
            IMediator bus)
        {
            _logger = logger;
            _clienteServico = clienteServico;
            _bus = bus;
        }
        public async Task<Resultado> Handle(CriarCliente request, CancellationToken cancellationToken)
        {
            var cliente =  await _clienteServico.AdicionarAsync();
            try
            {
                //dispara evento
                await _bus.Publish(
                    new LancamentoCriado(cliente.Id, LancamentoTipo.DepositoInicial, cliente.ContaCorrente.Saldo,
                                cliente.ContaCorrente.Numero, cliente.Nome,
                                cliente.ContaCorrente.Numero, cliente.Nome, cliente.ContaCorrente.Saldo, TransacaoTipo.Credito));
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Ocorreu uma falha ao enviar o lancamento gerado. {0}", ex.Message));
                return new Resultado(false, string.Format("Ocorreu uma falha ao enviar o lancamento gerado. {0}", ex.Message));
            }

            _logger.LogInformation("Cliente criado com suceso.");
            return new Resultado(true, "Cliente criado com sucesso.");
        }
    }
}