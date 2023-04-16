using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RickAndMorty.Core.Data;
using RickAndMorty.Core.Integration;
using RickAndMorty.Core.Services;
using RickAndMorty.Core.Services.Abstraction;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddRickAndMortyApiIntegration();
        services.AddRickAndMortyServices();
        services.AddRepositories();
    })
    .Build();

var cancellationTokenSource = new CancellationTokenSource();
using var scope = host.Services.CreateScope();

var etlService = scope.ServiceProvider.GetRequiredService<IRickAndMortyEtlService>();

await etlService.ResetDatabaseAsync(cancellationTokenSource.Token);
await etlService.EtlDataAsync(cancellationTokenSource.Token);