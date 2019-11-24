using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GL.NovoExtrato.Consulta.Infrastructure.Repositorios;
using GL.NovoExtrato.Consulta.Models;
using GL.NovoExtrato.Shared.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;

namespace GL.NovoExtrato.Consulta.Tests.IntegrationTests
{
    public class ExtratoControllerTests
    {

        private TestServer _server;
        private HttpClient _httpClient;
        private ExtratoContexto _context;
        private ExtratoModel _model;

        public ExtratoControllerTests()
        {

            _server = new TestServer(new WebHostBuilder()
                    .UseEnvironment("Development")
                    .UseStartup<Startup>());
            _context = _server.Host.Services.GetService(typeof(ExtratoContexto)) as ExtratoContexto;
            _httpClient = _server.CreateClient();
            _model = new ExtratoModel(123, 999999, DateTime.UtcNow, LancamentoTipo.TED, 1500, "TED", "NumOri", "Cliente Origem", "NumDes", "Cliente Destino", 3000, TransacaoTipo.Debito);
        }

        [Fact(DisplayName = "IT. ExtratoController Get - deve retornar extrato de um cliente")]
        public async Task DeveRetornarExtratoDoCliente_Quando_HouverNoBanco()
        {
            //arrange
            var json = JsonConvert.SerializeObject(_model);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/extrato/novo", payload);
            response.EnsureSuccessStatusCode();

            //act
            response = await _httpClient.GetAsync($"/api/extrato/{_model.ClienteId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var extratos = JsonConvert.DeserializeObject<IEnumerable<ExtratoModel>>(result);

            //assert
            extratos.Should().NotBeNull();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            foreach (var extrato in extratos)
            {
                response = await _httpClient.DeleteAsync($"/api/extrato/{extrato.Id}");
                response.EnsureSuccessStatusCode();
            }
        }

        [Fact(DisplayName = "IT. ExtratoController Post (novo) - deve retornar status ok quando gerar novo extrato")]
        public async Task DeveRetornarOk_Quando_CriarLancamentoComSucesso()
        {
            //arrange
            var json = JsonConvert.SerializeObject(_model);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");

            //act
            var response = await _httpClient.PostAsync("/api/extrato/novo", payload);
            response.EnsureSuccessStatusCode();

            //assert
            response.Should().NotBeNull();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _httpClient.GetAsync($"/api/extrato/{_model.ClienteId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var extratos = JsonConvert.DeserializeObject<IEnumerable<ExtratoModel>>(result);

            //assert
            extratos.Should().NotBeNull();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            foreach (var extrato in extratos)
            {
                response = await _httpClient.DeleteAsync($"/api/extrato/{extrato.Id}");
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
