#pragma checksum "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0929f575bf4b9bc37a2dcf389b3910370ea03002"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Consulta_Extrato), @"mvc.1.0.view", @"/Views/Consulta/Extrato.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Consulta/Extrato.cshtml", typeof(AspNetCore.Views_Consulta_Extrato))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/_ViewImports.cshtml"
using GL.NovoExtrato.Site;

#line default
#line hidden
#line 2 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/_ViewImports.cshtml"
using GL.NovoExtrato.Site.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0929f575bf4b9bc37a2dcf389b3910370ea03002", @"/Views/Consulta/Extrato.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7076356081dbee01ff96ad992535aef945f2c3ba", @"/Views/_ViewImports.cshtml")]
    public class Views_Consulta_Extrato : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GL.NovoExtrato.Site.Models.ExtratoListaModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
  
    ViewData["Title"] = "Extrato inicial";

#line default
#line hidden
            BeginContext(100, 504, true);
            WriteLiteral(@"
<h3>Extrato da conta</h3>
 <table class=""table table-bordered"">
          <thead>
            <tr>
              <th scope=""col"">Data</th>
              <th scope=""col"">Tipo</th>
              <th scope=""col"">Transação</th>
              <th scope=""col"">Valor</th>
              <th scope=""col"">Descrição</th>
              <th scope=""col"">Conta Origem</th>
              <th scope=""col"">Conta Destino</th>
              <th scope=""col"">Saldo</th>
            </tr>
          </thead>
          <tbody>
");
            EndContext();
#line 21 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
               foreach (var extrato in Model.Extratos)
              {

#line default
#line hidden
            BeginContext(675, 43, true);
            WriteLiteral("                <tr>\n                  <td>");
            EndContext();
            BeginContext(719, 16, false);
#line 24 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.CriadoEm);

#line default
#line hidden
            EndContext();
            BeginContext(735, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(764, 21, false);
#line 25 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.TipoDescricao);

#line default
#line hidden
            EndContext();
            BeginContext(785, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(814, 30, false);
#line 26 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.TransacaoTipoDescricao);

#line default
#line hidden
            EndContext();
            BeginContext(844, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(873, 22, false);
#line 27 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.ValorTransacao);

#line default
#line hidden
            EndContext();
            BeginContext(895, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(924, 17, false);
#line 28 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.Descricao);

#line default
#line hidden
            EndContext();
            BeginContext(941, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(970, 19, false);
#line 29 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.ContaOrigem);

#line default
#line hidden
            EndContext();
            BeginContext(989, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(1018, 20, false);
#line 30 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.ContaDestino);

#line default
#line hidden
            EndContext();
            BeginContext(1038, 28, true);
            WriteLiteral("</td>\n                  <td>");
            EndContext();
            BeginContext(1067, 13, false);
#line 31 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
                 Write(extrato.Saldo);

#line default
#line hidden
            EndContext();
            BeginContext(1080, 28, true);
            WriteLiteral("</td>\n                </tr>\n");
            EndContext();
#line 33 "/Users/guilhermelyracampos/Documents/profissional/projetos/testes/novo-extrato/src/client/GL.NovoExtrato.Site/Views/Consulta/Extrato.cshtml"
              }

#line default
#line hidden
            BeginContext(1124, 35, true);
            WriteLiteral("          </tbody>\n        </table>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GL.NovoExtrato.Site.Models.ExtratoListaModel> Html { get; private set; }
    }
}
#pragma warning restore 1591