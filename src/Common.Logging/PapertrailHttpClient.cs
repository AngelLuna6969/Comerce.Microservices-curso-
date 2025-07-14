using Microsoft.Extensions.Configuration;
using Serilog.Sinks.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    // Implementación de un cliente HTTP para interactuar con Papertrail
    public class PapertrailHttpClient : IHttpClient
    {
        private readonly HttpClient _client;

        public PapertrailHttpClient(string token)
        {
            _client = new HttpClient();

            // Configura la cabecera de autorización con el token proporcionado
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Establece el tipo de contenido como "application/octet-stream"
            // para enviar datos binarios
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/octet-stream");
        }

        // Método para enviar una solicitud HTTP POST con un flujo de datos
        public Task<HttpResponseMessage> PostAsync(string requestUri, Stream contentStream, CancellationToken cancellationToken)
        {
            var content = new StreamContent(contentStream);
            return _client.PostAsync(requestUri, content, cancellationToken);
        }

        // Método de configuración
        public void Configure(IConfiguration configuration)
        {
            // Sin configuración adicional
        }

        // Método para liberar los recursos del cliente HTTP
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
