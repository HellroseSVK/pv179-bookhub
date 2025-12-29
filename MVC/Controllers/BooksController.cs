using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.DTOs.Book;
using MVC.Models.Book;
using Mapster;
using MVC.Models.Author;
using MVC.Models.Genre;

namespace MVC.Controllers;

public class BooksController(IBookFacade bookFacade, IGenreService genreService, IAuthorService authorService) : Controller
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

        var pagedResult = await bookFacade.FilterBooksPaginatedAsync(filter);

        var allGenres = await genreService.GetGenreAsync(includeBookGenres: false);
        var allAuthors = await authorService.GetAuthorsAsync(includeBookAuthors: false);

        var viewModel = new BookListViewModel
        {
            Items = pagedResult.Items.Select(b => b.Adapt<BookViewModel>()).ToList(),
            CurrentPage = pagedResult.PageNumber,
            TotalPages = pagedResult.TotalPages,
            PageSize = pagedResult.PageSize,
            TotalItems = pagedResult.TotalCount,
            Genres = allGenres.Select(g => g.Adapt<GenreSimplifiedViewModel>()).ToList(),
            SelectedGenreId = genreId,
            Authors = allAuthors.Select(a => a.Adapt<AuthorSimplifiedViewModel>()).ToList(),
            SelectedAuthorId = authorId
        };

        return View(viewModel);
    }
}