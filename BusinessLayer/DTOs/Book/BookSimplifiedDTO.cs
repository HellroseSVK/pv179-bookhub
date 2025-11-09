namespace BusinessLayer.DTOs.Book
{
    public class BookSimplifiedDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
