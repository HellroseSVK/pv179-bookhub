namespace DataAccessLayer.Entities
{
    public class Genre : BaseEntity
    {
        public required string Name { get; set; }

        public List<BookGenre> BookGenres { get; set; } = [];
    }
}
