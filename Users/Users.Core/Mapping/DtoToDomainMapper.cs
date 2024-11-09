using Users.Core.DTOs;
using Users.Core.Models;

namespace Users.Core.Mapping;
public static class DtoToDomainMapper
{
    public static User ToUser(this RegistrationDTO registrationDTO)
    {
        return new User
        {
            Username = registrationDTO.Username,
            Email = registrationDTO.Email,
            FirstName = registrationDTO.FirstName,
            LastName = registrationDTO.LastName,
            City = registrationDTO.City,
            Avatar = registrationDTO.Avatar,
            AboutMe = registrationDTO.AboutMe,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }
    public static Social ToSocial(this AddSocialDTO addSocialDTO)
    {
        return new Social
        {
            Name = addSocialDTO.Name,
            Link = addSocialDTO.Link,
        };
    }
}
