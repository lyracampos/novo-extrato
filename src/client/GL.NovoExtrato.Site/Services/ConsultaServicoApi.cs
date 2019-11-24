﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GL.NovoExtrato.Site.Models;
using Microsoft.Extensions.Configuration;

namespace GL.NovoExtrato.Site.Services
{
    public class ConsultaServicoApi : IConsultaServicoApi
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;

        public ConsultaServicoApi(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_configuration["APIUrls:ConsultaAPI"]);
        }

        public async Task<IEnumerable<ExtratoModel>> ObterAsync(int clienteId)
        {
            using (var response = await _httpClient.GetAsync($"extrato/{clienteId}"))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<IEnumerable<ExtratoModel>>();
            }
        }
    }
}
