using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.DTOs.Book;
using MVC.Models.Book;
using Mapster;
using MVC.Models.Author;
using MVC.Models.Genre;

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
            Items = paginatedBooks,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalItems = totalBooks,
            Genres = allGenres.Select(g => g.Adapt<GenreSimplifiedViewModel>()).ToList(),
            SelectedGenreId = genreId,
            Authors = allAuthors.Select(a => a.Adapt<AuthorSimplifiedViewModel>()).ToList(),
            SelectedAuthorId = authorId
        };

        return View(viewModel);
    }
}