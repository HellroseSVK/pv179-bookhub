using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.DTOs.Book;
using MVC.Models.Book;
using Mapster;

namespace MVC.Controllers;

public class BooksController(IBookService bookService) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
    {
        var books = await bookService.FilterBooksAsync(new BookFilterDTO());
        var totalBooks = books.Count;
        var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);

        var paginatedBooks = books
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(b => b.Adapt<BookViewModel>())
            .ToList();

        var viewModel = new BookListViewModel
        {
            Books = paginatedBooks,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalBooks = totalBooks
        };

        return View(viewModel);
    }
}