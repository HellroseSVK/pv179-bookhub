namespace WebAPI.Models
{
    public class BookModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<int> AuthorIds { get; set; }

        public List<int> GenreIds { get; set; }

        public int PublisherId { get; set; }
    }
}
