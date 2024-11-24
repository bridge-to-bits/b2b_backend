using Users.Core.DTOs;
using Users.Core.Models;

namespace Users.Core.Mapping;
public static class DtoToDomainMapper
{
    public static User ToUser(this RegistrationDTO registrationDTO)
    {
        return new User
        {
            Email = registrationDTO.Email,
            FirstName = registrationDTO.FirstName,
            LastName = registrationDTO.LastName,
            City = registrationDTO.City,
            Avatar = registrationDTO.Avatar,
            ProfileBackground = registrationDTO.ProfileBackground,
            AboutMe = registrationDTO.AboutMe,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            UserType = registrationDTO.Type,
        };
    }
    public static Social ToSocial(this AddSocialDTO addSocialDTO)
    {
        return new Social
        {
            Name = addSocialDTO.Name,
            Link = addSocialDTO.Link
        };
    }

    public static Genre ToGenre(this AddGenreDTO addGenreDTO)
    {
        return new Genre
        {
            Name = addGenreDTO.Name,
        };
    }
}
