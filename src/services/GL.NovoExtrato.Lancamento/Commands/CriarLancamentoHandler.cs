using System;
using System.Threading;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using GL.NovoExtrato.Lancamento.Events;
using GL.NovoExtrato.Lancamento.Models;
using GL.NovoExtrato.Shared.Entities;
using GL.NovoExtrato.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GL.NovoExtrato.Lancamento.Commands
{
    public class CriarLancamentoHandler : IRequestHandler<CriarLancamento, Resultado>
    {
        private readonly ILogger _logger;
        private readonly IClienteServico _clienteServico;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMediator _bus;
        public CriarLancamentoHandler(
            ILogger<CriarLancamentoHandler> logger,
            IClienteServico clienteServico,
            IClienteRepositorio clienteRepositorio,
            IMediator bus)
        {
            _logger = logger;
            _clienteServico = clienteServico;
            _clienteRepositorio = clienteRepositorio;
            _bus = bus;
        }
        public async Task<Resultado> Handle(CriarLancamento request, CancellationToken cancellationToken)
        {
            //valida request
            var clienteOrigem = await _clienteRepositorio.ObterPorContaAsync(request.NumeroContaOrigem);
            if (clienteOrigem == null)
            {
                _logger.LogWarning("Cliente de conta Origem não encontrado.");
                return new Resultado(false, "Cliente de conta Origem não encontrado.");
            }

            var clienteDestino = await _clienteRepositorio.ObterPorContaAsync(request.NumeroContaDestino);
            if (clienteDestino == null)
            {
                _logger.LogWarning("Cliente de conta Origem não encontrado.");
                return new Resultado(false, "Cliente de conta Origem não encontrado.");
            }  

            //trata lancamento e ajusta saldos da conta origem e conta destino
            if (request.SolicitacaoDeTransferencia())
            {
                clienteOrigem.ContaCorrente.DiminuirSaldo(request.Valor);
                clienteDestino.ContaCorrente.AumentarSaldo(request.Valor);
            }

            try
            {
                //dispara evento
                await _bus.Publish(
                    new LancamentoCriado(clienteOrigem.Id, request.Tipo, request.Valor,
                                clienteOrigem.ContaCorrente.Numero, clienteOrigem.Nome,
                                clienteDestino.ContaCorrente.Numero, clienteDestino.Nome, clienteOrigem.ContaCorrente.Saldo, TransacaoTipo.Debito));

                await _bus.Publish(
                    new LancamentoCriado(clienteDestino.Id, request.Tipo, request.Valor,
                                clienteOrigem.ContaCorrente.Numero, clienteOrigem.Nome,
                                clienteDestino.ContaCorrente.Numero, clienteDestino.Nome, clienteDestino.ContaCorrente.Saldo, TransacaoTipo.Credito));
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Ocorreu uma falha ao enviar o lancamento gerado. {0}", ex.Message));
                return new Resultado(false, "Ocorreu uma falha ao enviar o lancamento gerado.");
            }

            await _clienteServico.SalvarAsync(clienteOrigem);
            await _clienteServico.SalvarAsync(clienteDestino);

            _logger.LogInformation("Lançamento gerado com suceso.");
            return new Resultado(true, "Lançamento gerado com sucesso.");
        }
    }
}
