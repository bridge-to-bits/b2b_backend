using Core.DTOs.Users;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Models;
using Core.Responses;

namespace Core.Services;

public class GenresService(IGenreRepository genreRepository) : IGenreService
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
        return genres.Select(DomainToResponseMapper.ToGenreResponse);
    }
}
