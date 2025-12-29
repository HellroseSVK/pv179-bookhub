using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Mapper;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Mapster configuration
builder.Services.RegisterMapsterConfiguration();

var envName = builder.Environment.EnvironmentName;

Console.WriteLine($"Env Name: {envName}");

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
    .Build();

var sqliteConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<CustomDBContext>(options =>
{
    options
        .UseSqlite(sqliteConnectionString)
        .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
        .EnableSensitiveDataLogging();
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookFacade, BookFacade>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IWishlistItemService, WishlistItemService>();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 10 * 1024 * 1024; // 10MB
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10MB
});

builder.Services.AddIdentity<LocalIdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CustomDBContext>()
    .AddDefaultTokenProviders();

// Additional configurations (password policies, cookie settings)
// Configure Identity options for password policy
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 0;
});

// Configure the application cookie settings.
builder.Services.ConfigureApplicationCookie(options =>
{
    // Sets the path for the login page.
    // When a user attempts to access a resource that requires authentication and they are not authenticated,
    // they will be redirected to this path.
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<CustomDBContext>();
await db.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var wwwRootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");

if (!Directory.Exists(wwwRootPath))
{
    Directory.CreateDirectory(wwwRootPath);
    Console.WriteLine($"Created missing static files folder at: {wwwRootPath}");
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwRootPath),
    RequestPath = ""
});

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();