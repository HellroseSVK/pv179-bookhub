using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Mapster;
using MVC.Models.Book;

namespace MVC.Controllers;

public class AdminController(IBookService bookService, IBookFacade bookFacade) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Books(int page = 1, int pageSize = 12)
    {
        var paginatedBooks = await bookService.FilterBooksPaginatedAsync(new BookFilterDTO
        { PageNumber = page, PageSize = pageSize });

        var viewModel = new BookListViewModel
        {
            Books = paginatedBooks.Items.Select(b => b.Adapt<BookViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedBooks.TotalPages,
            PageSize = pageSize,
            TotalBooks = paginatedBooks.TotalPages,
            Genres = [],
            SelectedGenreId = null,
            Authors = [],
            SelectedAuthorId = null
        };

        return View(viewModel);
    }

    public async Task<IActionResult> BookUpsert(int? bookId = null)
    {
        var viewModel = new BookViewModel();

        if (!bookId.HasValue)
        {
            return View(viewModel);
        }

        var book = await bookService.GetBookAsync(bookId.Value, includeReviews: false, includeWishlistItems: false,
            includeOrderItems: false);
        viewModel = book.Adapt<BookViewModel>();


        return View(viewModel);
    }

    public async Task<IActionResult> BookDelete(int bookId)
    {
        var book = await bookService.GetBookAsync(bookId, includePublisher: false, includeAuthors: false,
            includeGenres: false, includeReviews: false, includeWishlistItems: false,
            includeOrderItems: false);
        var viewModel = book.Adapt<BookViewModel>();

        await bookFacade.DeleteBookAsync(bookId);

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}