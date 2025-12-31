using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models.Order;

public class OrderUpsertViewModel
{
    public int? Id { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "User Email")]
    public string UserEmail { get; set; } = string.Empty;

    [Display(Name = "Books")]
    [Required(ErrorMessage = "Select at least one book")]
    public List<int> SelectedBookIds { get; set; } = [];

    [Display(Name = "Gift Code")]
    public string? GiftCode { get; set; }

    [Display(Name = "Order Status")]
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Draft;

    public List<SelectListItem> Books { get; set; } = [];
}