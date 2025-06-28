using SistemaEstoque.Core.Enums;

namespace SistemaEstoque.Core.Entities;

/// <summary>
/// Representa uma movimentação (Entrada, Saída, Ajuste) de um produto no estoque
/// </summary>
public class MovimentacaoEstoque
{
    public Guid Id { get; private set; }
    public DateTime DataHora { get; private set; }

    /// <summary>
    /// Chave estrangeira do produto que foi movimentado
    /// </summary>
    public Guid ProdutoId { get; private set; }

    /// <summary>
    /// Chave estrangeira do usuário que fez a movimentação do produto
    /// </summary>
    public Guid UsuarioId { get; private set; }
    public int Quantidade { get; private set; }
    public TipoMovimentacaoEnum Tipo { get; private set; }

    // propriedades de navegação (para o dapper preencher se for preciso)
    public Produto Produto { get; set; }
    public Usuario Usuario { get; set; }

    public MovimentacaoEstoque(Guid produtoId, Guid usuarioId, int quantidade, TipoMovimentacaoEnum tipo)
    {
        Id = Guid.NewGuid();
        DataHora = DateTime.Now;
        ProdutoId = produtoId;
        UsuarioId = usuarioId;
        Quantidade = quantidade;
        Tipo = tipo;
    }
}