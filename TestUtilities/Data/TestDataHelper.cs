using DataAccessLayer.Entities;

namespace TestUtilities.Data
{
    public class TestDataHelper
    {
        public static List<Genre> GetGenres()
        {
            return [
                new Genre { Id = 1, Name = "Science Fiction" },
                new Genre { Id = 2, Name = "Fantasy" },
                new Genre { Id = 3, Name = "Mystery" },
                new Genre { Id = 4, Name = "Historical Fiction" },
                new Genre { Id = 5, Name = "Romance" },
                new Genre { Id = 6, Name = "Horror" },
                new Genre { Id = 7, Name = "Adventure" },
            ];
        }

        public static List<Book> GetBooks()
        {
            return [
                new Book
                {
                    Id = 1,
                    Title = "The Fellowship of the Ring",
                    Description = "The first volume in J.R.R. Tolkien's epic adventure, starting the journey to destroy the One Ring.",
                    Price = 12.99M,
                    PublisherId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "1984",
                    Description = "George Orwell’s dystopian novel exploring totalitarianism, surveillance, and the loss of individual freedom.",
                    Price = 10.49M,
                    PublisherId = 2
                },
                new Book
                {
                    Id = 3,
                    Title = "Pride and Prejudice",
                    Description = "Jane Austen’s classic romantic novel about manners, marriage, and misconceptions in Georgian England.",
                    Price = 8.99M,
                    PublisherId = 3
                },
                new Book
                {
                    Id = 4,
                    Title = "Adventures of Huckleberry Finn",
                    Description = "Mark Twain’s tale following Huck and Jim on a journey through the Mississippi River, confronting society and freedom.",
                    Price = 9.49M,
                    PublisherId = 4
                },
                new Book
                {
                    Id = 5,
                    Title = "Murder on the Orient Express",
                    Description = "Agatha Christie’s famed mystery where Hercule Poirot investigates a murder aboard a luxurious train.",
                    Price = 11.99M,
                    PublisherId = 5
                },
                new Book
                {
                    Id = 6,
                    Title = "The Old Man and the Sea",
                    Description = "Ernest Hemingway’s Pulitzer-winning novella about an aging fisherman’s battle with a giant marlin.",
                    Price = 7.99M,
                    PublisherId = 6
                },
                new Book
                {
                    Id = 7,
                    Title = "Animal Farm",
                    Description = "George Orwell’s political allegory depicting a farm revolution corrupted into dictatorship.",
                    Price = 9.29M,
                    PublisherId = 2
                },
                new Book
                {
                    Id = 8,
                    Title = "Emma",
                    Description = "A comedic novel by Jane Austen following a well-meaning but meddling matchmaker.",
                    Price = 8.49M,
                    PublisherId = 3
                },
                new Book
                {
                    Id = 9,
                    Title = "The Mysterious Affair at Styles",
                    Description = "Agatha Christie’s debut novel introducing Hercule Poirot in a classic country-house murder mystery.",
                    Price = 10.99M,
                    PublisherId = 5
                },
                new Book
                {
                    Id = 10,
                    Title = "The Sun Also Rises",
                    Description = "Ernest Hemingway’s novel about the Lost Generation navigating love, identity, and post-war disillusionment.",
                    Price = 9.99M,
                    PublisherId = 6
                }
            ];
        }

        public static List<User> GetUsers()
        {
            return [
                new User
                {
                    Id = 1,
                    Email = "",
                    Password = "",
                    IsAdmin = false
                },
            ];
        }

        public static List<Review> GetReviews()
        {
            return [
                new Review
                {
                    Id = 1,
                    Rating = 5,
                    Body = "",
                    BookId = 1,
                    UserId = 1
                },
            ];
        }

        public static List<OrderItem> GetOrderItems()
        {
            return [
                new OrderItem
                {
                    Id = 1,
                    BookId = 1,
                    UserId = 1,
                },
            ];
        }

        public static List<WishlistItem> GetWishlistItems()
        {
            return [
                new WishlistItem
                {
                    Id = 1,
                    UserId = 1,
                    BookId = 2
                },
            ];
        }

        public static List<Author> GetAuthors()
        {
            return [
                new Author
                {
                    Id = 1,
                    Name = "J.R.R.",
                    Surname = "Tolkien",
                },
                new Author
                {
                    Id = 2,
                    Name = "George",
                    Surname = "Orwell",
                },
                new Author
                {
                    Id = 3,
                    Name = "Jane",
                    Surname = "Austen",
                },
                new Author
                {
                    Id = 4,
                    Name = "Mark",
                    Surname = "Twain",
                },
                new Author
                {
                    Id = 5,
                    Name = "Agatha",
                    Surname = "Christie",
                },
                new Author
                {
                    Id = 6,
                    Name = "Ernest",
                    Surname = "Hemingway",
                },
            ];
        }

        public static List<Publisher> GetPublishers()
        {
            return [
                new Publisher
                {
                    Id = 1,
                    Name = "HarperCollins",
                },
                new Publisher
                {
                    Id = 2,
                    Name = "Penguin Random House",
                },
                new Publisher
                {
                    Id = 3,
                    Name = "Simon & Schuster",
                },
                new Publisher
                {
                    Id = 4,
                    Name = "Hachette Book Group",
                },
                new Publisher
                {
                    Id = 5,
                    Name = "Macmillan Publishers",
                },
                new Publisher
                {
                    Id = 6,
                    Name = "Scholastic",
                },
            ];
        }

        public static List<BookGenre> GetBookGenres()
        {
            return [];
        }

        public static List<BookAuthor> GetBookAuthors()
        {
            return [];
        }
    }

}
