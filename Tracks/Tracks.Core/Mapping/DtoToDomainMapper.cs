using Tracks.Core.DTOs;
using Tracks.Core.Models;

namespace Tracks.Core.Mapping;

public static class DtoToDomainMapper
{
    public static Genre ToGenre(this AddGenreDTO dto)
    {
        return new Genre
        {
            Name = dto.Name,
        };
    }

    public static Track ToTrack(this UploadTracksDTO dto)
    {
        return new Track
        {
            Name = dto.Name,
            Description = dto.Description,
            PerformerId = Guid.Parse(dto.PerformerId)
        };
    }
}
