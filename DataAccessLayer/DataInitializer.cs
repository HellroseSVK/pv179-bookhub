using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace DataAccessLayer;

public static class DataInitializer
{
    private const int GenreCount = 10;
    private const int PublisherCount = 6;
    private const int AuthorCount = 20;
    private const int UserCount = 8;
    private const int BookCount = 25;
    private const int WishlistItemCount = 20;
    private const int OrderCount = 8;
    private const int OrderItemCount = 30;
    private const int ReviewCount = 20;
    private const int BookAuthorCount = 40;
    private const int BookGenreCount = 40;
    private const int GiftCardCount = 5;
    private const int GiftCodeCount = 500;

    public static void Seed(this ModelBuilder modelBuilder)
    {
        var giftCards = PrepareGiftCards();
        var giftCodes = PrepareGiftCode();
        var genres = PrepareGenres();
        var publishers = PreparePublishers();
        var authors = PrepareAuthors();
        var users = PrepareUsers();
        var books = PrepareBooks();
        var wishlistItems = PrepareWishlistItems();
        var orders = PrepareOrders();
        var orderItems = PrepareOrderItems();
        var reviews = PrepareReviews();
        var bookAuthors = LinkBooksAndAuthors();
        var bookGenres = LinkBooksAndGenres();

        modelBuilder.Entity<GiftCard>()
            .HasData(giftCards);
        modelBuilder.Entity<GiftCode>()
            .HasData(giftCodes);
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
        modelBuilder.Entity<Order>()
            .HasData(orders);
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
        var names = new[]
        {
            "J.K.", "George", "Harper", "F. Scott", "Jane", "Mark", "Ernest", "Agatha", "Stephen", "Toni"
        };

        var surnames = new[]
        {
            "Rowling", "Orwell", "Lee", "Fitzgerald", "Austen", "Twain", "Hemingway", "Christie", "King", "Morrison"
        };

        var faker = new Faker<Author>()
            .UseSeed(213496)
            .RuleFor(b => b.Id, f => f.IndexFaker + 1)
            .RuleFor(b => b.Name, f => f.PickRandom(names))
            .RuleFor(b => b.Surname, f => f.PickRandom(surnames));

        return faker.Generate(AuthorCount);
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

        var adjectives = new[]
        {
            "Silent", "Hidden", "Lost", "Forgotten", "Ancient", "Mysterious", "Eternal", "Sacred", "Dark", "Golden",
            "Silver", "Crimson", "Emerald", "Shadowed", "Bright"
        };
        var nouns = new[]
        {
            "Journey", "Kingdom", "Dream", "Secret", "Memory", "Heart", "Soul", "Mind", "Story", "Legend", "Myth",
            "Tale", "Adventure", "Quest", "Destiny"
        };
        var places = new[]
        {
            "London", "Paris", "Rome", "Venice", "Prague", "Istanbul", "Cairo", "Tokyo", "Vienna", "Barcelona",
            "Dublin", "Edinburgh", "Amsterdam", "Copenhagen"
        };
        var people = new[]
        {
            "Alexander", "Victoria", "Napoleon", "Elizabeth", "Catherine", "Marcus", "Helena", "Adrienne",
            "Sebastian", "Isabella", "Theodore", "Evangeline"
        };

        var faker = new Faker<Book>()
            .UseSeed(98765)
            .RuleFor(b => b.Id, f => f.IndexFaker + 1)
            .RuleFor(b => b.Title, f => GenerateBookTitle(f, bookTitleTemplates, adjectives, nouns, places, people))
            .RuleFor(b => b.Description, GenerateBookDescription)
            .RuleFor(b => b.Price, f => Math.Round(f.Random.Decimal(8.99m, 29.99m), 2))
            .RuleFor(b => b.PublisherId, f => f.Random.Int(1, PublisherCount));

        return faker.Generate(BookCount);
    }

    private static string GenerateBookTitle(Faker faker, string[] templates, string[] adjectives, string[] nouns,
        string[] places, string[] people)
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
        var genreNames = new[]
        {
            "Fantasy", "Science Fiction", "Mystery", "Romance", "Thriller",
            "Horror", "Historical Fiction", "Literary Fiction", "Adventure", "Classic", "Non-Fiction",
            "Biography", "Self-Help", "Young Adult", "Children's", "Dystopian", "Contemporary", "Graphic Novel",
            "Poetry", "Drama"
        };

