using BusinessLayer.DTOs.User;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.Publisher;
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
            TypeAdapterConfig<UserSimplifiedPartialDTO, UserSimplifiedDTO>
                .NewConfig()
                .IgnoreNullValues(true);

            // Book to BookDTO mapping with many-to-many relationships
            TypeAdapterConfig<Book, BookDTO>
                .NewConfig()
                .Map(dest => dest.Authors, src => src.BookAuthors.Select(ba => ba.Author))
                .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => bg.Genre));

            // BookCreateDTO to Book mapping with many-to-many relationships
            TypeAdapterConfig<BookCreateDTO, Book>
                .NewConfig()
                .Map(dest => dest.BookAuthors, src => src.AuthorIds.Select(id => new BookAuthor { AuthorId = id }))
                .Map(dest => dest.BookGenres, src => src.GenreIds.Select(id => new BookGenre { GenreId = id }));
        }
    }
}
