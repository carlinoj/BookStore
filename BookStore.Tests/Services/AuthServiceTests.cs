using BookStore.Application.DTOs.User;
using BookStore.Application.Services;
using BookStore.Application.Settings;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace BookStore.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task AutenticarAsync_DeveRetornarTokenValidoSeCredenciaisCorretas()
        {
            var user = new User { Id = Guid.NewGuid(), Nome = "Joao", Email = "joao@email.com", SenhaHash = "hashed", Role = "Admin" };
            var dto = new UserLoginDto { Email = user.Email, Senha = "123456" };

            var repoMock = new Mock<IRepository<User>>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User> { user });

            var hasherMock = new Mock<IPasswordHasher<User>>();
            hasherMock.Setup(h => h.VerifyHashedPassword(user, user.SenhaHash, dto.Senha))
                      .Returns(PasswordVerificationResult.Success);

            var jwtOptions = Options.Create(new JwtSettings
            {
                Secret = "mysupersecretkey1234567890",
                Issuer = "BookStoreAPI",
                Audience = "BookStoreClient",
                ExpirationMinutes = 60
            });

            var service = new AuthService(repoMock.Object, hasherMock.Object, jwtOptions);
            var token = await service.AutenticarAsync(dto);

            Assert.False(string.IsNullOrWhiteSpace(token));
        }

        [Fact]
        public async Task AutenticarAsync_DeveLancarExcecaoSeEmailNaoEncontrado()
        {
            var dto = new UserLoginDto { Email = "inexistente@email.com", Senha = "123" };

            var repoMock = new Mock<IRepository<User>>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());

            var service = new AuthService(repoMock.Object, Mock.Of<IPasswordHasher<User>>(), Options.Create(new JwtSettings()));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.AutenticarAsync(dto));
        }
    }
}