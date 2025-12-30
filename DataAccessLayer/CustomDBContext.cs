using System.Security.Claims;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace DataAccessLayer;

public class CustomDBContext(DbContextOptions options, IHttpContextAccessor accessor)
    : IdentityDbContext<LocalIdentityUser>(options)
{
    public new DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<GiftCard> GiftCards { get; set; }
    public DbSet<GiftCode> GiftCodes { get; set; }

    public DbSet<HttpLog> HttpLogs { get; set; }

    public DbSet<LocalIdentityUser> LocalIdentityUsers { get; set; }

    public override int SaveChanges()
    {
        ApplyAuditInfo();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditInfo()
    {
        string? userId = null;
        var httpContext = accessor?.HttpContext;

        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var user = httpContext.User;
            userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                     ?? user.FindFirst(JwtRegisteredClaimNames.Email)?.Value
                     ?? user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value
                     ?? user.FindFirst(ClaimTypes.Role)?.Value;
        }

        userId ??= "local-user";

        Console.WriteLine("Audit Info Applied: UserId = " + userId);

        var modifiedEntries = ChangeTracker.Entries<Book>()
            .Where(e => e.State is EntityState.Modified or EntityState.Added)
            .Select(entry =>
            {
                entry.Entity.LastModifiedByUserIds = entry.Entity.LastModifiedByUserIds == null
                    ? [userId!]
                    : entry.Entity.LastModifiedByUserIds.Concat([userId!]).ToArray();
                entry.Entity.EditCount += 1;
                return entry;
            });

        Console.WriteLine("Modified Entries Processed: " + modifiedEntries.Count());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Setup the delete method for all of the entities using reflexion
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.SetNull;
        }

        // Setup DB creation of timestamps
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            var prop = clrType.GetProperty(nameof(BaseEntity.CreatedAt));

            if (prop != null) // only configure if the property exists
            {
                modelBuilder.Entity(clrType)
                    .Property(nameof(BaseEntity.CreatedAt))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }

        modelBuilder.Entity<LocalIdentityUser>()
            .HasOne(u => u.User)
            .WithOne()
            .HasForeignKey<LocalIdentityUser>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // one-to-many relationships

        modelBuilder.Entity<WishlistItem>()
            .HasOne<User>(item => item.User)
            .WithMany(user => user.WishlistItems)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne<User>(order => order.User)
            .WithMany(user => user.Orders)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne<Order>(item => item.Order)
            .WithMany(order => order.OrderItems)
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

        modelBuilder.Entity<BookGenre>()
            .HasIndex(bg => new { bg.BookId, bg.IsPrimary })
            .HasFilter("IsPrimary = 1")
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasOne<Publisher>(book => book.Publisher)
            .WithMany(publisher => publisher.Books)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GiftCode>()
            .HasOne<GiftCard>(gc => gc.GiftCard)
            .WithMany(giftCard => giftCard.GiftCodes)
            .OnDelete(DeleteBehavior.Cascade);

        // one-to-one relationships
        modelBuilder.Entity<Order>()
            .HasOne(o => o.GiftCode)
            .WithOne(gc => gc.Order)
            .HasForeignKey<GiftCode>(gc => gc.OrderId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Seed();
    }
}