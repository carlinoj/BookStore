using BookStore.Application.DTOs.Book;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

/// <summary>
/// Controlador responsável por gerenciar os livros da aplicação BookStore.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Lista todos os livros, com suporte a busca por título e filtro por categoria.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] string? titulo = null, [FromQuery] string? categoria = null)
    {
        var livros = await _bookService.ListarTodosAsync(titulo, categoria);
        
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var book = await _bookService.ObterPorIdAsync(id);
        if (book == null) return NotFound();
        
        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] BookCreateDto dto)
    {
        var book = await _bookService.CriarAsync(dto);
        
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] BookUpdateDto dto)
    {
        await _bookService.AtualizarAsync(id, dto);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _bookService.RemoverAsync(id);
        
        return NoContent();
    }
}