using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Order
{
    public class OrderSimplifiedDTO : BaseDTO
    {
        public OrderStatus OrderStatus { get; set; }
    }
}
