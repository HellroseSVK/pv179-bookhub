using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer
{
    public static class DataInitializer
    {
        private const int GenreCount = 10;
        private const int PublisherCount = 6;
        private const int AuthorCount = 20;
        private const int UserCount = 8;
        private const int BookCount = 25;
        private const int WishlistItemCount = 20;
        private const int OrderItemCount = 30;
        private const int ReviewCount = 20;

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
            var bookAuthors = LinkBooksAndAuthors();
            var bookGenres = LinkBooksAndGenres();

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

            return faker.Generate(BookCount);
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

            return faker.Generate(ReviewCount);
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

            return faker.Generate(OrderItemCount);
        }

        private static List<User> PrepareUsers()
        {
            var passwordHasher = new PasswordHasher<User>();

            var faker = new Faker<User>()
                .UseSeed(666)
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.IsAdmin, f => f.Random.Bool(0.2f)) // 20% admins
                .FinishWith((f, u) =>
                 {
                     u.Password = passwordHasher.HashPassword(u, f.Internet.Password());
                 });

            return faker.Generate(UserCount);
        }

        private static List<WishlistItem> PrepareWishlistItems()
        {
            var faker = new Faker<WishlistItem>()
                .UseSeed(648651)
                .RuleFor(w => w.Id, f => f.IndexFaker + 1)
                .RuleFor(w => w.BookId, f => f.Random.Int(1, 25))
                .RuleFor(w => w.UserId, f => f.Random.Int(1, 8));

            return faker.Generate(WishlistItemCount);
        }

        private static List<BookAuthor> LinkBooksAndAuthors()
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

        private static List<BookGenre> LinkBooksAndGenres()
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