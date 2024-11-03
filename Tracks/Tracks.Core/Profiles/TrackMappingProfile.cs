using AutoMapper;
using Tracks.Core.DTOs;
using Tracks.Core.Models;
using Tracks.Core.Responses;

namespace Tracks.Core.Profiles
{
    public class TrackMappingProfile : Profile
    {
        public TrackMappingProfile()
        {
            CreateMap<UploadTrackDTO, Track>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => Convert.FromBase64String(src.Content)))
                .ForMember(dest => dest.PerformerId, opt => opt.MapFrom(src => Guid.Parse(src.PerformerId)))
                .ForMember(dest => dest.Genres, opt => opt.Ignore());
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Track, TrackResponse>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => Convert.ToBase64String(src.Content)));
        }
    }
}
