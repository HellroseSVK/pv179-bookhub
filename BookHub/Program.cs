using DataAccessLayer;
using Microsoft.OpenApi.Models;
using WebAPI.Middlewares;
using BusinessLayer.Services;
using BusinessLayer.Facades;
using BusinessLayer.Mapper;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Facades.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterMapsterConfiguration();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });

    // Configure Swagger to accept a static token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter the API key as follows: Bearer your_token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<CustomDBContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookFacade, BookFacade>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 10 * 1024 * 1024; // 10MB
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10MB
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = ""
});

app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseAuthorization();

app.UseMiddleware<JsonToXmlMiddleware>();

app.MapControllers();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<StatusCodeExceptionMiddleware>();

app.Run();
