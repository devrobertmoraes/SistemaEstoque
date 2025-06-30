using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaEstoque.Application.DTOs;
using SistemaEstoque.Application.Interfaces;
using SistemaEstoque.Application.Services;
using SistemaEstoque.Application.Extensions;
using SistemaEstoque.Core.Entities;
using SistemaEstoque.Infrastructure.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        var serviceProvider = new ServiceCollection()
        .AddSingleton<IConfiguration>(configuration)
        .AddApplication()
        .AddInfrastructure(configuration)
        .BuildServiceProvider();

        var produtoService = serviceProvider.GetService<ProdutoService>();

        await MenuPrincipal(produtoService);
    }

    public static async Task MenuPrincipal(ProdutoService produtoService)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Sistema de Gestão de Estoque ---");
            Console.WriteLine("1. Listar Produtos");
            Console.WriteLine("2. Adicionar Novo Produto");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    await ListarProdutosUI(produtoService);
                    break;
                case "2":
                    await AdicionarProdutoUI(produtoService);
                    break;
                case "3":
                    Console.WriteLine("Saindo do sistema...");
                    return;
                default:
                    Console.WriteLine("Opção inválida, tente novamente...");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    public static async Task ListarProdutosUI(ProdutoService service)
    {
        Console.WriteLine("\n--- Lista de Produtos Cadastrados ---");
        var produtos = await service.ObterTodosAsync();

        if (!produtos.Any())
        {
            Console.WriteLine("Nenhum produto encontrado.");
            return;
        }

        foreach (var produto in produtos)
        {
            Console.WriteLine($"ID: {produto.Id} | SKU: {produto.Sku} | Nome: {produto.Nome} | Preço: {produto.Preco:C}");
        }
    }

    public static async Task AdicionarProdutoUI(ProdutoService service)
    {
        Console.WriteLine("\n--- Adicionar Novo Produto ---");

        try
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Preco (ex: 19,99): ");
            decimal.TryParse(Console.ReadLine(), out decimal preco);

            Console.Write("SKU (Código): ");
            string sku = Console.ReadLine();

            var novoProdutoDto = new CriarProdutoDTO
            {
                Nome = nome,
                Descricao = descricao,
                Preco = preco,
                Sku = sku
            };

            await service.CriarProdutoAsync(novoProdutoDto);

            Console.WriteLine("\nProduto adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nErro ao adicionar produto: {ex.Message}");
            Console.ResetColor();
        }
    }
}