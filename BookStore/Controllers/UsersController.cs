using BookStore.Application.DTOs.User;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var user = await _userService.CadastrarUsuarioAsync(dto);

        return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
    }
}