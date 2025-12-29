using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.DTOs.Book;
using MVC.Models.Book;
using Mapster;

namespace MVC.Controllers;

public class BooksController(IBookService bookService, IGenreService genreService, IAuthorService authorService) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 12, int? genreId = null, int? authorId = null)
    {
        var filter = new BookFilterDTO();
        if (genreId.HasValue)
        {
            filter.GenreIds = [genreId.Value];
        }
        if (authorId.HasValue)
        {
            filter.AuthorIds = [authorId.Value];
        }

        var books = await bookService.FilterBooksAsync(filter);
        var totalBooks = books.Count;
        var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

        var paginatedBooks = books
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => b.Adapt<BookViewModel>())
            .ToList();

        var allGenres = await genreService.GetGenreAsync(includeBookGenres: false);
        var allAuthors = await authorService.GetAuthorsAsync(includeBookAuthors: false);

        var viewModel = new BookListViewModel
        {
            Books = paginatedBooks,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalBooks = totalBooks,
            Genres = allGenres.Select(g => g.Adapt<GenreViewModel>()).ToList(),
            SelectedGenreId = genreId,
            Authors = allAuthors.Select(a => a.Adapt<AuthorViewModel>()).ToList(),
            SelectedAuthorId = authorId
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var bookDto = await bookService.GetBookAsync(id, includeAuthors: true, includeGenres: true, includePublisher: true, includeReviews: true);
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
}