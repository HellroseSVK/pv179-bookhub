using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace DataAccessLayer
{
    public class CustomDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var dbPath = Path.Join(Environment.GetFolderPath(folder), "BookHub.db");

            optionsBuilder
                .UseSqlite($"Data Source={dbPath}") // Windows path: $USER/AppData/Local/BookHub.db
                .LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup the delete method for all of the entities using reflexion
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }

            // Setup DB creation of timestamps
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseEntity.CreatedAt))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }

            // one-to-many relationships

            modelBuilder.Entity<WishlistItem>()
                .HasOne<User>(item => item.User)
                .WithMany(user => user.WishlistItems)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne<User>(item => item.User)
                .WithMany(user => user.OrderItems)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne<User>(review => review.User)
                .WithMany(user => user.Reviews)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishlistItem>()
                .HasOne<Book>(item => item.Book)
                .WithMany(book => book.WishlistItems)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Book>(item => item.Book)
                .WithMany(book => book.OrderItems)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne<Book>(review => review.Book)
                .WithMany(book => book.Reviews)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
                .HasOne<Author>(bauthor => bauthor.Author)
                .WithMany(author => author.BookAuthors)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
                .HasOne<Book>(bauthor => bauthor.Book)
                .WithMany(book => book.BookAuthors)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookGenre>()
                .HasOne<Genre>(bgenre => bgenre.Genre)
                .WithMany(genre => genre.BookGenres)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookGenre>()
                .HasOne<Book>(bgenre => bgenre.Book)
                .WithMany(book => book.BookGenres)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne<Publisher>(book => book.Publisher)
                .WithMany(publisher => publisher.Books)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

    }
}
