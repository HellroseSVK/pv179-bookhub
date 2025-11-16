using BusinessLayer.DTOs.Author;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.Exception;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder().AddServices().AddMockedDBContext();

        [Fact]
        public async Task CreateAuthor_ShouldReturnAuthorDto()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            var authorDto = new AuthorUpsertDTO()
            {
                Name = "John",
                Surname = "Pork",
                BookIds = [1, 2]
            };

            // Act
            var created = await authorService.CreateAuthorAsync(authorDto);

            // Assert
            var now = DateTime.UtcNow;

            Assert.NotNull(created);
            Assert.True(created.Id > 0);
            // Assert.InRange(created.CreatedAt, now.AddSeconds(-60), now);
            Assert.Equal(authorDto.Name, created.Name);
            Assert.Equal(authorDto.Surname, created.Surname);
            Assert.Equal(2, created.Books.Count);
        }

        [Fact]
        public async Task CreateAuthor_ShouldThrowException()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            var authorDto = new AuthorUpsertDTO
            {
                Name = "John",
                Surname = "Pork",
                BookIds = [9999] // invalid book id
            };

            // Act + Assert
            await Assert.ThrowsAsync<StatusCodeException>(() =>
                authorService.CreateAuthorAsync(authorDto));
        }

        [Fact]
        public async Task GetAuthor_ShouldReturnAuthorDto()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            // seeded DB should contain Author 1
            const int authorId = 1;

            // Act
            var author = await authorService.GetAuthorAsync(authorId);

            // Assert
            Assert.NotNull(author);
            Assert.Equal(authorId, author.Id);
            Assert.False(string.IsNullOrEmpty(author.Name));
            Assert.NotNull(author.Books);
        }

        [Fact]
        public async Task GetAuthor_ShouldThrowException()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            const int invalidAuthorId = 9999;

            // Act + Assert
            await Assert.ThrowsAsync<StatusCodeException>(() =>
                authorService.GetAuthorAsync(invalidAuthorId));
        }

        [Fact]
        public async Task GetAuthors_ShouldReturnListOfAuthorDto()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            // Act
            var authors = await authorService.GetAuthorsAsync();

            // Assert
            Assert.NotNull(authors);
            Assert.True(authors.Count > 0);
            Assert.All(authors, a =>
            {
                Assert.True(a.Id > 0);
                Assert.False(string.IsNullOrEmpty(a.Name));
            });
        }

        [Fact]
        public async Task PatchAuthor_ShouldUpdateAuthor()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            var patchDto = new AuthorPartialDTO
            {
                Name = "PatchedName"
            };

            const int authorId = 1;

            // Act
            await authorService.PatchAuthorAsync(authorId, patchDto);
            var updated = await authorService.GetAuthorAsync(authorId);

            // Assert
            Assert.Equal("PatchedName", updated.Name);
        }

        [Fact]
        public async Task UpdateAuthor_ShouldUpdateAuthor()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            var updateDto = new AuthorUpsertDTO
            {
                Name = "UpdatedName",
                Surname = "UpdatedSurname",
                BookIds = [1]
            };

            const int authorId = 1;

            // Act
            await authorService.UpdateAuthorAsync(authorId, updateDto);
            var updated = await authorService.GetAuthorAsync(authorId);

            // Assert
            Assert.Equal("UpdatedName", updated.Name);
            Assert.Equal("UpdatedSurname", updated.Surname);
            Assert.Single(updated.Books);
        }

        [Fact]
        public async Task DeleteAuthor_ShouldDeleteAuthor()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            const int authorId = 1;

            // Act
            await authorService.DeleteAuthorAsync(authorId);

            // Assert
            await Assert.ThrowsAsync<StatusCodeException>(() =>
                authorService.GetAuthorAsync(authorId));
        }

        [Fact]
        public async Task DeleteAuthor_ShouldThrowException()
        {
            // Arrange
            var provider = _serviceProviderBuilder.Create();
            var authorService = provider.GetRequiredService<IAuthorService>();

            const int invalidAuthorId = 999999;

            // Act + Assert
            await Assert.ThrowsAsync<StatusCodeException>(() =>
                authorService.DeleteAuthorAsync(invalidAuthorId));
        }
    }
}
