using BusinessLayer.DTOs.Simplified;

namespace BusinessLayer.DTOs;

public class OrderItemDTO
{
    public BookSimplifiedDTO Book { get; set; }
    public UserSimplifiedDTO User { get; set; }
}
