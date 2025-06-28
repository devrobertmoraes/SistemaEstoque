using SistemaEstoque.Core.Entities;

namespace SistemaEstoque.Application.Interfaces;

public interface IProdutoRepository
{
    Task CriarAsync(Produto produto);
    Task<Produto> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
    Task<Produto> ObterPorIdComMovimentacoesAsync(Guid id);
}