using BusinessLayer.DTOs.Book;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Extensions;

public static class BookQueryExtensions
{
    public static IQueryable<Book> Paginate(this IQueryable<Book> bookQuery, int pageNumber, int pageSize)
    {
        return bookQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static IQueryable<Book> IncludeVirtuals(this IQueryable<Book> query,
        bool includeWishlistItems = false,
        bool includeOrderItems = false)
    {
        IQueryable<Book> result = query
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
            .Include(b => b.Publisher)
            .Include(b => b.Reviews);

        if (includeWishlistItems)
        {
            result = result.Include(b => b.WishlistItems);
        }

        if (includeOrderItems)
        {
            result = result.Include(b => b.OrderItems);
        }

        return result;
    }

    public static IQueryable<Book> ApplyBookFilter(this IQueryable<Book> query, BookFilterDTO filter)
    {
        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            var searchTermLower = filter.SearchTerm.ToLower();
            query = query.Where(b => b.Title.ToLower().Contains(searchTermLower) ||
                               b.Description.ToLower().Contains(searchTermLower) ||
                               b.BookAuthors.Any(ba => ba.Author != null &&
                                                     ((ba.Author.Name + " " + ba.Author.Surname).ToLower().Contains(searchTermLower) ||
                                                     (ba.Author.Surname + " " + ba.Author.Name).ToLower().Contains(searchTermLower) ||
                                                     ba.Author.Name.ToLower().Contains(searchTermLower) ||
                                                     ba.Author.Surname.ToLower().Contains(searchTermLower))) ||
                               b.BookGenres.Any(bg => bg.Genre != null && bg.Genre.Name.ToLower().Contains(searchTermLower)) ||
                               (b.Publisher != null && b.Publisher.Name.ToLower().Contains(searchTermLower)));
        }

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(b => b.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(b => b.Description.Contains(filter.Description));
        }

        if (filter.PriceMin.HasValue)
        {
            query = query.Where(b => b.Price >= filter.PriceMin.Value);
        }

        if (filter.PriceMax.HasValue)
        {
            query = query.Where(b => b.Price <= filter.PriceMax.Value);
        }

        if (filter.RatingMin.HasValue)
        {
            query = query
                .Where(b => b.Reviews.Any())
                .Where(b => b.Reviews.Average(r => r.Rating) >= (double)filter.RatingMin.Value);
        }

        if (filter.AuthorIds is { Count: > 0 })
        {
            query = query.Where(b => b.BookAuthors.Any(ba => filter.AuthorIds.Contains(ba.AuthorId)));
        }

        if (filter.GenreIds is { Count: > 0 })
        {
            query = query.Where(b => b.BookGenres.Any(bg => filter.GenreIds.Contains(bg.GenreId)));
        }

        if (filter.PublisherIds is { Count: > 0 })
        {
            query = query.Where(b => filter.PublisherIds.Contains(b.PublisherId));
        }

        return query;
    }
}