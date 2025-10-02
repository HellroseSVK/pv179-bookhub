using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // TODO: Seed the data in correct order! (foreign keys)

            var genres = PrepareGenres();
            var publishers = PreparePublishers();
            var authors = PrepareAuthors();
            var wishlists = PrepareWishlists();
            var users = PrepareUsers();
            var books = PrepareBooks();
            var reviews = PrepareReviews();
            var bookAuthors = LinkBooksAndAuthors(books, authors);
            var bookGenres = LinkBooksAndGenres(books, genres);

            modelBuilder.Entity<Genre>()
                .HasData(genres);
            modelBuilder.Entity<Publisher>()
                .HasData(publishers);
            modelBuilder.Entity<Author>()
                .HasData(authors);
            modelBuilder.Entity<Wishlist>()
                .HasData(wishlists);
            modelBuilder.Entity<User>()
                .HasData(users);
            modelBuilder.Entity<Book>()
                .HasData(books);
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
            return new List<Genre>() { };
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
            return new List<Review>() { };
        }

        private static List<User> PrepareUsers()
        {
            return new List<User>() { };
        }

        private static List<Wishlist> PrepareWishlists()
        {
            return new List<Wishlist>() { };
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
            return new List<BookGenre>();
        }
    }
}