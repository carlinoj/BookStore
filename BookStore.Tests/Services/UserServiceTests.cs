using BookStore.Application.DTOs.User;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BookStore.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IRepository<User>> _repoMock;
    private readonly Mock<IPasswordHasher<User>> _hasherMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _repoMock = new Mock<IRepository<User>>();
        _hasherMock = new Mock<IPasswordHasher<User>>();

        _service = new UserService(_repoMock.Object, _hasherMock.Object);
    }

    [Fact]
    public async Task CadastrarUsuarioAsync_DeveCriarUsuarioComHash()
    {
        // Arrange
        var dto = new UserRegisterDto
        {
            Nome = "Maria",
            Email = "maria@email.com",
            Senha = "123456"
        };

        var hashEsperado = "HASH123";

        _hasherMock.Setup(h => h.HashPassword(It.IsAny<User>(), dto.Senha))
                   .Returns(hashEsperado);

        // Act
        var result = await _service.CadastrarUsuarioAsync(dto);

        // Assert
        Assert.Equal(dto.Nome, result.Nome);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(hashEsperado, result.SenhaHash);

        _repoMock.Verify(r => r.AddAsync(It.Is<User>(u =>
            u.Nome == dto.Nome &&
            u.Email == dto.Email &&
            u.SenhaHash == hashEsperado
        )), Times.Once);

        _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}