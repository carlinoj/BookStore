using BookStore.Application.DTOs.Book;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services;

public class BookService
{
    private readonly IRepository<Book> _bookRepo;
    private readonly IRepository<Author> _authorRepo;

    public BookService(IRepository<Book> bookRepo, IRepository<Author> authorRepo)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
    }

    public async Task<IEnumerable<BookReadDto>> ListarTodosAsync(string? titulo = null, string? categoria = null)
    {
        var livros = await _bookRepo.GetAllAsync();

        if (!string.IsNullOrWhiteSpace(titulo))
        {
            livros = livros.Where(b => b.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            livros = livros.Where(b => b.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
        }

        return livros.Select(b => new BookReadDto
        {
            Id = b.Id,
            Titulo = b.Titulo,
            Categoria = b.Categoria,
            DataPublicacao = b.DataPublicacao,
            Preco = b.Preco,
            QuantidadeEstoque = b.QuantidadeEstoque,
            AuthorId = b.AuthorId,
            AuthorNome = b.Autor?.Nome ?? string.Empty
        });
    }

    public async Task<Book?> ObterPorIdAsync(Guid id) =>
        await _bookRepo.GetByIdAsync(id);

    public async Task<Book> CriarAsync(BookCreateDto dto)
    {
        var autor = await _authorRepo.GetByIdAsync(dto.AuthorId);
        if (autor == null)
            throw new Exception("Autor não encontrado");

        var book = new Book
        {
            Titulo = dto.Titulo,
            Categoria = dto.Categoria,
            DataPublicacao = dto.DataPublicacao,
            Preco = dto.Preco,
            QuantidadeEstoque = dto.QuantidadeEstoque,
            AuthorId = dto.AuthorId
        };

        await _bookRepo.AddAsync(book);
        await _bookRepo.SaveChangesAsync();

        return book;
    }

    public async Task AtualizarAsync(Guid id, BookUpdateDto dto)
    {
        var book = await _bookRepo.GetByIdAsync(id)
            ?? throw new Exception("Livro não encontrado");

        book.Titulo = dto.Titulo;
        book.Categoria = dto.Categoria;
        book.DataPublicacao = dto.DataPublicacao;
        book.Preco = dto.Preco;
        book.QuantidadeEstoque = dto.QuantidadeEstoque;

        _bookRepo.Update(book);
        await _bookRepo.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid id)
    {
        var book = await _bookRepo.GetByIdAsync(id)
            ?? throw new Exception("Livro não encontrado");

        _bookRepo.Remove(book);
        await _bookRepo.SaveChangesAsync();
    }
}