using SistemaEstoque.Application.DTOs;
using SistemaEstoque.Application.Interfaces;
using SistemaEstoque.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEstoque.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    /// <summary>
    /// Cria um novo produto no sistema com base nos dados fornecidos.
    /// </summary>
    /// <param name="dto">O objeto de transferência de dados contendo as informações do novo produto</param>
    /// <returns>Uma task que representa a conclusão da operação assíncrona</returns>
    /// <exception cref="Exception">Lança uma excessão se o preço do produto for menor que 0</exception>
    public async Task CriarProdutoAsync(CriarProdutoDTO dto)
    {
        if (dto.Preco <= 0)
        {
            throw new Exception("O preço do produto deve ser maior que 0");
        }

        Produto produto = new Produto(dto.Nome, dto.Descricao, dto.Preco, dto.Sku);

        await _produtoRepository.CriarAsync(produto);
    }

    public async Task<IEnumerable<ProdutoDTO>> ObterTodosAsync()
    {
        var produtos = await _produtoRepository.ObterTodosAsync();

        return produtos.Select(p => new ProdutoDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Sku = p.Sku
        });
    }
}