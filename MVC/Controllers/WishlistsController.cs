using System.Diagnostics;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.WishlistItem;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Wishlists;
using Shared.Exception;

namespace MVC.Controllers;

public class WishlistsController(
    IWishlistItemService wishlistItemService,
    UserManager<LocalIdentityUser> userManager) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var paginatedWishlistItems = await wishlistItemService.GetWishlistItemsPaginatedAsync(new BookFilterDTO
        { PageNumber = page, PageSize = pageSize }, userId: user.UserId);

        var viewModel = new WishListItemListItemViewModel()
        {
            Items = paginatedWishlistItems.Items.Select(b => b.Adapt<WishListItemViewModel>()).ToList(),
            CurrentPage = page,
            TotalPages = paginatedWishlistItems.TotalPages,
            PageSize = pageSize,
            TotalItems = paginatedWishlistItems.TotalCount,
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddToWishlist(int bookId)
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null)
        {
            return Unauthorized();
        }

        var createDTO = new WishlistItemUpsertDTO
        {
            BookId = bookId,
            UserId = identityUser.UserId
        };

        try
        {
            await wishlistItemService.CreateWishlistItemAsync(createDTO);
            TempData["SuccessMessage"] = "Book added to wishlist!";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index", "Books");
    }

    public async Task<IActionResult> Remove(int wishlistId)
    {

        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null)
        {
            return Unauthorized();
        }

        try
        {
            await wishlistItemService.DeleteWishlistItemAsync(wishlistId);
            TempData["SuccessMessage"] = "Book removed from wishlist!";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}