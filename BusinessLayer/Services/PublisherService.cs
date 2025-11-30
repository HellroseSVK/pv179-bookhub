using BusinessLayer.DTOs.Publisher;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class PublisherService(CustomDBContext dbContext) : IPublisherService
{
    public async Task<PublisherDTO> CreatePublisherAsync(PublisherUpsertDTO publisherDto)
    {
        var books = await FindBooks(publisherDto.BookIds);

        var publisher = publisherDto.Adapt<Publisher>();
        publisher.Books = books;

        var created = dbContext.Publishers.Add(publisher);

        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<PublisherDTO>();
    }

    public async Task<List<PublisherDTO>> GetPublishersAsync(int[]? ids = null, bool includeBooks = true)
    {
        var query = GetBaseQuery(includeBooks);

        if (ids is { Length: > 0 })
        {
            query = query.Where(p => ids.Contains(p.Id));
        }

        var publishers = await query.ToListAsync();

        return publishers.Adapt<List<PublisherDTO>>();
    }

    public async Task<PublisherDTO> GetPublisherAsync(int id, bool includeBooks = true)
    {
        var publisher = await FindPublisher(id, includeBooks);

        return publisher.Adapt<PublisherDTO>();
    }

    public async Task<List<PublisherDTO>> SearchPublishersAsync(string searchTerm)
    {
        var query = GetBaseQuery(false);
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p => p.Name.Contains(searchTerm));
        }
        
        var publishers = await query.ToListAsync();
        return publishers.Adapt<List<PublisherDTO>>();
    }

    public async Task PatchPublisherAsync(int id, PublisherPartialDTO publisherDto)
    {
        var publisher = await FindPublisher(id, true);

        publisher.Name = publisherDto.Name ?? publisher.Name;

        if (publisherDto.BookIds != null)
        {
            var books = await FindBooks(publisherDto.BookIds);
            var newBooks = books.Except(publisher.Books);
            publisher.Books.AddRange(newBooks);
        }

        dbContext.Publishers.Update(publisher);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdatePublisherAsync(int id, PublisherUpsertDTO publisherDto)
    {
        var publisher = await FindPublisher(id, true);

        var books = await FindBooks(publisherDto.BookIds);

        publisher.Name = publisherDto.Name;

        var oldBooks = publisher.Books.Except(books).ToList();
        publisher.Books.RemoveAll(b => oldBooks.Contains(b));
        var newBooks = books.Except(publisher.Books).ToList();
        publisher.Books.AddRange(newBooks);

        dbContext.Publishers.Update(publisher);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeletePublisherAsync(int id)
    {
        var publisher = await FindPublisher(id);

        dbContext.Publishers.Remove(publisher);
        await dbContext.SaveChangesAsync();
    }

    private async Task<Publisher> FindPublisher(int publisherId, bool includeBooks = false)
    {
        var query = GetBaseQuery(includeBooks);

        var publisher = await query.FirstOrDefaultAsync(publisher => publisher.Id == publisherId);

        return publisher ?? throw new StatusCodeException(StatusCodes.Status404NotFound,
            $"Publisher with id {publisherId} not found.");
    }

    private IQueryable<Publisher> GetBaseQuery(bool includeBooks)
    {
        IQueryable<Publisher> userQuery = dbContext.Publishers;

        if (includeBooks)
        {
            userQuery = userQuery
                .Include(publisher => publisher.Books);
        }

        return userQuery;
    }

    private async Task<List<Book>> FindBooks(List<int> bookIds)
    {
        var books = await dbContext.Books
            .Where(book => bookIds.Contains(book.Id))
            .ToListAsync();

        var missingBookIds = bookIds.Except(books.Select(b => b.Id)).ToList();
        if (missingBookIds.Any())
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Books with ids {string.Join(", ", missingBookIds)} not found.");
        }

        return books;
    }
}