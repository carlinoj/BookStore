using BookStore.Application.DTOs.Author;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services;

public class AuthorService
{
    private readonly IRepository<Author> _authorRepo;

    public AuthorService(IRepository<Author> authorRepo)
    {
        _authorRepo = authorRepo;
    }

    public async Task<IEnumerable<AuthorReadDto>> ListarTodosAsync()
    {
        var autores = await _authorRepo.GetAllAsync();

        return autores.Select(a => new AuthorReadDto
        {
            Id = a.Id,
            Nome = a.Nome,
            DataNascimento = a.DataNascimento,
            Pais = a.Pais
        });
    }

    public async Task<Author?> ObterPorIdAsync(Guid id) =>
        await _authorRepo.GetByIdAsync(id);

    public async Task<Author> CriarAsync(AuthorCreateDto dto)
    {
        var autor = new Author
        {
            Nome = dto.Nome,
            DataNascimento = dto.DataNascimento,
            Pais = dto.Pais
        };

        await _authorRepo.AddAsync(autor);
        await _authorRepo.SaveChangesAsync();

        return autor;
    }

    public async Task AtualizarAsync(Guid id, AuthorUpdateDto dto)
    {
        var autor = await _authorRepo.GetByIdAsync(id)
            ?? throw new Exception("Autor não encontrado");

        autor.Nome = dto.Nome;
        autor.DataNascimento = dto.DataNascimento;
        autor.Pais = dto.Pais;

        _authorRepo.Update(autor);
        await _authorRepo.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid id)
    {
        var autor = await _authorRepo.GetByIdAsync(id)
            ?? throw new Exception("Autor não encontrado");

        _authorRepo.Remove(autor);
        await _authorRepo.SaveChangesAsync();
    }
}