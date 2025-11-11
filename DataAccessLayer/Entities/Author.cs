namespace DataAccessLayer.Entities
{
    public class Author : BaseEntity
    {
        public required string Name { get; set; }

        public required string Surname { get; set; }

        public List<BookAuthor> BookAuthors { get; set; } = [];
    }
}