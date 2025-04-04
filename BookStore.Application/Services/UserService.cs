using BookStore.Application.DTOs.User;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Application.Services;

public class UserService
{
    private readonly IRepository<User> _repository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IRepository<User> repository, IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> CadastrarUsuarioAsync(UserRegisterDto dto)
    {
        var user = new User
        {
            Nome = dto.Nome,
            Email = dto.Email
        };

        user.SenhaHash = _passwordHasher.HashPassword(user, dto.Senha);

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        return user;
    }
}