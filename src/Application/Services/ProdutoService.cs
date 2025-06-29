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