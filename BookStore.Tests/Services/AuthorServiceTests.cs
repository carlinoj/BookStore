using BookStore.Application.DTOs.Author;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Moq;
using Xunit;

namespace BookStore.Tests.Services
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task CriarAsync_DeveAdicionarAutorComSucesso()
        {
            var dto = new AuthorCreateDto { Nome = "Autor X", DataNascimento = new DateTime(1980, 1, 1), Pais = "Brasil" };
            var repoMock = new Mock<IRepository<Author>>();
            var service = new AuthorService(repoMock.Object);

            var result = await service.CriarAsync(dto);

            Assert.Equal(dto.Nome, result.Nome);
            Assert.Equal(dto.DataNascimento, result.DataNascimento);
            Assert.Equal(dto.Pais, result.Pais);
            repoMock.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Once);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}