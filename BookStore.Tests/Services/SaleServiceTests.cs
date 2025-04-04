using BookStore.Application.DTOs.Sales;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Moq;
using Xunit;

namespace BookStore.Tests.Services
{
    public class SaleServiceTests
    {
        [Fact]
        public async Task RealizarCompraAsync_DeveDiminuirEstoqueSeValido()
        {
            var livro = new Book { Id = Guid.NewGuid(), QuantidadeEstoque = 10 };
            var dto = new BookSalesDto { LivroId = livro.Id, Quantidade = 3 };

            var repoMock = new Mock<IRepository<Book>>();
            repoMock.Setup(r => r.GetByIdAsync(dto.LivroId)).ReturnsAsync(livro);

            var service = new SaleService(repoMock.Object);
            await service.RealizarCompraAsync(dto);

            Assert.Equal(7, livro.QuantidadeEstoque);
            repoMock.Verify(r => r.Update(It.Is<Book>(b => b.QuantidadeEstoque == 7)), Times.Once);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RealizarCompraAsync_DeveFalharComEstoqueInsuficiente()
        {
            var livro = new Book { Id = Guid.NewGuid(), QuantidadeEstoque = 2 };
            var dto = new BookSalesDto { LivroId = livro.Id, Quantidade = 5 };

            var repoMock = new Mock<IRepository<Book>>();
            repoMock.Setup(r => r.GetByIdAsync(dto.LivroId)).ReturnsAsync(livro);
            var service = new SaleService(repoMock.Object);

            var ex = await Assert.ThrowsAsync<Exception>(() => service.RealizarCompraAsync(dto));
            Assert.Equal("Estoque insuficiente.", ex.Message);
        }
    }
}