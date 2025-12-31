using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.DTOs.Book;
using MVC.Models.Book;
using Mapster;
using MVC.Models.Author;
using MVC.Models.Genre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Entities;
using BusinessLayer.DTOs.Review;
using Shared.Exception;
using Microsoft.Extensions.Caching.Memory;

namespace MVC.Controllers;

public class BooksController(IBookFacade bookFacade, IGenreService genreService, IAuthorService authorService, IReviewService reviewService, UserManager<LocalIdentityUser> userManager, IMemoryCache cache) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 12, int? genreId = null, int? authorId = null)
    {
        var filter = new BookFilterDTO
        {
            PageNumber = page,
            PageSize = pageSize
        };
        if (genreId.HasValue)
        {
            filter.GenreIds = [genreId.Value];
        }
        if (authorId.HasValue)
        {
            filter.AuthorIds = [authorId.Value];
        }

        // Cache paginated book results with short TTL
        var booksCacheKey = $"Books:Filter:{genreId?.ToString() ?? "0"}:{authorId?.ToString() ?? "0"}:{page}:{pageSize}";
        var pagedResult = await cache.GetOrCreateAsync(booksCacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
            return await bookFacade.FilterBooksPaginatedAsync(filter);
        });

        // Cache sidebar lists (long TTL)
        var allGenres = await cache.GetOrCreateAsync("Genres:All", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            return await genreService.GetGenreAsync(includeBookGenres: false);
        });

        var allAuthors = await cache.GetOrCreateAsync("Authors:All", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            return await authorService.GetAuthorsAsync(includeBookAuthors: false);
        });

        var viewModel = new BookListViewModel
        {
            Items = pagedResult!.Items.Select(b => b.Adapt<BookViewModel>()).ToList(),
            CurrentPage = pagedResult.PageNumber,
            TotalPages = pagedResult.TotalPages,
            PageSize = pagedResult.PageSize,
            TotalItems = pagedResult.TotalCount,
            Genres = allGenres!.Select(g => g.Adapt<GenreSimplifiedViewModel>()).ToList(),
            SelectedGenreId = genreId,
            Authors = allAuthors!.Select(a => a.Adapt<AuthorSimplifiedViewModel>()).ToList(),
            SelectedAuthorId = authorId
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var bookDto = await bookFacade.GetBookAsync(id, includeAuthors: true, includeGenres: true, includePublisher: true, includeReviews: true);
        if (bookDto == null)
        {
            return NotFound();
        }

        var viewModel = new BookDetailsViewModel
        {
            Book = bookDto.Adapt<BookViewModel>(),
            Reviews = bookDto.Reviews ?? new List<BusinessLayer.DTOs.Review.ReviewSimplifiedDTO>()
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReview(int bookId, int rating, string body)
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null)
        {
            return Unauthorized();
        }

        var dto = new ReviewUpsertDTO
        {
            BookId = bookId,
            Rating = rating,
            Body = body,
            UserId = identityUser.UserId
        };

        try
        {
            await reviewService.CreateReviewAsync(dto);
            TempData["SuccessMessage"] = "Review submitted.";

            // Invalidate cached book details and paginated lists that could be affected
            cache.Remove($"Book:Details:{bookId}");
            // Optionally remove filtered lists - simple approach: remove all Books:Filter* entries by a known pattern isn't directly supported by IMemoryCache
            // For now rely on small TTL; for eager invalidation consider tracking keys or using a distributed cache that supports scanning.
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Details", new { id = bookId });
    }
}