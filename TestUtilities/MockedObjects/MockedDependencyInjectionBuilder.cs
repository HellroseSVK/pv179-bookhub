using BusinessLayer.Mapper;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Services;
using DataAccessLayer;
using BusinessLayer.Services.Interfaces;

namespace TestUtilities.MockedObjects
{
    public class MockedDependencyInjectionBuilder
    {
        private IServiceCollection _serviceCollection = new ServiceCollection();

        public MockedDependencyInjectionBuilder AddMockedDBContext()
        {
            var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
            var context = MockedDbContext.CreateFromOptions(options);

            _serviceCollection = _serviceCollection.AddScoped<CustomDBContext>(_ => context);

            return this;
        }

        public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
            where T : class
        {
            _serviceCollection = _serviceCollection.AddScoped<T>(_ => objectToRegister);

            return this;
        }

        public MockedDependencyInjectionBuilder AddServices()
        {
            _serviceCollection.RegisterMapsterConfiguration();

            _serviceCollection = _serviceCollection
                .AddScoped<IAuthorService, AuthorService>()
                .AddScoped<IBookService, BookService>()
                .AddScoped<IUserService, UserService>();

            return this;
        }

        public ServiceProvider Create()
        {
            return _serviceCollection.BuildServiceProvider();
        }
    }
}

