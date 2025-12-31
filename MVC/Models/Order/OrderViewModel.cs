using DataAccessLayer.Enums;
using MVC.Models.Book;
using MVC.Models.GiftCard;

namespace MVC.Models.Order;

public class OrderViewModel
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; }

    public decimal TotalPrice => Math.Max(0, Books.Sum(b => b.Price) - (GiftCard?.Price ?? 0));

    public List<BookSimplifiedViewModel> Books { get; set; } = [];
    public OrderGiftCardViewModel? GiftCard { get; set; }
}