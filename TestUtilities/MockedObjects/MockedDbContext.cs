using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects
{
    public class MockedDbContext
    {
        public static string RandomDbName => Guid.NewGuid().ToString();

        public static DbContextOptions<CustomDBContext> GenerateNewInMemoryDbContextOptions()
        {
            var dbContextOptions = new DbContextOptionsBuilder<CustomDBContext>()
                .UseInMemoryDatabase(RandomDbName)
                .Options;

            return dbContextOptions;
        }

        public static CustomDBContext CreateFromOptions(DbContextOptions<CustomDBContext> options)
        {
            var dbContextToMock = new CustomDBContext(options);
            PrepareData(dbContextToMock);

            return dbContextToMock;
        }
        private static void PrepareData(CustomDBContext dbContext)
        {
            dbContext.Authors.AddRange(TestDataHelper.GetAuthors());
            dbContext.Publishers.AddRange(TestDataHelper.GetPublishers());
            dbContext.Genres.AddRange(TestDataHelper.GetGenres());
            dbContext.Books.AddRange(TestDataHelper.GetBooks());
            dbContext.Users.AddRange(TestDataHelper.GetUsers());
            dbContext.Reviews.AddRange(TestDataHelper.GetReviews());
            dbContext.WishlistItems.AddRange(TestDataHelper.GetWishlistItems());
            dbContext.OrderItems.AddRange(TestDataHelper.GetOrderItems());

            dbContext.SaveChanges();
        }
    }
}
