using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaEstoque.Application.Interfaces;
using SistemaEstoque.Application.Services;
using SistemaEstoque.Infrastructure.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        var serviceProvider = new ServiceCollection().AddSingleton<IConfiguration>(configuration)
        .AddScoped<IProdutoRepository>(sp => new ProdutoRepository(configuration.GetConnectionString("DefaultConnection")))
        .AddScoped<ProdutoService>()
        .BuildServiceProvider();

        Console.WriteLine("Injeção de Dependência configurada!");
        Console.WriteLine("A Camada de Aplicação está pronta para ser usada.");

        await Task.CompletedTask;
    }
}