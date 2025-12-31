using System.Diagnostics;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.GiftCard;
using MVC.Models.Order;

namespace MVC.Controllers;

public class OrdersController(
    IOrderService orderService,
    IGiftCardService giftCardService,
    UserManager<LocalIdentityUser> userManager) : Controller
{
    public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
    {
        var user = await userManager.GetUserAsync(User);

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

    public async Task<IActionResult> Order(int orderId)
    {
        var user = await userManager.GetUserAsync(User);

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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}