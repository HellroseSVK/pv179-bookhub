using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using MVC.Models.Book;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models.Genre;

namespace MVC.Controllers;

[Authorize(Roles = "Admin")]
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
            Items = paginatedBooks.Items.Select(b => b.Adapt<BookViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedBooks.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedBooks.TotalCount,
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
        await PopulateBookDropdownsAsync(viewModel);

        if (!bookId.HasValue)
        {
            return View(viewModel);
        }

        var book = await bookFacade.GetBookAsync(bookId.Value,
            includeReviews: false, includeWishlistItems: false, includeOrderItems: false);

        viewModel.Id = book.Id;
        viewModel.Title = book.Title;
        viewModel.Description = book.Description;
        viewModel.Price = book.Price;
        viewModel.CurrentImageUrl = book.ImageUrl;

        if (book.Publisher != null)
        {
            viewModel.SelectedPublisherId = book.Publisher.Id;
        }

        var primaryGenre = book.Genres.FirstOrDefault(g => g.IsPrimary);
        if (primaryGenre != null)
        {
            viewModel.SelectedPrimaryGenreId = primaryGenre.Id;
        }

        viewModel.SelectedSecondaryGenreIds = book.Genres
            .Where(g => !g.IsPrimary)
            .Select(g => g.Id)
            .ToList();

        viewModel.SelectedAuthorIds = book.Authors.Select(a => a.Id).ToList();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookUpsert(BookUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateBookDropdownsAsync(viewModel);
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
            await PopulateBookDropdownsAsync(viewModel);
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

    public async Task<IActionResult> Genres(int page = 1, int pageSize = 12)
    {
        var paginatedGenres = await genreService.GetGenrePaginatedAsync(new PaginationParameters
        { PageNumber = page, PageSize = pageSize });

        var viewModel = new GenreListViewModel
        {
            Items = paginatedGenres.Items.Select(b => b.Adapt<GenreSimplifiedViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedGenres.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedGenres.TotalCount,
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GenreUpsert(int? genreId = null)
    {
        var viewModel = new GenreUpsertViewModel();
        await PopulateGenreDropdownsAsync(viewModel);

        if (!genreId.HasValue)
        {
            return View(viewModel);
        }

        var genre = await genreService.GetGenreAsync(genreId.Value, includeBookGenres: false);

        viewModel.Id = genre.Id;
        viewModel.Name = genre.Name;

        viewModel.SelectedBooksIds = genre.Books
            .Select(b => b.Id)
            .ToList();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenreUpsert(GenreUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateGenreDropdownsAsync(viewModel);
            return View(viewModel);
        }

        var genreDto = new GenreUpsertDTO
        {
            Name = viewModel.Name,
            BookIds = viewModel.SelectedBooksIds,
        };

        try
        {
            if (!viewModel.Id.HasValue)
            {
                await genreService.CreateGenreAsync(genreDto);
                TempData["SuccessMessage"] = "Genre created successfully!";
            }
            else
            {
                await genreService.UpdateGenreAsync(viewModel.Id.Value, genreDto);
                TempData["SuccessMessage"] = "Genre updated successfully!";
            }

            return RedirectToAction(nameof(Genres));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving genre: {ex.Message}");
            await PopulateGenreDropdownsAsync(viewModel);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> GenreDelete(int genreId)
    {
        var genre = await genreService.GetGenreAsync(genreId, includeBookGenres: false);
        var viewModel = genre.Adapt<GenreSimplifiedViewModel>();

        await genreService.DeleteGenreAsync(genreId);

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task PopulateBookDropdownsAsync(BookUpsertViewModel viewModel)
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

    private async Task PopulateGenreDropdownsAsync(GenreUpsertViewModel viewModel)
    {
        var books = await bookService.FilterBooksAsync(new BookFilterDTO());

        viewModel.Books = books
            .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title })
            .OrderBy(a => a.Text)
            .ToList();
    }
}