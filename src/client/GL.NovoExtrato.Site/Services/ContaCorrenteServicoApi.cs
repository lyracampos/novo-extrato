using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GL.NovoExtrato.Site.Models;
using Microsoft.Extensions.Configuration;

namespace GL.NovoExtrato.Site.Services
{
    public class ContaCorrenteServicoApi : IContaCorrenteServicoApi
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public ContaCorrenteServicoApi(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_configuration["APIUrls:ContaCorrenteAPI"]);
        }

        public async Task CriarClienteAsync()
        {
            var model = new CriarClienteModel();
            using (var response = await _httpClient.PostAsJsonAsync("contacorrente/novo-cliente", model))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<IEnumerable<ContaCorrenteModel>> ObterContas()
        {
            using (var response = await _httpClient.GetAsync("contacorrente"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<ContaCorrenteModel>>();
            }
        }

        public async Task CriarLancamento(CriarLancamentoModel model)
        {
            using (var response = await _httpClient.PostAsJsonAsync($"contacorrente/novo-lancamento", model))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}

