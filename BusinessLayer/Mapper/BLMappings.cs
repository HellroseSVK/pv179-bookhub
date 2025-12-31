using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using DataAccessLayer.Entities;
using Mapster;

namespace BusinessLayer.Mapper;

public class BLMappings : IRegister
{
    // Custom mapping belongs here
    public void Register(TypeAdapterConfig config)
    {
        // Author mappings
        config.NewConfig<Author, AuthorDTO>()
            .Map(dest => dest.Books, src => src.BookAuthors.Select(ba => ba.Book));

        // Genre mappings
        config.NewConfig<Genre, GenreDTO>()
            .Map(dest => dest.Books, src => src.BookGenres.Select(bg => bg.Book));

        // Book mappings
        config.NewConfig<Book, BookDTO>()
            .Map(dest => dest.Authors, src => src.BookAuthors.Select(ba => ba.Author))
            .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => new GenreSimplifiedDTO
            {
                Id = bg.Genre!.Id,
                Name = bg.Genre.Name,
                IsPrimary = bg.IsPrimary,
                CreatedAt = bg.Genre.CreatedAt
            }))
            .Map(dest => dest.ImageUrl, src => !string.IsNullOrEmpty(src.ImagePath) ? $"/images/books/{src.ImagePath}" : null);

        config.NewConfig<Book, BookSimplifiedDTO>()
            .Map(dest => dest.ImageUrl, src => !string.IsNullOrEmpty(src.ImagePath) ? $"/images/books/{src.ImagePath}" : null);
    }
}