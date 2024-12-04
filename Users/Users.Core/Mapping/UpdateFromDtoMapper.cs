using Users.Core.DTOs;
using Users.Core.Models;

namespace Users.Core.Mapping;
public static class UpdateFromDtoMapper
{
    public static void UpdateUser(this User user, UpdateProfileDTO updateProfileDTO)
    {
        if (!string.IsNullOrWhiteSpace(updateProfileDTO.Username))
            user.Username = updateProfileDTO.Username;

        if (!string.IsNullOrWhiteSpace(updateProfileDTO.City))
            user.City = updateProfileDTO.City;

        if (!string.IsNullOrWhiteSpace(updateProfileDTO.Avatar))
            user.Avatar = updateProfileDTO.Avatar;

        if (!string.IsNullOrWhiteSpace(updateProfileDTO.ProfileBackground))
            user.ProfileBackground = updateProfileDTO.ProfileBackground;

        if (!string.IsNullOrWhiteSpace(updateProfileDTO.AboutMe))
            user.AboutMe = updateProfileDTO.AboutMe;

        user.UpdatedAt = DateTime.Now;
    }

    public static void UpdateSocial(this Social social, AddSocialDTO addSocialDTO)
    {
        if (!string.IsNullOrWhiteSpace(addSocialDTO.Name))
            social.Name = addSocialDTO.Name;

        if (!string.IsNullOrWhiteSpace(addSocialDTO.Link))
            social.Link = addSocialDTO.Link;
    }

    public static void UpdateGenre(this Genre genre, AddGenreDTO addGenreDTO)
    {
        if (!string.IsNullOrWhiteSpace(addGenreDTO.Name))
            genre.Name = addGenreDTO.Name;
    }
}