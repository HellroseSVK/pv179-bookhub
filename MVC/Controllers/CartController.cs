using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Cart;
using Shared.Exception;

namespace MVC.Controllers;

public class CartController(
    IOrderService orderService,
    UserManager<LocalIdentityUser> userManager) : Controller
{
    [Authorize]
    public async Task<IActionResult> Index()
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null) return Unauthorized();

        var cartDto = await orderService.GetUserCartAsync(identityUser.UserId);
        var cartSummary = await orderService.CalculateCartTotalsAsync(identityUser.UserId);

        var viewModel = new CartViewModel
        {
            CartId = cartDto.Id,
            Items = cartDto.OrderItems.Select(oi => new CartItemViewModel
            {
                OrderItemId = oi.Id,
                BookId = oi.Book!.Id,
                Title = oi.Book.Title,
                Author = null, // BookSimplifiedDTO doesn't include author info
                CoverImageUrl = oi.Book.ImageUrl,
                Price = oi.Book.Price
            }).ToList(),
            Subtotal = cartSummary.Subtotal,
            Discount = cartSummary.Discount,
            Total = cartSummary.Total,
            AppliedGiftCode = cartSummary.AppliedGiftCode
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddToCart(int bookId)
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null) return Unauthorized();

        try
        {
            await orderService.AddBookToCartAsync(identityUser.UserId, bookId);
            TempData["SuccessMessage"] = "Book added to cart!";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index", "Books");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> RemoveItem(int orderItemId)
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null) return Unauthorized();

        try
        {
            await orderService.RemoveItemFromCartAsync(identityUser.UserId, orderItemId);
            TempData["SuccessMessage"] = "Item removed from cart.";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ApplyGiftCode(string code)
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null) return Unauthorized();

        try
        {
            await orderService.ApplyGiftCodeToCartAsync(identityUser.UserId, code);
            TempData["SuccessMessage"] = "Gift code applied!";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> RemoveGiftCode()
    {
        var identityUser = await userManager.GetUserAsync(User);
        if (identityUser == null) return Unauthorized();

        try
        {
            await orderService.RemoveGiftCodeFromCartAsync(identityUser.UserId);
            TempData["SuccessMessage"] = "Gift code removed.";
        }
        catch (StatusCodeException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }

        return RedirectToAction("Index");
    }
}
