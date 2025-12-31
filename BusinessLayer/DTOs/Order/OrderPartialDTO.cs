using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Order
{
    public class OrderPartialDTO
    {
        public List<int>? BookIds { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public string? GiftCode { get; set; }
    }
}
