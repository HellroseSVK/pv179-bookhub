using BusinessLayer.DTOs.Author;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthorService
{
    public Task<AuthorSimplifiedDTO> CreateAuthorAsync(AuthorSimplifiedDTO author);

    public Task<List<AuthorSimplifiedDTO>> GetAuthorsAsync(
        int[]? ids = null,
        bool includeBookAuthors = true);

    public Task<AuthorSimplifiedDTO> GetAuthorAsync(
        int id,
        bool includeBookAuthors = true);

    public Task PatchAuthorAsync(int id, AuthorSimplifiedPartialDTO authorDto);
    public Task UpdateAuthorAsync(int id, AuthorDTO authorDto);
    public Task DeleteAuthorAsync(int id);
}