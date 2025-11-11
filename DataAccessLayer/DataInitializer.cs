using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var genres = PrepareGenres();
            var publishers = PreparePublishers();
            var authors = PrepareAuthors();
            var users = PrepareUsers();
            var books = PrepareBooks();
            var wishlistItems = PrepareWishlistItems();
            var orderItems = PrepareOrderItems();
            var reviews = PrepareReviews();
            var bookAuthors = LinkBooksAndAuthors(books, authors);
            var bookGenres = LinkBooksAndGenres(books, genres);

            modelBuilder.Entity<Genre>()
                .HasData(genres);
            modelBuilder.Entity<Publisher>()
                .HasData(publishers);
            modelBuilder.Entity<Author>()
                .HasData(authors);
            modelBuilder.Entity<User>()
                .HasData(users);
            modelBuilder.Entity<Book>()
                .HasData(books);
            modelBuilder.Entity<WishlistItem>()
                .HasData(wishlistItems);
            modelBuilder.Entity<OrderItem>()
                .HasData(orderItems);
            modelBuilder.Entity<Review>()
                .HasData(reviews);
            modelBuilder.Entity<BookAuthor>()
                .HasData(bookAuthors);
            modelBuilder.Entity<BookGenre>()
                .HasData(bookGenres);
        }

        private static List<Author> PrepareAuthors()
        {
            return new List<Author>()
            {
                new Author { Id = 1, Name = "J.K.", Surname = "Rowling" },
                new Author { Id = 2, Name = "George", Surname = "Orwell" },
                new Author { Id = 3, Name = "Harper", Surname = "Lee" },
                new Author { Id = 4, Name = "F. Scott", Surname = "Fitzgerald" },
                new Author { Id = 5, Name = "Jane", Surname = "Austen" },
                new Author { Id = 6, Name = "Mark", Surname = "Twain" },
                new Author { Id = 7, Name = "Ernest", Surname = "Hemingway" },
                new Author { Id = 8, Name = "Agatha", Surname = "Christie" },
                new Author { Id = 9, Name = "Stephen", Surname = "King" },
                new Author { Id = 10, Name = "Toni", Surname = "Morrison" }
            };
        }

        private static List<Book> PrepareBooks()
        {
            return new List<Book>()
            {
                new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", Description = "A young wizard's journey begins at Hogwarts School of Witchcraft and Wizardry.", Price = 19.99m, PublisherId = 1 },
                new Book { Id = 2, Title = "1984", Description = "A dystopian social science fiction novel about totalitarian surveillance.", Price = 15.99m, PublisherId = 2 },
                new Book { Id = 3, Title = "To Kill a Mockingbird", Description = "A novel about racial injustice and childhood in the American South.", Price = 14.99m, PublisherId = 2 },
                new Book { Id = 4, Title = "The Great Gatsby", Description = "A critique of the American Dream set in the Jazz Age.", Price = 12.99m, PublisherId = 2 },
                new Book { Id = 5, Title = "Pride and Prejudice", Description = "A romantic novel about manners, upbringing, morality, and marriage.", Price = 13.99m, PublisherId = 3 },
                new Book { Id = 6, Title = "The Adventures of Tom Sawyer", Description = "A boy's adventures along the Mississippi River.", Price = 11.99m, PublisherId = 2 },
                new Book { Id = 7, Title = "The Old Man and the Sea", Description = "An aging Cuban fisherman's struggle with a giant marlin.", Price = 10.99m, PublisherId = 2 },
                new Book { Id = 8, Title = "Murder on the Orient Express", Description = "A detective story featuring Hercule Poirot on a luxury train.", Price = 16.99m, PublisherId = 3 },
                new Book { Id = 9, Title = "The Shining", Description = "A horror novel about a family isolated in a haunted hotel.", Price = 18.99m, PublisherId = 4 },
                new Book { Id = 10, Title = "Beloved", Description = "A story about the legacy of slavery and trauma.", Price = 17.99m, PublisherId = 5 }
            };
        }

        private static List<Genre> PrepareGenres()
        {
            return new List<Genre>()
            {
                new Genre { Id = 1, Name = "Fantasy" },
                new Genre { Id = 2, Name = "Science Fiction" },
                new Genre { Id = 3, Name = "Mystery" },
                new Genre { Id = 4, Name = "Romance" },
                new Genre { Id = 5, Name = "Thriller" },
                new Genre { Id = 6, Name = "Horror" },
                new Genre { Id = 7, Name = "Historical Fiction" },
                new Genre { Id = 8, Name = "Literary Fiction" },
                new Genre { Id = 9, Name = "Adventure" },
                new Genre { Id = 10, Name = "Classic" }
            };
        }

        private static List<Publisher> PreparePublishers()
        {
            return new List<Publisher>()
            {
                new Publisher { Id = 1, Name = "Bloomsbury Publishing Plc" },
                new Publisher { Id = 2, Name = "Harper & Brothers" },
                new Publisher { Id = 3, Name = "Canongate Books" },
                new Publisher { Id = 4, Name = "Ian Allan Publishing" },
                new Publisher { Id = 5, Name = "Bison Books" },
                new Publisher { Id = 6, Name = "Ladybird Books" },
                new Publisher { Id = 7, Name = "Harlequin Enterprises Ltd" },
                new Publisher { Id = 8, Name = "Medknow Publications" },
                new Publisher { Id = 9, Name = "University of California Press" },
                new Publisher { Id = 10, Name = "Emerald Group Publishing" }
            };
        }

        private static List<Review> PrepareReviews()
        {
            return new List<Review>()
            {
                new Review { Id = 1, Rating = 1, Body = "I kept waiting for the story to pick up, but it never really went anywhere.", BookId = 1, UserId = 1 },
                new Review { Id = 2, Rating = 2, Body = "The premise sounded great, but the execution was flat and predictable.", BookId = 2, UserId = 1 },
                new Review { Id = 3, Rating = 3, Body = "A fast-paced read that kept me hooked, though the ending felt a bit rushed.", BookId = 3, UserId = 2 },
                new Review { Id = 4, Rating = 4, Body = "Beautiful writing, but I struggled with the slow middle chapters.", BookId = 4, UserId = 3 },
                new Review { Id = 5, Rating = 5, Body = "Loved the characters — they felt real and flawed in the best way.", BookId = 4, UserId = 4 },
                new Review { Id = 6, Rating = 1, Body = "The characters felt one-dimensional and I couldn’t connect with any of them.", BookId = 2, UserId = 4 },
                new Review { Id = 7, Rating = 2, Body = "Too many clichés and not enough originality — I lost interest halfway through.", BookId = 7, UserId = 4 },
                new Review { Id = 8, Rating = 3, Body = "A solid story, though I guessed the twist halfway through.", BookId = 8, UserId = 6 },
                new Review { Id = 9, Rating = 4, Body = "Enjoyable overall, but I didn’t connect with the main character.", BookId = 3, UserId = 8 },
                new Review { Id = 10, Rating = 5, Body = "An emotional rollercoaster — I cried more than once.", BookId = 1, UserId = 8 }
            };
        }

        private static List<OrderItem> PrepareOrderItems()
        {
            return new List<OrderItem>()
            {
                new OrderItem { Id = 1, BookId = 1, UserId = 1},
                new OrderItem { Id = 2, BookId = 2, UserId = 1},
                new OrderItem { Id = 3, BookId = 3, UserId = 2},
                new OrderItem { Id = 4, BookId = 4, UserId = 3},
                new OrderItem { Id = 5, BookId = 5, UserId = 4},
                new OrderItem { Id = 6, BookId = 6, UserId = 5},
                new OrderItem { Id = 7, BookId = 3, UserId = 6},
                new OrderItem { Id = 8, BookId = 4, UserId = 6},
                new OrderItem { Id = 9, BookId = 7, UserId = 6},
                new OrderItem { Id = 10, BookId = 8, UserId = 6},
            };
        }

        private static List<User> PrepareUsers()
        {
            return new List<User>()
            {
                new User { Id = 1, Email = "james.miller92@gmail.com", Password = "pbkdf2_sha256$100000$2133ad0a4d31462ef5766ff6b03c7987$f919d3c6f899fe09d8f3158618e63988a1d09daea26e0aeed5219768f1e2d199", IsAdmin = false },
                new User { Id = 2, Email = "sarah.lee@outlook.com", Password = "pbkdf2_sha256$100000$0acfd8d2ff40624052f26f2fb3495254$93857a9a6b61f8f9098e639f645f7143d92006ce925fc50e7af534567cafe07f", IsAdmin = false },
                new User { Id = 3, Email = "marco.rossi89@yahoo.com", Password = "pbkdf2_sha256$100000$6190de7b0dda9c8235e0e55455d43f2b$f33f0dfb4085e26e30dd79b9c9959a0a01a033155b4cfe8b9e8b9baa9670b8c3", IsAdmin = false },
                new User { Id = 4, Email = "emily_watson@icloud.com", Password = "pbkdf2_sha256$100000$84beb8171db67abbd93e38551e232120$d47e1668e7ced2fa8462c530ea3f8081e118c4eeb69ba9f7d640c2f6e860fae7", IsAdmin = false },
                new User { Id = 5, Email = "david.nguyen@protonmail.com", Password = "pbkdf2_sha256$100000$6a9df2a924d667a1bd472317344c8f26$45a2a5bc1f82bb6a1c61c33e434fd1b0c0e66cadb6b1bb4d4bffa89bd2687a3b", IsAdmin = false },
                new User { Id = 6, Email = "lucas-johnson@live.com", Password = "pbkdf2_sha256$100000$f67ad2c96b570f5e8c7def3da9f9cc7b$75b5631e544d43972e646d51ab14479771044310b42d0901cb4e2a067192e4fa", IsAdmin = false },
                new User { Id = 7, Email = "maria.garcia23@bookhub.com", Password = "pbkdf2_sha256$100000$6973359e189c00dff9c42cc516cca724$17ae99a40f51d065b02ad1ded4959166e67499c1c6f1fe7a1dea1036b3530dd6", IsAdmin = true },
                new User { Id = 8, Email = "andrew.smith@bookhub.com", Password = "pbkdf2_sha256$100000$463d7f87b63f4843bd6b899bced0acdd$cb8c5551c30f874fb846de9c7cb56595e12baa83c346ad6950504c500519c002", IsAdmin = true },
            };
        }

        private static List<WishlistItem> PrepareWishlistItems()
        {
            return new List<WishlistItem>()
            {
                new WishlistItem { Id = 1, BookId = 7, UserId = 3},
                new WishlistItem { Id = 2, BookId = 6, UserId = 3},
                new WishlistItem { Id = 3, BookId = 5, UserId = 1},
                new WishlistItem { Id = 4, BookId = 4, UserId = 5},
                new WishlistItem { Id = 5, BookId = 3, UserId = 4},
                new WishlistItem { Id = 6, BookId = 2, UserId = 7},
                new WishlistItem { Id = 7, BookId = 7, UserId = 8},
                new WishlistItem { Id = 8, BookId = 6, UserId = 8},
                new WishlistItem { Id = 9, BookId = 2, UserId = 8},
                new WishlistItem { Id = 10, BookId = 1, UserId = 8},
            };
        }

        private static List<BookAuthor> LinkBooksAndAuthors(List<Book> books, List<Author> authors)
        {
            return new List<BookAuthor>()
            {
                new BookAuthor { Id = 1, BookId = 1, AuthorId = 1 }, // Harry Potter - J.K. Rowling
                new BookAuthor { Id = 2, BookId = 2, AuthorId = 2 }, // 1984 - George Orwell
                new BookAuthor { Id = 3, BookId = 3, AuthorId = 3 }, // To Kill a Mockingbird - Harper Lee
                new BookAuthor { Id = 4, BookId = 4, AuthorId = 4 }, // The Great Gatsby - F. Scott Fitzgerald
                new BookAuthor { Id = 5, BookId = 5, AuthorId = 5 }, // Pride and Prejudice - Jane Austen
                new BookAuthor { Id = 6, BookId = 6, AuthorId = 6 }, // Tom Sawyer - Mark Twain
                new BookAuthor { Id = 7, BookId = 7, AuthorId = 7 }, // The Old Man and the Sea - Ernest Hemingway
                new BookAuthor { Id = 8, BookId = 8, AuthorId = 8 }, // Murder on Orient Express - Agatha Christie
                new BookAuthor { Id = 9, BookId = 9, AuthorId = 9 }, // The Shining - Stephen King
                new BookAuthor { Id = 10, BookId = 10, AuthorId = 10 } // Beloved - Toni Morrison
            };
        }

        private static List<BookGenre> LinkBooksAndGenres(List<Book> books, List<Genre> genres)
        {
            return new List<BookGenre>()
            {
                new BookGenre { Id = 1, BookId = 1, GenreId = 1 }, // Harry Potter - Fantasy
                new BookGenre { Id = 2, BookId = 2, GenreId = 2 }, // 1984 - Science Fiction
                new BookGenre { Id = 3, BookId = 3, GenreId = 8 }, // To Kill a Mockingbird - Literary Fiction
                new BookGenre { Id = 4, BookId = 4, GenreId = 10 }, // The Great Gatsby - Classic
                new BookGenre { Id = 5, BookId = 5, GenreId = 4 }, // Pride and Prejudice - Romance
                new BookGenre { Id = 6, BookId = 6, GenreId = 9 }, // Tom Sawyer - Adventure
                new BookGenre { Id = 7, BookId = 7, GenreId = 8 }, // The Old Man and the Sea - Literary Fiction
                new BookGenre { Id = 8, BookId = 8, GenreId = 3 }, // Murder on Orient Express - Mystery
                new BookGenre { Id = 9, BookId = 9, GenreId = 6 }, // The Shining - Horror
                new BookGenre { Id = 10, BookId = 10, GenreId = 8 } // Beloved - Literary Fiction
            };
        }
    }
}