using BusinessLayer.DTOs.Book;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Exception;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class BookServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder =
        new MockedDependencyInjectionBuilder().AddServices().AddMockedDBContext();

    [Fact]
    public async Task CreateBook_ShouldReturnBookDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var bookDto = new BookUpsertDTO
        {
            Title = "Harry",
            Description = "You are a wizard",
            Price = new decimal(10.0),
            PublisherId = 1,
            AuthorIds = [1, 2],
            GenreIds = [1, 2],
        };

        // Act
        var created = await bookService.CreateBookAsync(bookDto);

        // Assert
        var now = DateTime.UtcNow;

        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal(bookDto.Title, created.Title);
        Assert.Equal(bookDto.Description, created.Description);
        Assert.Equal(bookDto.Price, created.Price);
        Assert.NotNull(created.Publisher);
        Assert.Equal(bookDto.PublisherId, created.Publisher.Id);
        Assert.Equal(2, created.Authors.Count);
        Assert.Equal(2, created.Genres.Count);
    }

    [Fact]
    public async Task CreateBook_InvalidPublisher_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var bookDto = new BookUpsertDTO
        {
            Title = "Harry",
            Description = "You are a wizard",
            Price = new decimal(10.0),
            PublisherId = 9999,
            AuthorIds = [1, 2],
            GenreIds = [1, 2],
        };

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            bookService.CreateBookAsync(bookDto));
    }

    [Fact]
    public async Task CreateBook_InvalidAuthor_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var bookDto = new BookUpsertDTO
        {
            Title = "Harry",
            Description = "You are a wizard",
            Price = new decimal(10.0),
            PublisherId = 1,
            AuthorIds = [1, 9999],
            GenreIds = [1, 2],
        };

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            bookService.CreateBookAsync(bookDto));
    }

    [Fact]
    public async Task CreateBook_InvalidGenre_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var bookDto = new BookUpsertDTO
        {
            Title = "Harry",
            Description = "You are a wizard",
            Price = 10.0m,
            PublisherId = 1,
            AuthorIds = [1, 2],
            GenreIds = [1, 9999],
        };

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            bookService.CreateBookAsync(bookDto));
    }

    [Fact]
    public async Task GetBook_ShouldReturnBookDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        // seeded DB should contain Book 1
        const int bookId = 1;

        // Act
        var book = await bookService.GetBookAsync(bookId);

        // Assert
        Assert.NotNull(book);
        Assert.Equal(bookId, book.Id);
        Assert.False(string.IsNullOrEmpty(book.Title));
        Assert.False(string.IsNullOrEmpty(book.Description));
        Assert.True(book.Price > 0);
        Assert.NotNull(book.Publisher);
        Assert.NotNull(book.Authors);
        Assert.NotNull(book.Genres);
    }

    [Fact]
    public async Task GetBook_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        const int invalidBookId = 9999;

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            bookService.GetBookAsync(invalidBookId));
    }

    [Fact]
    public async Task GetBooks_ShouldReturnListOfBookDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        // Act
        var books = await bookService.FilterBooksAsync(new BookFilterDTO());

        // Assert
        Assert.NotNull(books);
        Assert.True(books.Count > 0);
        Assert.All(books, a =>
        {
            Assert.True(a.Id > 0);
            Assert.False(string.IsNullOrEmpty(a.Title));
        });
    }

    [Fact]
    public async Task PatchBook_ShouldUpdateBook()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var patchDto = new BookPartialDTO
        {
            Title = "PatchedTitle"
        };

        const int bookId = 1;

        // Act
        await bookService.PatchBookAsync(bookId, patchDto);
        var updated = await bookService.GetBookAsync(bookId);

        // Assert
        Assert.Equal("PatchedTitle", updated.Title);
    }

    [Fact]
    public async Task UpdateBook_ShouldUpdateBook()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var updateDto = new BookUpsertDTO
        {
            Title = "UpdatedTitle",
            Description = "UpdatedDescription",
            Price = 20.0m,
            PublisherId = 2,
            AuthorIds = [2],
            GenreIds = [2],
        };

        const int bookId = 1;

        // Act
        await bookService.UpdateBookAsync(bookId, updateDto);
        var updated = await bookService.GetBookAsync(bookId);

        // Assert
        Assert.Equal("UpdatedTitle", updated.Title);
        Assert.Equal("UpdatedDescription", updated.Description);
        Assert.Equal(20.0m, updated.Price);
        Assert.NotNull(updated.Publisher);
        Assert.Equal(2, updated.Publisher.Id);
        Assert.Single(updated.Authors);
        Assert.Single(updated.Genres);
    }

    [Fact]
    public async Task DeleteBook_ShouldDeleteBook()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        const int bookId = 1;

        // Act
        await bookService.DeleteBookAsync(bookId);

        // Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            bookService.GetBookAsync(bookId));
    }

    [Fact]
    public async Task DeleteBook_ShouldThrowException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        const int invalidBookId = 999999;

        // Act + Assert
        await Assert.ThrowsAsync<StatusCodeException>(() =>
            bookService.DeleteBookAsync(invalidBookId));
    }
}