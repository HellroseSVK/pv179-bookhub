using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Mapper
{
    public static class MapsterConfig
    {
        // Custom mapping belongs here
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            // Author mappings
            TypeAdapterConfig<Author, AuthorDTO>
                .NewConfig()
                .Map(dest => dest.Books, src => src.BookAuthors.Select(ba => ba.Book));

            // Genre mappings
            TypeAdapterConfig<Genre, GenreDTO>
                .NewConfig()
                .Map(dest => dest.Books, src => src.BookGenres.Select(bg => bg.Book));

            // Book mappings
            TypeAdapterConfig<Book, BookDTO>
                .NewConfig()
                .Map(dest => dest.Authors, src => src.BookAuthors.Select(ba => ba.Author))
                .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => bg.Genre))
                .Map(dest => dest.ImageUrl, src => !string.IsNullOrEmpty(src.ImagePath) ? $"/images/books/{src.ImagePath}" : null);

            TypeAdapterConfig<Book, BookSimplifiedDTO>
                .NewConfig()
                .Map(dest => dest.ImageUrl, src => !string.IsNullOrEmpty(src.ImagePath) ? $"/images/books/{src.ImagePath}" : null);
        }
    }
}