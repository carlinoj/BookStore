using BookStore.Application.DTOs.Book;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Moq;
using Xunit;

namespace BookStore.Tests.Services
{
    public class BookServiceTests
    {
        [Fact]
        public async Task CriarAsync_DeveCriarLivroQuandoAutorExiste()
        {
            var dto = new BookCreateDto
            {
                Titulo = "Livro Teste",
                Categoria = "Ficcao",
                DataPublicacao = DateTime.Now,
                Preco = 29.99m,
                QuantidadeEstoque = 5,
                AuthorId = Guid.NewGuid()
            };

            var autor = new Author { Id = dto.AuthorId, Nome = "Autor Y" };

            var bookRepoMock = new Mock<IRepository<Book>>();
            var authorRepoMock = new Mock<IRepository<Author>>();
            authorRepoMock.Setup(r => r.GetByIdAsync(dto.AuthorId)).ReturnsAsync(autor);

            var service = new BookService(bookRepoMock.Object, authorRepoMock.Object);
            var book = await service.CriarAsync(dto);

            Assert.Equal(dto.Titulo, book.Titulo);
            Assert.Equal(dto.AuthorId, book.AuthorId);
            bookRepoMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
            bookRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CriarAsync_DeveLancarExcecaoSeAutorNaoExiste()
        {
            var dto = new BookCreateDto { Titulo = "Livro", AuthorId = Guid.NewGuid() };
            var bookRepoMock = new Mock<IRepository<Book>>();
            var authorRepoMock = new Mock<IRepository<Author>>();
            authorRepoMock.Setup(r => r.GetByIdAsync(dto.AuthorId)).ReturnsAsync((Author?)null);
            var service = new BookService(bookRepoMock.Object, authorRepoMock.Object);

            await Assert.ThrowsAsync<Exception>(() => service.CriarAsync(dto));
        }
    }
}