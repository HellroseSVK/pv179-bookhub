namespace MVC.Models.Order;

public class OrderListItemViewModel : PagedViewModel<OrderViewModel>
{
    public string? UserEmail { get; set; } = string.Empty;
}