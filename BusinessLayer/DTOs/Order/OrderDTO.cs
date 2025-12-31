using BusinessLayer.DTOs.GiftCard;
using BusinessLayer.DTOs.OrderItem;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Order
{
    public class OrderDTO : OrderSimplifiedDTO
    {
        public UserSimplifiedNoPasswordDTO? User { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = [];

        public GiftCodeDTO? GiftCode { get; set; }
    }
}
