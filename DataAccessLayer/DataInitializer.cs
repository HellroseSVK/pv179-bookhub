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
        }

        private static List<Author> PrepareAuthors()
        {
            return new List<Author>() { };
        }

        private static List<Book> PrepareBooks()
        {
            return new List<Book>() { };
        }

        private static List<Genre> PrepareGenres()
        {
            return new List<Genre>() { };
        }

        private static List<Publisher> PreparePublishers()
        {
            return new List<Publisher>() { };
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
            return new List<BookAuthor>();
        }

        private static List<BookGenre> LinkBooksAndGenres(List<Book> books, List<Genre> genres)
        {
            return new List<BookGenre>();
        }
    }
}