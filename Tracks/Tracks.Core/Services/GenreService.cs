using Tracks.Core.DTOs;
using Tracks.Core.Interfaces;
using Tracks.Core.Mapping;
using Tracks.Core.Models;
using Tracks.Core.Responses;

namespace Tracks.Core.Services;

public class GenreService(IGenreRepository genreRepository) : IGenreService
{
    public Task<Genre> AddGenre(AddGenreDTO addGenreDTO)
    {
        var genre = addGenreDTO.ToGenre();
        return genreRepository.AddGenre(genre);
    }

    public async Task RemoveGenre(string genreId)
    {
        var genre = await genreRepository.GetGenre(genre => genre.Id == Guid.Parse(genreId));
        if (genre == null) return;

        await genreRepository.RemoveGenre(genre);
    }

    public async Task<IEnumerable<GenreResponse>> GetAllGenres()
    {
        var genres = await genreRepository.GetAllGenres();
        return genres.Select(DomainToDtoMapper.ToGenreResponse);
    }
}
