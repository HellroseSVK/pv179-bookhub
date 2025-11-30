using BusinessLayer.DTOs.Book;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Search;

namespace MVC.Controllers;

public class SearchController(
    IBookFacade bookFacade,
    IGenreService genreService,
    IPublisherService publisherService,
    IAuthorService authorService) : Controller
{
    public async Task<IActionResult> Index(SearchRequestModel model)
    {
        var viewModel = new SearchViewModel
        {
            SearchTerm = model.SearchTerm,
            Page = model.Page,
            PageSize = model.PageSize
        };

        if (!string.IsNullOrEmpty(model.SearchTerm))
        {
            var bookFilter = new BookFilterDTO
            {
                SearchTerm = model.SearchTerm,
                PageNumber = model.Page,
                PageSize = model.PageSize
            };

            viewModel.Books = await bookFacade.FilterBooksPaginatedAsync(bookFilter);
            viewModel.Genres = await genreService.SearchGenresAsync(model.SearchTerm);
            viewModel.Publishers = await publisherService.SearchPublishersAsync(model.SearchTerm);
            viewModel.Authors = await authorService.SearchAuthorsAsync(model.SearchTerm);
        }

        return View(viewModel);
    }
}