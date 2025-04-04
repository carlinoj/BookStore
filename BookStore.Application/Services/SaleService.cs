using BookStore.Application.DTOs.Sales;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services;

public class SaleService
{
    private readonly IRepository<Book> _bookRepo;

    public SaleService(IRepository<Book> bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public async Task RealizarCompraAsync(BookSalesDto dto)
    {
        var livro = await _bookRepo.GetByIdAsync(dto.LivroId)
            ?? throw new Exception("Livro não encontrado");

        if (dto.Quantidade <= 0)
            throw new Exception("Quantidade inválida.");

        if (dto.Quantidade > livro.QuantidadeEstoque)
            throw new Exception("Estoque insuficiente.");

        livro.QuantidadeEstoque -= dto.Quantidade;

        _bookRepo.Update(livro);
        await _bookRepo.SaveChangesAsync();
    }
}
