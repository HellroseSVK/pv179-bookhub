using BusinessLayer.Mapper;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Services;
using DataAccessLayer;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TestUtilities.MockedObjects;

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
        var inMemorySettings = new Dictionary<string, string?>
        {
            // add keys your services expect; adjust values as needed for tests
            { "Jwt:Key", "test_jwt_key" },
            { "Jwt:Issuer", "" },
            { "Jwt:Audience", "" },
        };
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        _serviceCollection = _serviceCollection.AddSingleton<IConfiguration>(config);

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