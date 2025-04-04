using BookStore.Application.DTOs.Sales;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

/// <summary>
/// Controlador para compras de livros.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly SaleService _saleService;

    public SalesController(SaleService compraService)
    {
        _saleService = compraService;
    }

    /// <summary>
    /// Realiza a compra de um livro, diminuindo o estoque.
    /// </summary>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Comprar([FromBody] BookSalesDto dto)
    {
        try
        {
            await _saleService.RealizarCompraAsync(dto);
 
            return Ok(new { mensagem = "Compra realizada com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}