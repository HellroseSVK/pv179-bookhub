namespace WebAPI.Models
{
    public class BookModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public int PublisherId { get; set; }
    }
}
