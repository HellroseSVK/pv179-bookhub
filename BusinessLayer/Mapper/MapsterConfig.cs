using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
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
            TypeAdapterConfig<Author, AuthorDTO>
                .NewConfig()
                .Map(dest => dest.Books, src => src.BookAuthors.Select(ba => ba.Book));
            TypeAdapterConfig<AuthorCreateDTO, Author>
                .NewConfig()
                .Map(dest => dest.BookAuthors, src => src.BookIds.Select(id => new BookAuthor { BookId = id }));
        }
    }
}