using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GL.NovoExtrato.Lancamento.Events
{
    public class LancamentoCriadoHandler : INotificationHandler<LancamentoCriado>
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public LancamentoCriadoHandler(
            ILogger<LancamentoCriado> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:8000/api/");
        }
        public async Task Handle(LancamentoCriado notification, CancellationToken cancellationToken)
        {
            using (var response = await _httpClient.PostAsJsonAsync($"extrato/novo/", notification))
            {
                response.EnsureSuccessStatusCode();
            }

            _logger.LogInformation($"Lançamento realizado entre as contas '{notification.ContaOrigem}' e '{notification.ContaDestino}'.");
        }
    }
}