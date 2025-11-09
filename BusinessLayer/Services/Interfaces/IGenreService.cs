using BusinessLayer.DTOs.Genre;

namespace BusinessLayer.Services.Interfaces;

public interface IGenreService
{
    public Task<GenreDTO> CreateGenreAsync(GenreCreateDTO genreDto);

    public Task<List<GenreDTO>> GetGenreAsync(int[]? ids = null, bool includeBookGenres = true);

    public Task<GenreDTO> GetGenreAsync(int id, bool includeBookGenres = true);

    public Task PatchGenreAsync(int id, GenrePartialDTO genreDto);
    public Task UpdateGenreAsync(int id, GenreUpdateDTO genreDto);
    public Task DeleteGenreAsync(int id);
}