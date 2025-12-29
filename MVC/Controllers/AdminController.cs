using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Mapster;
using MVC.Models.Book;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers;

public class AdminController(
    IBookService bookService,
    IBookFacade bookFacade,
    IAuthorService authorService,
    IGenreService genreService,
    IPublisherService publisherService) : Controller
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

    [HttpGet]
    public async Task<IActionResult> BookUpsert(int? bookId = null)
    {
        var viewModel = new BookUpsertViewModel();
        await PopulateDropdownsAsync(viewModel);

        if (bookId.HasValue)
        {
            var book = await bookService.GetBookAsync(bookId.Value,
                includeReviews: false, includeWishlistItems: false, includeOrderItems: false);

            viewModel.Id = book.Id;
            viewModel.Title = book.Title;
            viewModel.Description = book.Description;
            viewModel.Price = book.Price;
            viewModel.CurrentImageUrl = book.ImageUrl;

            if (book.Publisher != null)
                viewModel.SelectedPublisherId = book.Publisher.Id;

            var primaryGenre = book.Genres.FirstOrDefault(g => g.IsPrimary);
            if (primaryGenre != null)
                viewModel.SelectedPrimaryGenreId = primaryGenre.Id;

            viewModel.SelectedSecondaryGenreIds = book.Genres
                .Where(g => !g.IsPrimary)
                .Select(g => g.Id)
                .ToList();

            viewModel.SelectedAuthorIds = book.Authors.Select(a => a.Id).ToList();
        }

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookUpsert(BookUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }

        var bookDto = new BookUpsertDTO
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Price = viewModel.Price,
            PublisherId = viewModel.SelectedPublisherId,
            AuthorIds = viewModel.SelectedAuthorIds,
            GenreIds = new List<int> { viewModel.SelectedPrimaryGenreId }
                .Concat(viewModel.SelectedSecondaryGenreIds)
                .Distinct()
                .ToList()
        };

        try
        {
            if (!viewModel.Id.HasValue)
            {
                await bookFacade.CreateBookWithImageAsync(bookDto, viewModel.ImageFile);
                TempData["SuccessMessage"] = "Book created successfully!";
            }
            else
            {
                await bookFacade.UpdateBookAsync(viewModel.Id.Value, bookDto);

                if (viewModel.ImageFile != null)
                    await bookFacade.UploadBookImageAsync(viewModel.Id.Value, viewModel.ImageFile);

                TempData["SuccessMessage"] = "Book updated successfully!";
            }

            return RedirectToAction(nameof(Books));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving book: {ex.Message}");
            await PopulateDropdownsAsync(viewModel);
            return View(viewModel);
        }
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

    private async Task PopulateDropdownsAsync(BookUpsertViewModel viewModel)
    {
        var authorsTask = authorService.GetAuthorsAsync(includeBookAuthors: false);
        var genresTask = genreService.GetGenreAsync(includeBookGenres: false);
        var publishersTask = publisherService.GetPublishersAsync(includeBooks: false);

        await Task.WhenAll(authorsTask, genresTask, publishersTask);

        viewModel.Authors = authorsTask.Result
            .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = $"{a.Name} {a.Surname}" })
            .OrderBy(a => a.Text)
            .ToList();

        viewModel.Genres = genresTask.Result
            .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
            .OrderBy(g => g.Text)
            .ToList();

        viewModel.Publishers = publishersTask.Result
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
            .OrderBy(p => p.Text)
            .ToList();
    }
}