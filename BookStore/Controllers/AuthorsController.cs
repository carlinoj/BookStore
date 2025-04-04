using BookStore.Application.DTOs.Author;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

/// <summary>
/// Controlador responsável por gerenciar os autores da aplicação BookStore.
/// A criação, atualização e remoção requerem papel de Admin.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorsController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    /// <summary>
    /// Lista todos os autores.
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var autores = await _authorService.ListarTodosAsync();
        return Ok(autores);
    }

    /// <summary>
    /// Obtém um autor por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var autor = await _authorService.ObterPorIdAsync(id);
        if (autor == null) return NotFound();
        return Ok(autor);
    }

    /// <summary>
    /// Cria um novo autor. Acesso restrito a administradores.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDto dto)
    {
        var autor = await _authorService.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = autor.Id }, autor);
    }

    /// <summary>
    /// Atualiza um autor existente. Acesso restrito a administradores.
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AuthorUpdateDto dto)
    {
        await _authorService.AtualizarAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Remove um autor. Acesso restrito a administradores.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _authorService.RemoverAsync(id);
        return NoContent();
    }
}