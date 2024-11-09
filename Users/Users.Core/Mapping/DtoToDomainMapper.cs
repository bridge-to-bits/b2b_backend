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
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }
}
