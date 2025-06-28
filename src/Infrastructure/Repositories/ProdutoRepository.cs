using Dapper;
using Microsoft.Data.SqlClient;
using SistemaEstoque.Application.Interfaces;
using SistemaEstoque.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaEstoque.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly string _connectionString;

    // O repositório recebe a string de conexão quando é criado
    public ProdutoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task CriarAsync(Produto produto)
    {
        // O using garante que a conexão com o banco será fechada ao final do bloco
        using SqlConnection connection = new SqlConnection(_connectionString);

        string sql = "INSERT INTO Produtos (Id, Nome, Descricao, Preco, Sku) VALUES (@Id, @Nome, @Descricao, @Preco, @Sku)";

        // ExecuteAsync é um método do Dapper para executar comandos que não retornam valores (INSERT, UPDATE, DELETE)
        // O Dapper magicamente mapeia as propriedades do objeto produto para os parâmetros do SQL (@Id, @Nome...)
        await connection.ExecuteAsync(sql, produto);
    }

    public async Task<Produto> ObterPorIdAsync(Guid id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);

        string sql = "SELECT * FROM Produtos WHERE Id = @Id";

        // QueryFirstOrDefaultAsync busca o primeiro resultado ou null se não encontrar.
        // O Dapper mapeia as colunas do resultado para as propriedades da classe Produto.
        return await connection.QueryFirstOrDefaultAsync<Produto>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        using var connection = new SqlConnection(_connectionString);

        var sql = "SELECT * FROM Produtos ORDER BY Nome";

        // QueryAsync retorna uma coleção de objetos.
        return await connection.QueryAsync<Produto>(sql);
    }

    public async Task<Produto> ObterPorIdComMovimentacoesAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);

        // A MÁGICA DO JOIN:
        // - SELECT P.*, M.*: Selecione todas as colunas da tabela Produtos (apelidada de P) E todas da MovimentacoesEstoque (M).
        // - FROM Produtos P: Comece pela tabela Produtos.
        // - LEFT JOIN MovimentacoesEstoque M ON P.Id = M.ProdutoId:
        //   Junte com a tabela MovimentacoesEstoque. A condição da junção (ON) é que o Id do Produto (P.Id) seja igual
        //   ao ProdutoId da movimentação (M.ProdutoId).
        // - 'LEFT JOIN' significa: "traga-me todos os produtos, mesmo que eles não tenham nenhuma movimentação".
        string sql = @"SELECT P.*, M.* FROM Produtos P LEFT JOIN MovimentacoesEstoque M ON P.Id = M.ProdutoId WHERE P.id = @Id";

        Produto produto = null;
        
        // O Dapper consegue mapear o resultado de um JOIN para múltiplos objetos.
        // O último tipo genérico <Produto> é o tipo de retorno da função.
        await connection.QueryAsync<Produto, MovimentacaoEstoque, Produto>(
            sql,
            (prod, mov) =>
            {
                // Este bloco é executado para cada linha retornada pelo SQL.
                // Na primeira vez, criamos o objeto 'produto'.
                if (produto == null)
                {
                    produto = prod;
                }

                // Se a movimentação não for nula (o produto pode não ter movimentações),
                // nós a adicionamos à lista do produto.
                if (mov != null)
                {
                    produto.MovimentacoesEstoque.Add(mov);
                }

                return produto;
            },
            new { Id = id },
            // 'splitOn' diz ao Dapper onde começa a próxima entidade na consulta SQL (a coluna Id da MovimentacaoEstoque).
            splitOn: "Id");

        return produto;
    }
}