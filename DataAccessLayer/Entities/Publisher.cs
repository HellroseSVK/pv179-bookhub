namespace DataAccessLayer.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
