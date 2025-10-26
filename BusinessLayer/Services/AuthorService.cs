using BusinessLayer.DTOs.Author;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI.Exception;

namespace BusinessLayer.Services;

public class AuthorService(CustomDBContext dBContext) : IAuthorService
{
    public async Task<AuthorSimplifiedDTO> CreateAuthorAsync(AuthorSimplifiedDTO author)
    {
        var created = dBContext.Authors.Add(author.Adapt<Author>());

        await dBContext.SaveChangesAsync();

        return created.Entity.Adapt<AuthorSimplifiedDTO>();
    }

    public async Task<List<AuthorSimplifiedDTO>> GetAuthorsAsync(int[]? ids = null, bool includeBookAuthors = true)
    {
        var query = GetBaseQuery(includeBookAuthors);

        if (ids is { Length: > 0 })
        {
            query = query.Where(a => ids.Contains(a.Id));
        }

        var authors = await query.ToListAsync();

        return authors.Adapt<List<AuthorSimplifiedDTO>>();
    }

    public async Task<AuthorSimplifiedDTO> GetAuthorAsync(int id, bool includeBookAuthors = true)
    {
        var author = await FindAuthor(id, includeBookAuthors);

        return author.Adapt<AuthorSimplifiedDTO>();
    }

    public async Task PatchAuthorAsync(int id, AuthorSimplifiedPartialDTO authorDto)
    {
        var author = await FindAuthor(id);

        author.Name = authorDto.Name ?? author.Name;
        author.Surname = authorDto.Surname ?? author.Surname;

        dBContext.Authors.Update(author);

        await dBContext.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(int id, AuthorDTO authorDto)
    {
        var author = await FindAuthor(id);
        
        author.Name = authorDto.Name;
        author.Surname = authorDto.Surname;
        
        dBContext.Authors.Update(author);
        
        await dBContext.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await FindAuthor(id);
        
        dBContext.Authors.Remove(author);
        
        await dBContext.SaveChangesAsync();
    }

    private async Task<Author> FindAuthor(int authorId, bool includeBookAuthors = false)
    {
        var query = GetBaseQuery(includeBookAuthors);

        var author = await query.FirstOrDefaultAsync(author => author.Id == authorId);

        return author ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"Author with id {authorId} not found.");
    }

    private IQueryable<Author> GetBaseQuery(bool includeBookAuthors)
    {
        IQueryable<Author> userQuery = dBContext.Authors;

        if (includeBookAuthors)
        {
            userQuery = userQuery
                .Include(author => author.BookAuthors);
        }

        return userQuery;
    }
}