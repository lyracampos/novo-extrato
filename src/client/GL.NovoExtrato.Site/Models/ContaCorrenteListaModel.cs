using System.Collections.Generic;
using System.Linq;

namespace GL.NovoExtrato.Site.Models
{
    public class ContaCorrenteListaModel
    {
        public ContaCorrenteListaModel(IEnumerable<ContaCorrenteModel> contasCorrentes)
        {
            ContasCorrentes = contasCorrentes;
        }
        public IEnumerable<ContaCorrenteModel> ContasCorrentes { get; set; }
        public bool TemContaCorrente
        {
            get
            {
                return ContasCorrentes != null && ContasCorrentes.Any();
            }
        }
    }
}
