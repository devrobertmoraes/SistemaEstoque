namespace SistemaEstoque.Core.Entities;

/// <summary>
/// Representa um produto no estoque
/// </summary>
public class Produto
{
    /// <summary>
    /// Id do produto
    /// </summary>
    public Guid Id { set; private get; }

    /// <summary>
    /// Nome do produto
    /// </summary>
    public string Nome { set; private get; }

    /// <summary>
    /// Descrição detalhada
    /// </summary>
    public string Descricao { set; private get; }

    /// <summary>
    /// Preço
    /// </summary>
    public decimal Preco { set; private get; }

    /// <summary>
    /// Stock Keeping Unit
    /// </summary>
    public string Sku { set; private get; }

    /// <summary>
    /// Um produto pode ter várias movimentações
    /// </summary>
    public List<MovimentacaoEstoque> MovimentacoesEstoque { get; private set; }

    /// <summary>
    /// Contrutor da classe
    /// </summary>
    public Produto(string nome, string descricao, decimal preco, string sku)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Sku = sku;
        MovimentacoesEstoque = new List<MovimentacaoEstoque>();
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (novoPreco > 0)
        {
            Preco = novoPreco;
        }
    }
}