using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Order;

public class OrderItemModel
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public int UserId { get; set; }
}