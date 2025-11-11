using BusinessLayer.DTOs.Genre;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Genre;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController(IGenreService genreService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<GenreResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await genreService.GetGenreAsync(includeBookGenres: true);

        return Ok(genres.Adapt<List<GenreResponse>>());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<GenreResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await genreService.GetGenreAsync(id);

        return Ok(genre.Adapt<GenreResponse>());
    }

    [HttpPost]
    [ProducesResponseType<GenreResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateGenre([FromBody] GenreModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var genre = await genreService.CreateGenreAsync(model.Adapt<GenreCreateDTO>());

        return Created($"/Genre/{genre.Id}", genre.Adapt<GenreResponse>());
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PatchGenre(int id, [FromBody] GenrePartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await genreService.PatchGenreAsync(id, model.Adapt<GenrePartialDTO>());

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await genreService.UpdateGenreAsync(id, model.Adapt<GenreUpdateDTO>());

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await genreService.DeleteGenreAsync(id);

        return NoContent();
    }
}