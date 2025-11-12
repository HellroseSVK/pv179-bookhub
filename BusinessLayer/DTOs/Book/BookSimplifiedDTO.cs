namespace BusinessLayer.DTOs.Book
{
    public class BookSimplifiedDTO : BaseDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
