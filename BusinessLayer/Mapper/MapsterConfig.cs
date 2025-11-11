using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.OrderItem;
using BusinessLayer.DTOs.Review;
using BusinessLayer.DTOs.User;
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

            // Author mappings
            TypeAdapterConfig<Author, AuthorDTO>
                .NewConfig()
                .Map(dest => dest.Books, src => src.BookAuthors.Select(ba => ba.Book));
            TypeAdapterConfig<AuthorCreateDTO, Author>
                .NewConfig()
                .Map(dest => dest.BookAuthors, src => src.BookIds.Select(id => new BookAuthor { BookId = id }));

            // Genre mappings
            TypeAdapterConfig<Genre, GenreDTO>
                .NewConfig()
                .Map(dest => dest.Books, src => src.BookGenres.Select(bg => bg.Book));
            TypeAdapterConfig<GenreCreateDTO, Genre>
                .NewConfig()
                .Map(dest => dest.BookGenres, src => src.BookIds.Select(id => new BookGenre { BookId = id }));

            // Book mappings
            TypeAdapterConfig<Book, BookDTO>
                .NewConfig()
                .Map(dest => dest.Authors, src => src.BookAuthors.Select(ba => ba.Author))
                .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => bg.Genre));
        }
    }
}