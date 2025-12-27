using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.DTOs.Book;
using MVC.Models.Book;
using Mapster;

namespace MVC.Controllers;

public class BooksController(IBookService bookService, IGenreService genreService) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 12, int? genreId = null)
    {
        var filter = new BookFilterDTO();
        if (genreId.HasValue)
        {
            filter.GenreIds = [genreId.Value];
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

        var viewModel = new BookListViewModel
        {
            Books = paginatedBooks,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalBooks = totalBooks,
            Genres = allGenres.Select(g => g.Adapt<GenreViewModel>()).ToList(),
            SelectedGenreId = genreId
        };

        return View(viewModel);
    }
}