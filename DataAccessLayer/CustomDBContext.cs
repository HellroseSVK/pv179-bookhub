using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CustomDBContext : DbContext
    {
        // public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var dbPath = Path.Join(Environment.GetFolderPath(folder), "BookHub.db");

            optionsBuilder
                .UseSqlite($"Data Source={dbPath}")
                .LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup the delete method for all of the entities using reflexion
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }

            /* one-to-many relationship */

            /* many-to-many relationship */

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

    }
}
