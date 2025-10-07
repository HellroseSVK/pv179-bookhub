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

            // TODO:
            /* one-to-many relationship */

            // TODO:
            /* many-to-many relationship */

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

    }
}