        var faker = new Faker<Genre>()
            .UseSeed(87416)
            .RuleFor(b => b.Id, f => f.IndexFaker + 1)
            .RuleFor(b => b.Name, f => f.PickRandom(genreNames));

        return faker.Generate(GenreCount);
    }

    private static List<Publisher> PreparePublishers()
    {
        return
        [
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
        ];
    }

    private static List<Review> PrepareReviews()
    {
        var faker = new Faker<Review>()
            .UseSeed(12345)
            .RuleFor(r => r.Id, f => f.IndexFaker + 1)
            .RuleFor(r => r.Rating, f => f.Random.WeightedRandom(
                [1, 2, 3, 4, 5],
                [0.1f, 0.1f, 0.2f, 0.3f, 0.3f]
            ))
            .RuleFor(r => r.Body, (f, r) => GenerateReviewBody(f, r.Rating))
            .RuleFor(r => r.BookId, f => f.Random.Int(1, BookCount))
            .RuleFor(r => r.UserId, f => f.Random.Int(1, UserCount));

        return faker.Generate(ReviewCount);
    }

    private static string GenerateReviewBody(Faker faker, int rating)
    {
        var positiveAdjectives = new[]
        {
            "amazing", "fantastic", "brilliant", "wonderful", "excellent", "outstanding", "captivating", "engaging",
            "masterful", "beautiful"
        };
        var negativeAdjectives = new[]
        {
            "disappointing", "boring", "confusing", "poorly written", "predictable", "slow", "uninteresting",
            "weak", "frustrating", "tedious"
        };
        var neutralAdjectives = new[]
        {
            "decent", "okay", "average", "readable", "solid", "fine", "acceptable", "reasonable", "adequate",
            "satisfactory"
        };

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
            5 =>
                $"{faker.PickRandom(positiveComments)} {faker.PickRandom(positiveAdjectives).ToUpper()} book that I would definitely recommend.",
            4 =>
                $"Really {faker.PickRandom(positiveAdjectives)} book. {faker.PickRandom(positiveComments)} Just a few minor issues kept it from being perfect.",
            3 => $"{faker.PickRandom(neutralComments)} {faker.PickRandom(neutralAdjectives).ToUpper()} overall.",
            2 =>
                $"Unfortunately {faker.PickRandom(negativeAdjectives)}. {faker.PickRandom(negativeComments)} Had some potential but didn't deliver.",
            1 =>
                $"Very {faker.PickRandom(negativeAdjectives)} read. {faker.PickRandom(negativeComments)} Would not recommend.",
            _ => faker.Lorem.Sentence(faker.Random.Int(5, 15))
        };
    }

    private static List<Order> PrepareOrders()
    {
        var faker = new Faker<Order>()
            .UseSeed(111222)
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.UserId, f => f.Random.Int(1, UserCount))
            .RuleFor(o => o.OrderStatus, f => OrderStatus.Completed);

        return faker.Generate(OrderCount);
    }

    private static List<OrderItem> PrepareOrderItems()
    {
        var faker = new Faker<OrderItem>()
            .UseSeed(54321)
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.BookId, f => f.Random.Int(1, BookCount))
            .RuleFor(o => o.OrderId, f => f.Random.Int(1, OrderCount));

        return faker.Generate(OrderItemCount);
    }

    private static List<User> PrepareUsers()
    {
        var faker = new Faker<User>()
            .UseSeed(666)
            .RuleFor(u => u.Id, f => f.IndexFaker + 1)
            .RuleFor(u => u.Email, GenerateEmail)
            .RuleFor(u => u.IsAdmin, f => f.Random.Bool(0.2f)) // 20% admins
            .RuleFor(u => u.Password, GeneratePassword);

        return faker.Generate(UserCount);
    }

    private static string GeneratePassword(Faker faker)
    {
        var letters = faker.Random.String2(20, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
        var digits = faker.Random.String2(5, "0123456789");

        return letters + digits;
    }

    private static readonly string[] FirstNames =
    [
        "john", "jane", "michael", "sarah", "david",
        "emily", "chris", "laura", "robert", "anna"
    ];

    private static readonly string[] LastNames =
    [
        "smith", "johnson", "williams", "brown", "jones",
        "garcia", "miller", "davis", "martinez", "lee"
    ];

    private static string GenerateEmail(Faker faker)
    {
        var first = faker.Random.ArrayElement(FirstNames);
        var last = faker.Random.ArrayElement(LastNames);
        var domain = faker.Random.ArrayElement(["example.com", "test.com", "sample.org"]);

        return $"{first}.{last}@{domain}";
    }

    private static List<WishlistItem> PrepareWishlistItems()
    {
        var faker = new Faker<WishlistItem>()
            .UseSeed(648651)
            .RuleFor(w => w.Id, f => f.IndexFaker + 1)
            .RuleFor(w => w.BookId, f => f.Random.Int(1, BookCount))
            .RuleFor(w => w.UserId, f => f.Random.Int(1, UserCount));

        return faker.Generate(WishlistItemCount);
    }

    private static List<BookAuthor> LinkBooksAndAuthors()
    {
        var faker = new Faker<BookAuthor>()
            .UseSeed(761321)
            .RuleFor(w => w.Id, f => f.IndexFaker + 1)
            .RuleFor(w => w.BookId, f => f.Random.Int(1, BookCount))
            .RuleFor(w => w.AuthorId, f => f.Random.Int(1, AuthorCount));

        return faker.Generate(BookAuthorCount);
    }

    private static List<BookGenre> LinkBooksAndGenres()
    {
        var rand = new Random(761321);
        var pairs = new HashSet<(int BookId, int GenreId)>();
        var primaryPairs = new HashSet<(int BookId, int GenreId)>();

        for (int bookId = 1; bookId <= BookCount; bookId++)
        {
            var genreId = rand.Next(1, GenreCount + 1);
            pairs.Add((bookId, genreId));
            primaryPairs.Add((bookId, genreId));
        }

        while (pairs.Count < BookGenreCount)
        {
            var bookId = rand.Next(1, BookCount + 1);
            var genreId = rand.Next(1, GenreCount + 1);
            pairs.Add((bookId, genreId));
        }

        var result = pairs
            .OrderBy(p => p.BookId)
            .ThenBy(p => p.GenreId)
            .Select((p, i) => new BookGenre
            {
                Id = i + 1,
                BookId = p.BookId,
                GenreId = p.GenreId,
                IsPrimary = primaryPairs.Contains(p)
            })
            .ToList();

        return result;
    }

    private static readonly string[] Titles =
    [
        "Birthday Bliss Gift Card",
        "Holiday Cheer Gift Card",
        "Thank You Treat Gift Card",
        "Just Because Gift Card",
        "Congratulations Gift Card",
        "Thinking of You Gift Card",
        "Anniversary Delight Gift Card",
        "Welcome Gift Card",
        "Get Well Soon Gift Card",
        "New Home Gift Card",
        "Seasonal Surprise Gift Card",
        "Care Package Gift Card"
    ];
    private static List<GiftCard> PrepareGiftCards()
    {
        var faker = new Faker<GiftCard>()
            .UseSeed(1645)
            .RuleFor(card => card.Id, f => f.IndexFaker + 1)
            .RuleFor(card => card.Title, f => f.Random.ArrayElement(Titles))
            .RuleFor(card => card.ValidFrom, f => new DateTime(2025, 1, 1))
            .RuleFor(card => card.ValidTo, f => new DateTime(2026, 1, 1))
            .RuleFor(card => card.Price, f => f.Random.ArrayElement(new[] { 50m, 100m, 150m, 200m, 250m }));

        return faker.Generate(GiftCardCount);
    }

    private static List<GiftCode> PrepareGiftCode()
    {
        var faker = new Faker<GiftCode>()
            .UseSeed(1645)
            .RuleFor(code => code.Id, f => f.IndexFaker + 1)
            .RuleFor(code => code.Code, f => f.Random.AlphaNumeric(12).ToUpper())
            .RuleFor(code => code.GiftCardId, f => f.Random.Int(1, GiftCardCount));

        return faker.Generate(GiftCodeCount);
    }
}