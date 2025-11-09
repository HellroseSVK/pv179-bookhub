namespace DataAccessLayer.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
    }
}