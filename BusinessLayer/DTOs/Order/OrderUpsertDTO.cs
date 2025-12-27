namespace BusinessLayer.DTOs.Order
{
    public class OrderUpsertDTO
    {
        public int UserId { get; set; }
        public List<int> BookIds { get; set; } = [];
    }
}
