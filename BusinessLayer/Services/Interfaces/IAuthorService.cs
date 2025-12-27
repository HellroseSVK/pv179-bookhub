using BusinessLayer.DTOs.Author;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthorService
{
    public Task<AuthorDTO> CreateAuthorAsync(AuthorUpsertDTO authorDto);

    public Task<List<AuthorDTO>> GetAuthorsAsync(int[]? ids = null, bool includeBookAuthors = true);

    public Task<AuthorDTO> GetAuthorAsync(int id, bool includeBookAuthors = true);

    public Task<List<AuthorDTO>> SearchAuthorsAsync(string searchTerm);

    public Task PatchAuthorAsync(int id, AuthorPartialDTO authorDto);
    public Task UpdateAuthorAsync(int id, AuthorUpsertDTO authorDto);
    public Task DeleteAuthorAsync(int id);
}