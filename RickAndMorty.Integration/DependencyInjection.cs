using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Core.Integration.Abstraction;
using RickAndMorty.Core.Integration.Client;

namespace RickAndMorty.Core.Integration
{
    public static class DependencyInjection
    {
        public static void AddRickAndMortyApiIntegration(this IServiceCollection services)
        {
            services.AddHttpClient<IRickAndMortyHttpClient, RickAndMortyHttpClient>();
        }
    }
}
