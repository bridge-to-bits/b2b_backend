using Users.Core.DTOs;
using Users.Core.Models;

namespace Users.Core.Mapping;
public static class DtoToDomainMapper
{
    public static User ToUser(this MainRegistrationDTO registrationDTO)
    {
        return new User
        {
            Email = registrationDTO.Email,
            Username = registrationDTO.Username,
            LastName = registrationDTO.LastName ?? "",
            UserType = registrationDTO.UserType,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
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
