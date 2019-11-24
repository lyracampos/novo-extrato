using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GL.NovoExtrato.Lancamento.Domain.Entities;
using GL.NovoExtrato.Lancamento.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace GL.NovoExtrato.Lancamento.Infrastructure.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly LancamentoContexto _contexto;

        public ClienteRepositorio(LancamentoContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            await _contexto.Clientes.AddRangeAsync(cliente);
            await _contexto.SaveChangesAsync();
            return cliente;
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Cliente> ObterPorContaAsync(string numeroDaConta)
        {
            return await _contexto.Clientes
                                  .Include(p => p.ContaCorrente)
                                  .FirstOrDefaultAsync(p => p.ContaCorrente.Numero.ToLowerInvariant().Equals(numeroDaConta.ToLowerInvariant()));
        }

        public async Task<Cliente> ObterPorIdAsync(int id)
        {
            return await _contexto.Clientes
                                  .Include(p => p.ContaCorrente)
                                  .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _contexto.Clientes.Include(p => p.ContaCorrente).ToArrayAsync();
        }
    }
}
