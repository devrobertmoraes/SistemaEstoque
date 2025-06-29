using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaEstoque.Application.Interfaces;
using SistemaEstoque.Application.Services;
using SistemaEstoque.Application.DTOs;
using SistemaEstoque.Infrastructure.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("--- Iniciando a conexão manual ---");

        // definindo a receita para acessar o banco de dados
        string connectionString = "Server=.\\SQLEXPRESS;Database=SistemaEstoqueDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // criando a instância do repositório
        ProdutoRepository produtoRepository = new ProdutoRepository(connectionString);

        // criando a instância do serviço
        ProdutoService produtoService = new ProdutoService(produtoRepository);

        Console.WriteLine("Conexão manual feita com sucesso! O serviço está pronto para ser usado.");
        Console.WriteLine("---------------------------------------\n");

        // usando o serviço
        await ListarTodosProdutos(produtoService);
    }

    public static async Task ListarTodosProdutos(ProdutoService service)
    {
        Console.WriteLine("Buscando todos os produtos no banco de dados...");
        IEnumerable<ProdutoDTO> produtos = await service.ObterTodosAsync();

        if (!produtos.Any())
        {
            Console.WriteLine("Nenhum produto encontrado no estoque.");
            return;
        }

        foreach (var produto in produtos)
        {
            Console.WriteLine($"ID: {produto.Id} | SKU: {produto.Sku} | Nome: {produto.Nome} | Preço: {produto.Preco:C}");
        }
    }
}