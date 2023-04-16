using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Core.Data.Abstractions;
using RickAndMorty.Core.Data.Repositories;

namespace RickAndMorty.Core.Data
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            var connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True";

            services.AddDbContext<RickAndMortyContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    s =>
                    {
                        s.MigrationsAssembly(typeof(RickAndMortyContext).Assembly.FullName);
                    });
            });

            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
        }
    }
}