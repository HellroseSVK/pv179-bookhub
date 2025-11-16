using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Bogus;

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
            var bookTitleTemplates = new[]
            {
                "The {adjective} {noun}",
                "{adjective} {noun}",
                "The {noun} of {place}",
                "{person}'s {noun}",
                "The {adjective} {person}",
                "{noun} in {place}",
                "Beyond the {noun}",
                "Tales of {adjective} {noun}",
                "The Last {noun}",
                "Secrets of the {adjective} {noun}"
            };

            var adjectives = new[] { "Silent", "Hidden", "Lost", "Forgotten", "Ancient", "Mysterious", "Eternal", "Sacred", "Dark", "Golden", "Silver", "Crimson", "Emerald", "Shadowed", "Bright" };
            var nouns = new[] { "Journey", "Kingdom", "Dream", "Secret", "Memory", "Heart", "Soul", "Mind", "Story", "Legend", "Myth", "Tale", "Adventure", "Quest", "Destiny" };
            var places = new[] { "London", "Paris", "Rome", "Venice", "Prague", "Istanbul", "Cairo", "Tokyo", "Vienna", "Barcelona", "Dublin", "Edinburgh", "Amsterdam", "Copenhagen" };
            var people = new[] { "Alexander", "Victoria", "Napoleon", "Elizabeth", "Catherine", "Marcus", "Helena", "Adrienne", "Sebastian", "Isabella", "Theodore", "Evangeline" };

            var faker = new Faker<Book>()
                .UseSeed(98765)
                .RuleFor(b => b.Id, f => f.IndexFaker + 1)
                .RuleFor(b => b.Title, f => GenerateBookTitle(f, bookTitleTemplates, adjectives, nouns, places, people))
                .RuleFor(b => b.Description, f => GenerateBookDescription(f))
                .RuleFor(b => b.Price, f => Math.Round(f.Random.Decimal(8.99m, 29.99m), 2))
                .RuleFor(b => b.PublisherId, f => f.Random.Int(1, 10));

            return faker.Generate(25);
        }

        private static string GenerateBookTitle(Faker faker, string[] templates, string[] adjectives, string[] nouns, string[] places, string[] people)
        {
            var template = faker.PickRandom(templates);
            return template
                .Replace("{adjective}", faker.PickRandom(adjectives))
                .Replace("{noun}", faker.PickRandom(nouns))
                .Replace("{place}", faker.PickRandom(places))
                .Replace("{person}", faker.PickRandom(people));
        }

        private static string GenerateBookDescription(Faker faker)
        {
            var openings = new[]
            {
                "A captivating tale of",
                "An epic journey through",
                "A haunting story about",
                "A thrilling adventure featuring",
                "A beautiful narrative exploring",
                "A gripping saga of",
                "An unforgettable story following",
                "A masterful exploration of",
                "A compelling tale that",
                "A breathtaking story of"
            };

            var themes = new[]
            {
                "love and betrayal",
                "courage and sacrifice",
                "mystery and intrigue",
                "friendship and loyalty",
                "power and corruption",
                "hope and redemption",
                "loss and discovery",
                "adventure and danger",
                "family and tradition",
                "dreams and reality"
            };

            var settings = new[]
            {
                "set in a world of magic and wonder",
                "unfolding in the shadows of ancient cities",
                "taking place across vast kingdoms",
                "set against the backdrop of war",
                "in a realm where nothing is as it seems",
                "spanning generations of conflict",
                "in a time of great change",
                "where legends come to life",
                "in a place forgotten by time",
                "across mysterious landscapes"
            };

            var opening = faker.PickRandom(openings);
            var theme = faker.PickRandom(themes);
            var setting = faker.PickRandom(settings);

            return $"{opening} {theme}, {setting}. {faker.Lorem.Sentence(faker.Random.Int(8, 15))}";
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
            var faker = new Faker<Review>()
                .UseSeed(12345)
                .RuleFor(r => r.Id, f => f.IndexFaker + 1)
                .RuleFor(r => r.Rating, f => f.Random.WeightedRandom(
                    new[] { 1, 2, 3, 4, 5 },
                    new[] { 0.1f, 0.1f, 0.2f, 0.3f, 0.3f }
                ))
                .RuleFor(r => r.Body, (f, r) => GenerateReviewBody(f, r.Rating))
                .RuleFor(r => r.BookId, f => f.Random.Int(1, 10))
                .RuleFor(r => r.UserId, f => f.Random.Int(1, 8));

            return faker.Generate(20);
        }

        private static string GenerateReviewBody(Faker faker, int rating)
        {
            var positiveAdjectives = new[] { "amazing", "fantastic", "brilliant", "wonderful", "excellent", "outstanding", "captivating", "engaging", "masterful", "beautiful" };
            var negativeAdjectives = new[] { "disappointing", "boring", "confusing", "poorly written", "predictable", "slow", "uninteresting", "weak", "frustrating", "tedious" };
            var neutralAdjectives = new[] { "decent", "okay", "average", "readable", "solid", "fine", "acceptable", "reasonable", "adequate", "satisfactory" };

            var positiveComments = new[]
            {
                "I couldn't put this book down!",
                "The characters felt real and well-developed.",
                "The plot was engaging from start to finish.",
                "Beautiful writing style that flows perfectly.",
                "This book exceeded all my expectations.",
                "The emotional depth really resonated with me.",
                "A truly memorable reading experience."
            };

            var negativeComments = new[]
            {
                "I struggled to get through this one.",
                "The characters felt one-dimensional.",
                "The plot was too predictable for my taste.",
                "I lost interest halfway through.",
                "The pacing was really off.",
                "Too many clichés and not enough originality.",
                "The ending was disappointing."
            };

            var neutralComments = new[]
            {
                "A solid story, though not groundbreaking.",
                "Some good moments but overall just okay.",
                "It was fine, but didn't leave a lasting impression.",
                "Decent read for passing time.",
                "Had its moments but also some slow parts.",
                "Worth reading but not a favorite.",
                "Good enough to finish but wouldn't recommend highly."
            };

            return rating switch
            {
                5 => $"{faker.PickRandom(positiveComments)} {faker.PickRandom(positiveAdjectives).ToUpper()} book that I would definitely recommend.",
                4 => $"Really {faker.PickRandom(positiveAdjectives)} book. {faker.PickRandom(positiveComments)} Just a few minor issues kept it from being perfect.",
                3 => $"{faker.PickRandom(neutralComments)} {faker.PickRandom(neutralAdjectives).ToUpper()} overall.",
                2 => $"Unfortunately {faker.PickRandom(negativeAdjectives)}. {faker.PickRandom(negativeComments)} Had some potential but didn't deliver.",
                1 => $"Very {faker.PickRandom(negativeAdjectives)} read. {faker.PickRandom(negativeComments)} Would not recommend.",
                _ => faker.Lorem.Sentence(faker.Random.Int(5, 15))
            };
        }

        private static List<OrderItem> PrepareOrderItems()
        {
            var faker = new Faker<OrderItem>()
                .UseSeed(54321)
                .RuleFor(o => o.Id, f => f.IndexFaker + 1)
                .RuleFor(o => o.BookId, f => f.Random.WeightedRandom(
                    new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                    new[] { 0.15f, 0.12f, 0.12f, 0.10f, 0.12f, 0.08f, 0.08f, 0.10f, 0.08f, 0.05f }
                ))
                .RuleFor(o => o.UserId, f => f.Random.WeightedRandom(
                    new[] { 1, 2, 3, 4, 5, 6, 7, 8 },
                    new[] { 0.20f, 0.15f, 0.12f, 0.15f, 0.10f, 0.18f, 0.05f, 0.05f }
                ));

            return faker.Generate(30);
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