using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GL.NovoExtrato.Consulta.Domain.Entities;
using GL.NovoExtrato.Consulta.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GL.NovoExtrato.Consulta.Infrastructure.Repositorios
{
    public class ExtratoRepositorio : IExtratoRepositorio
    {
        private readonly ExtratoContexto _contexto;
        public ExtratoRepositorio(ExtratoContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task AdicionarAsync(Extrato extrato)
        {
            await _contexto.Extratos.AddAsync(extrato);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Extrato>> ObterPorCliente(int clienteId)
        {
            return await _contexto.Extratos
                            .Where(p => p.ClienteId == clienteId)
                            .ToArrayAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var extrato = await _contexto.Extratos.FindAsync(id);
            if (extrato != null)
            {
                _contexto.Extratos.Remove(extrato);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}