using System.ComponentModel.DataAnnotations;
using Users.Core.Models;

namespace Users.Core.DTOs;

public class UpdateProfileDTO
{
    [StringLength(100, ErrorMessage = "Name is too long (max:100)")]
    public string? Name { get; set; }

    [StringLength(100, ErrorMessage = "City name is too long (max:100)")]
    public string? City { get; set; }

    public string? Avatar { get; set; }

    public string? ProfileBackground { get; set; }

    [MaxLength(512, ErrorMessage = "AboutMe length must be less than 512")]
    public string? AboutMe { get; set; }

    [Required(ErrorMessage = "Role cannot be empty")]
    public UserType Type { get; set; }

    public IEnumerable<AddSocialDTO>? Socials { get; set; }

    public IEnumerable<string> GenreIds { get; set; }
}
