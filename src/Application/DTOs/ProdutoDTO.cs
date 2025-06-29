namespace SistemaEstoque.Application.DTOs;

public class ProdutoDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Sku { get; set; }
    public decimal Preco { get; set; }
}