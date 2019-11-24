using System.Net.Http;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Entities;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using GL.NovoExtrato.Lancamento.Models;

namespace GL.NovoExtrato.Lancamento.Services
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _repositorio;
        public ClienteServico(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Cliente> AdicionarAsync()
        {
            var cliente = new Cliente();
            return await _repositorio.AdicionarAsync(cliente);
        }

        public async Task SalvarAsync(Cliente cliente)
        {
            var clienteDb = await _repositorio.ObterPorIdAsync(cliente.Id);
            if (clienteDb != null)
            {
                clienteDb.ContaCorrente.SetSaldo(cliente.ContaCorrente.Saldo);
                await _repositorio.AtualizarAsync(clienteDb);
            }
        }
    }
}