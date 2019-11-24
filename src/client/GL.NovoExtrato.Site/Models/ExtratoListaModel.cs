using System;
using System.Collections.Generic;

namespace GL.NovoExtrato.Site.Models
{
    public class ExtratoListaModel
    {
        public ExtratoListaModel(IEnumerable<ExtratoModel> extratos)
        {
            Extratos = extratos;
        }

        public IEnumerable<ExtratoModel> Extratos { get; set; }
    }
}
