using Microsoft.Extensions.DependencyInjection;
using SistemaEstoque.Application.Services;

namespace SistemaEstoque.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registramos os nossos serviços aqui
        services.AddScoped<ProdutoService>();

        return services;
    }
}