using BusinessLayer.DTOs.Author;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthorService
{
    public Task<AuthorDTO> CreateAuthorAsync(AuthorCreateDTO authorDto);

    public Task<List<AuthorDTO>> GetAuthorsAsync(int[]? ids = null, bool includeBookAuthors = true);

    public Task<AuthorDTO> GetAuthorAsync(int id, bool includeBookAuthors = true);

    public Task PatchAuthorAsync(int id, AuthorPartialDTO authorDto);
    public Task UpdateAuthorAsync(int id, AuthorUpdateDTO authorDto);
    public Task DeleteAuthorAsync(int id);
}