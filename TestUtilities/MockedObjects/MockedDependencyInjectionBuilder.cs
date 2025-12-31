using BusinessLayer.Mapper;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Services;
using DataAccessLayer;
using BusinessLayer.Services.Interfaces;
using Mapster;
using MapsterMapper;
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
            { "Jwt:Key", "very_secret_token" },
            { "Jwt:Issuer", "bookhub" },
            { "Jwt:Audience", "bookhub" },
        };
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        _serviceCollection = _serviceCollection.AddSingleton<IConfiguration>(config);

        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        mapsterConfig.Apply(new BLMappings());

        _serviceCollection.AddSingleton(mapsterConfig);
        _serviceCollection.AddScoped<IMapper, Mapper>();

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