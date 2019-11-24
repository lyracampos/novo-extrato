using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GL.NovoExtrato.Consulta.Domain.Interfaces;
using GL.NovoExtrato.Consulta.Models;
using GL.NovoExtrato.Shared.Entities;
namespace GL.NovoExtrato.Consulta.Services
{
    public class ExtratoServico : IExtratoServico
    {
        private readonly IExtratoRepositorio _repositorio;
        public ExtratoServico(IExtratoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<Resultado> AdicionarAsync(ExtratoModel model)
        {
            var extrato = ExtratoMapping.ModeloParaEntidade(model);
            await _repositorio.AdicionarAsync(extrato);
            return new Resultado(true, "Extrato adicionar com sucesso.");
        }

        public async Task<IEnumerable<ExtratoModel>> ObterPorClienteAsync(int clienteId)
        {
            var lancamentos = await _repositorio.ObterPorCliente(clienteId);
            if(lancamentos == null) return null;
            return lancamentos.Select(extrato => ExtratoMapping.EntidadeParaModelo(extrato));
        }

        public async Task RemoverAsync(int id)
        {
            await _repositorio.RemoverAsync(id);
        }
    }
}