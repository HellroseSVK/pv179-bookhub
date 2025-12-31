using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Text;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.Order;
using BusinessLayer.DTOs.Publisher;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MVC.Models.Book;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using MVC.Models.Author;
using MVC.Models.Genre;
using MVC.Models.GiftCard;
using MVC.Models.Order;
using MVC.Models.Publisher;
using MVC.Models.User;
using Shared.Exception;

namespace MVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(
    IBookService bookService,
    IBookFacade bookFacade,
    IAuthorService authorService,
    IGenreService genreService,
    IPublisherService publisherService,
    IUserService userService,
    IGiftCardService giftCardService,
    IOrderService orderService,
    UserManager<LocalIdentityUser> userManager) : Controller
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
            Items = paginatedGenres.Items.Select(g => g.Adapt<GenreSimplifiedViewModel>()).ToList(),
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

    public async Task<IActionResult> Authors(int page = 1, int pageSize = 12)
    {
        var paginatedAuthors = await authorService.GetAuthorsPaginatedAsync(new PaginationParameters
        { PageNumber = page, PageSize = pageSize });

        var viewModel = new AuthorListViewModel
        {
            Items = paginatedAuthors.Items.Select(a => a.Adapt<AuthorSimplifiedViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedAuthors.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedAuthors.TotalCount,
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AuthorUpsert(int? authorId = null)
    {
        var viewModel = new AuthorUpsertViewModel();
        await PopulateAuthorDropdownsAsync(viewModel);

        if (!authorId.HasValue)
        {
            return View(viewModel);
        }

        var author = await authorService.GetAuthorAsync(authorId.Value, includeBookAuthors: false);

        viewModel.Id = author.Id;
        viewModel.Name = author.Name;
        viewModel.Surname = author.Surname;

        viewModel.SelectedBooksIds = author.Books
            .Select(b => b.Id)
            .ToList();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AuthorUpsert(AuthorUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateAuthorDropdownsAsync(viewModel);
            return View(viewModel);
        }

        var authorDto = new AuthorUpsertDTO()
        {
            Name = viewModel.Name,
            Surname = viewModel.Surname,
            BookIds = viewModel.SelectedBooksIds,
        };

        try
        {
            if (!viewModel.Id.HasValue)
            {
                await authorService.CreateAuthorAsync(authorDto);
                TempData["SuccessMessage"] = "Author created successfully!";
            }
            else
            {
                await authorService.UpdateAuthorAsync(viewModel.Id.Value, authorDto);
                TempData["SuccessMessage"] = "Author updated successfully!";
            }

            return RedirectToAction(nameof(Authors));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving author: {ex.Message}");
            await PopulateAuthorDropdownsAsync(viewModel);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> AuthorDelete(int authorId)
    {
        var author = await authorService.GetAuthorAsync(authorId, includeBookAuthors: false);
        var viewModel = author.Adapt<AuthorSimplifiedViewModel>();

        await authorService.DeleteAuthorAsync(authorId);

        return View(viewModel);
    }

    public async Task<IActionResult> Publishers(int page = 1, int pageSize = 12)
    {
        var paginatedPublishers = await publisherService.GetPublishersPaginatedAsync(new PaginationParameters
        { PageNumber = page, PageSize = pageSize });

        var viewModel = new PublisherListViewModel
        {
            Items = paginatedPublishers.Items.Select(p => p.Adapt<PublisherSimplifiedViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedPublishers.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedPublishers.TotalCount,
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> PublisherUpsert(int? publisherId = null)
    {
        var viewModel = new PublisherUpsertViewModel();
        await PopulatePublisherDropdownsAsync(viewModel);

        if (!publisherId.HasValue)
        {
            return View(viewModel);
        }

        var author = await publisherService.GetPublisherAsync(publisherId.Value, includeBooks: false);

        viewModel.Id = author.Id;
        viewModel.Name = author.Name;

        viewModel.SelectedBooksIds = author.Books
            .Select(b => b.Id)
            .ToList();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PublisherUpsert(PublisherUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulatePublisherDropdownsAsync(viewModel);
            return View(viewModel);
        }

        var publisherDto = new PublisherUpsertDTO()
        {
            Name = viewModel.Name,
            BookIds = viewModel.SelectedBooksIds,
        };

        try
        {
            if (!viewModel.Id.HasValue)
            {
                await publisherService.CreatePublisherAsync(publisherDto);
                TempData["SuccessMessage"] = "Publisher created successfully!";
            }
            else
            {
                await publisherService.UpdatePublisherAsync(viewModel.Id.Value, publisherDto);
                TempData["SuccessMessage"] = "Publisher updated successfully!";
            }

            return RedirectToAction(nameof(Publishers));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving publisher: {ex.Message}");
            await PopulatePublisherDropdownsAsync(viewModel);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> PublisherDelete(int publisherId)
    {
        var publisher = await publisherService.GetPublisherAsync(publisherId, includeBooks: false);
        var viewModel = publisher.Adapt<PublisherSimplifiedViewModel>();

        await publisherService.DeletePublisherAsync(publisherId);

        return View(viewModel);
    }

    public async Task<IActionResult> Users(int page = 1, int pageSize = 12)
    {
        var paginatedUsers = await userService.GetUsersPaginatedAsync(new PaginationParameters
        { PageNumber = page, PageSize = pageSize });

        var viewModel = new UserListViewModel
        {
            Items = paginatedUsers.Items.Select(u => u.Adapt<UserSimplifiedViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedUsers.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedUsers.TotalCount,
        };

        return View(viewModel);
    }

    public async Task<IActionResult> UserReset(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            TempData["ErrorMessage"] = "User with the specified email was not found.";
            return RedirectToAction(nameof(Users));
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        // TODO: send the callbackUrl to the user's email

        var viewModel = new UserResetViewModel
        {
            Email = user.Email ?? string.Empty,
            ResetToken = encodedToken
        };

        return View(viewModel);
    }

    public async Task<IActionResult> GiftCards(int page = 1, int pageSize = 12)
    {
        var paginatedGiftCards = await giftCardService.GetGiftCardPaginatedAsync(new PaginationParameters
        { PageNumber = page, PageSize = pageSize });

        var viewModel = new GiftCardListViewModel()
        {
            Items = paginatedGiftCards.Items.Select(a => a.Adapt<GiftCardViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedGiftCards.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedGiftCards.TotalCount,
        };

        return View(viewModel);
    }

    public async Task<IActionResult> GiftCard(int giftCardId, int page = 1, int pageSize = 12)
    {
        var giftCard = await giftCardService.GetGiftCardAsync(giftCardId);

        var viewModel = giftCard.Adapt<GiftCardViewModel>();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GiftCardUpsert(int? giftCardId = null)
    {
        var viewModel = new GiftCardUpsertViewModel();

        if (!giftCardId.HasValue)
        {
            return View(viewModel);
        }

        var giftCard = await giftCardService.GetGiftCardAsync(giftCardId.Value);
        viewModel.Id = giftCard.Id;
        viewModel.Title = giftCard.Title;
        viewModel.ValidFrom = giftCard.ValidFrom;
        viewModel.ValidTo = giftCard.ValidTo;
        viewModel.Price = giftCard.Price;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GiftCardUpsert(GiftCardUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (viewModel.ValidFrom >= viewModel.ValidTo)
        {
            ModelState.AddModelError(nameof(viewModel.ValidTo),
                "Valid To must be after Valid From");
            return View(viewModel);
        }

        var giftCardDto = new BusinessLayer.DTOs.GiftCard.GiftCardUpsertDTO
        {
            Title = viewModel.Title,
            ValidFrom = viewModel.ValidFrom,
            ValidTo = viewModel.ValidTo,
            Price = viewModel.Price
        };

        try
        {
            if (!viewModel.Id.HasValue)
            {
                await giftCardService.CreateGiftCardAsync(giftCardDto);
                TempData["SuccessMessage"] = "Gift card created successfully!";
            }
            else
            {
                await giftCardService.UpdateGiftCardAsync(viewModel.Id.Value, giftCardDto);
                TempData["SuccessMessage"] = "Gift card updated successfully!";
            }

            return RedirectToAction(nameof(GiftCards));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving gift card: {ex.Message}");
            return View(viewModel);
        }
    }

    [HttpGet]
    public IActionResult GiftCodesUpsert(int giftCardId)
    {
        var viewModel = new GiftCodesUpsertViewModel
        {
            GiftCardId = giftCardId,
            Count = 0,
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GiftCodesUpsert(GiftCodesUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await giftCardService.AddGiftCodesToGiftCard(viewModel.GiftCardId, viewModel.Count);
            TempData["SuccessMessage"] = "Publisher created successfully!";

            return RedirectToAction(nameof(GiftCard), new { giftCardId = viewModel.GiftCardId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving gift card: {ex.Message}");
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Orders(string email, int page = 1, int pageSize = 12)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var paginatedOrders = await orderService.GetOrdersPaginatedAsync(new BookFilterDTO
        { PageNumber = page, PageSize = pageSize }, userId: user.UserId);

        var giftCards = await giftCardService.GetGiftCardsAsync(paginatedOrders.Items
            .Where(o => o.GiftCode != null)
            .Select(o => o.GiftCode!.GiftCardId)
            .Distinct().ToArray(), includeCodes: false);

        var viewModel = new OrderListItemViewModel
        {
            UserEmail = user.Email,
            Items = paginatedOrders.Items.Select(b => b.Adapt<OrderViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedOrders.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedOrders.TotalCount,
        };

        viewModel.Items.ForEach(o =>
        {
            var originalOrder = paginatedOrders.Items.FirstOrDefault(po => po.Id == o.Id);
            var giftCard = originalOrder?.GiftCode != null
                ? giftCards.FirstOrDefault(gc => gc.Id == originalOrder.GiftCode!.GiftCardId)
                : null;

            if (giftCard == null)
            {
                return;
            }

            o.GiftCard = giftCard.Adapt<OrderGiftCardViewModel>();
            o.GiftCard.GiftCode = originalOrder!.GiftCode.Adapt<GiftCodeViewModel>();
        });

        return View(viewModel);
    }

    public async Task<IActionResult> Order(int orderId, string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var order = await orderService.GetOrderAsync(orderId);


        var viewModel = order.Adapt<OrderViewModel>();

        if (order.GiftCode == null)
        {
            return View(viewModel);
        }

        var giftCard = await giftCardService.GetGiftCardAsync(order.GiftCode.GiftCardId);

        viewModel.GiftCard = giftCard.Adapt<OrderGiftCardViewModel>();
        viewModel.GiftCard.GiftCode = order.GiftCode.Adapt<GiftCodeViewModel>();

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> OrderUpsert(string email, int? orderId = null)
    {
        var viewModel = new OrderUpsertViewModel
        {
            Id = orderId,
            UserEmail = email,
        };
        await PopulateOrderDropdownsAsync(viewModel);

        if (!orderId.HasValue)
        {
            return View(viewModel);
        }

        var order = await orderService.GetOrderAsync(orderId.Value);

        viewModel.SelectedBookIds = order.OrderItems.Where(oi => oi.Book != null).Select(oi => oi.Book!.Id).ToList();
        viewModel.GiftCode = order.GiftCode?.Code;
        viewModel.OrderStatus = order.OrderStatus;

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OrderUpsert(OrderUpsertViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateOrderDropdownsAsync(viewModel);
            return View(viewModel);
        }

        var user = await userManager.FindByEmailAsync(viewModel.UserEmail);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        OrderDTO? order = null;

        if (viewModel.Id.HasValue)
        {
            order = await orderService.GetOrderAsync(viewModel.Id.Value);
        }

        if (viewModel.GiftCode != null && order is { GiftCode: null } or null)
        {
            try
            {
                await giftCardService.ValidateGiftCodeAsync(viewModel.GiftCode);
                TempData["SuccessMessage"] = "Gift code applied!";
            }
            catch (StatusCodeException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(viewModel);
            }
        }

        var orderDto = new OrderUpsertDTO()
        {
            UserId = user.UserId,
            BookIds = viewModel.SelectedBookIds,
            OrderStatus = viewModel.OrderStatus,
            GiftCode = viewModel.GiftCode,
        };

        try
        {
            if (!viewModel.Id.HasValue)
            {
                await orderService.CreateOrderAsync(orderDto);
                TempData["SuccessMessage"] = "Order created successfully!";
            }
            else
            {
                await orderService.UpdateOrderAsync(viewModel.Id.Value, orderDto);
                TempData["SuccessMessage"] = "Order updated successfully!";
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error saving order: {ex.Message}");
            await PopulateOrderDropdownsAsync(viewModel);
            return View(viewModel);
        }

        if (viewModel.GiftCode == null || order is not ({ GiftCode: null } or null))
        {
            return RedirectToAction(nameof(Orders), new { email = viewModel.UserEmail });
        }

        try
        {
            await orderService.ApplyGiftCodeToOrderAsync(user.UserId, viewModel.GiftCode);
            TempData["SuccessMessage"] = "Gift code applied!";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction(nameof(Orders), new { email = viewModel.UserEmail });
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

    private async Task PopulateAuthorDropdownsAsync(AuthorUpsertViewModel viewModel)
    {
        var books = await bookService.FilterBooksAsync(new BookFilterDTO());

        viewModel.Books = books
            .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title })
            .OrderBy(a => a.Text)
            .ToList();
    }

    private async Task PopulatePublisherDropdownsAsync(PublisherUpsertViewModel viewModel)
    {
        var books = await bookService.FilterBooksAsync(new BookFilterDTO());

        viewModel.Books = books
            .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title })
            .OrderBy(a => a.Text)
            .ToList();
    }

    private async Task PopulateOrderDropdownsAsync(OrderUpsertViewModel viewModel)
    {
        var books = await bookService.FilterBooksAsync(new BookFilterDTO());

        viewModel.Books = books
            .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Title })
            .OrderBy(a => a.Text)
            .ToList();
    }
}