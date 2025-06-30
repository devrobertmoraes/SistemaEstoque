using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using SistemaEstoque.Application.Interfaces;
using SistemaEstoque.Infrastructure.Repositories;

namespace SistemaEstoque.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProdutoRepository>(sp => new ProdutoRepository(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}