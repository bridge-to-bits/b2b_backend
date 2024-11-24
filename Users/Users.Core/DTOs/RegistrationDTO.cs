using System.ComponentModel.DataAnnotations;
using Users.Core.Models;

namespace Users.Core.DTOs;

public class RegistrationDTO
{
    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{6,32}$", 
        ErrorMessage = "The password must be between 6 and 32 characters long, include at least 1 digit and 1 latin letter")]
    public string Password { get; set; }

    [Required(ErrorMessage = "LastName cannot be empty")]
    [StringLength(100, ErrorMessage = "LastName is too long (max:100)")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "FirstName cannot be empty")]
    [StringLength(100, ErrorMessage = "FirstName is too long (max:100)")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "City cannot be empty")]
    [StringLength(100, ErrorMessage = "City name is too long (max:100)")]
    public string City { get; set; }

    [Required (ErrorMessage = "Avatar cannot be empty")]
    public string Avatar { get; set; }
    public string ProfileBackground { get; set; }

    [MaxLength(512, ErrorMessage = "AboutMe length must be less than 512")]
    public string? AboutMe { get; set; }

    [Required(ErrorMessage = "Role cannot be empty")]
    public UserType Type { get; set; }

    [Required(ErrorMessage = "IsAdmin cannot be empty")]
    public bool IsAdmin { get; set; }

    public IEnumerable<AddSocialDTO>? Socials { get; set; }

    public IEnumerable<string> GenreIds { get; set; }
}
