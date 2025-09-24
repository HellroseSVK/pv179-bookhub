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

            var users = PrepareUsers();
            var wishlists = PrepareWishlists();
            var reviews = PrepareReviews();
            var publishers = PreparePublishers();
            var genres = PrepareGenres();
            var books = PrepareBooks();
            var authors = PrepareAuthors();

            modelBuilder.Entity<User>()
                .HasData(users);
            modelBuilder.Entity<Wishlist>()
                .HasData(wishlists);
            modelBuilder.Entity<Review>()
                .HasData(reviews);
            modelBuilder.Entity<Publisher>()
                .HasData(publishers);
            modelBuilder.Entity<Genre>()
                .HasData(genres);
            modelBuilder.Entity<Book>()
                .HasData(books);
            modelBuilder.Entity<Author>()
                .HasData(authors);
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
    }
}
