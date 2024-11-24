using System.ComponentModel.DataAnnotations;
using Users.Core.Models;

namespace Users.Core.DTOs;

public class AdditionalRegistrationDTO
{
    [StringLength(100, ErrorMessage = "City name is too long (max:100)")]
    public string City { get; set; }

    [Required(ErrorMessage = "Avatar cannot be empty")]
    public string Avatar { get; set; }

    [MaxLength(512, ErrorMessage = "AboutMe length must be less than 512")]
    public string? AboutMe { get; set; }

    [Required(ErrorMessage = "Role cannot be empty")]
    public UserType Type { get; set; }

    [Required(ErrorMessage = "IsAdmin cannot be empty")]
    public bool IsAdmin { get; set; }

    public IEnumerable<AddSocialDTO>? Socials { get; set; }

    public IEnumerable<string> GenreIds { get; set; }
}
