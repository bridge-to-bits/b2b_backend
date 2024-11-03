using AutoMapper;
using Tracks.Core.DTOs;
using Tracks.Core.Models;
using Tracks.Core.Responses;

namespace Tracks.Core.Profiles
{
    public class GenreMappingProfile: Profile
    {
        public GenreMappingProfile() 
        {
            CreateMap<AddGenreDTO, Genre>();
            CreateMap<Genre, GenreResponse>();
        }
    }
}
