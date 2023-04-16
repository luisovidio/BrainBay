using RickAndMorty.Core.Domain.ExternalEntities;
using RickAndMorty.Core.Integration.Abstraction;
using RickAndMorty.Core.Integration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Integration.Client
{
    public class RickAndMortyHttpClient : IRickAndMortyHttpClient
    {
        private HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;
        private const string _baseUrl = "https://rickandmortyapi.com/api/";

        public RickAndMortyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IncludeFields = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
        }

        public async Task<ResponseModel<T>> GetPageAsync<T>(string controller, int page = 1, CancellationToken cancellationToken = default)
            where T : IExternalClass
        {
            string path = $"{_baseUrl}{controller}/?page={page}";

            var response = await _httpClient.GetAsync(path, cancellationToken);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<ResponseModel<T>>(content, _serializerOptions)!;
        }
    }
}
