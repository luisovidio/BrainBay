using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Core.Services.Abstraction;
using System.Reflection;
using MediatR;
using RickAndMorty.Core.Services.PipelineBehaviors;

namespace RickAndMorty.Core.Services
{
    public static class DependencyInjection
    {
        public static void AddRickAndMortyServices(this IServiceCollection services)
        {
            services.AddScoped<IRickAndMortyEtlService, RickAndMortyEtlService>();
            
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(CachingBehavior<,>));
            });

            services.AddMemoryCache();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}