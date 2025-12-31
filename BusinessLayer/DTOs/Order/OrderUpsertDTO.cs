using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Order
{
    public class OrderUpsertDTO
    {
        public int UserId { get; set; }
        public List<int> BookIds { get; set; } = [];

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Draft;

        public string? GiftCode { get; set; }
    }
}
