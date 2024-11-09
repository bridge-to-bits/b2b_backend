using System.ComponentModel.DataAnnotations;
using Users.Core.Models;

namespace Users.Core.DTOs;

public class RegistrationDTO
{
    [Required (ErrorMessage = "Username cannot be empty")]
    [RegularExpression(@"^[a-zA-Z0-9_]{4,40}$",
        ErrorMessage = "Username is not correct (a-zA-Z0-9_), or too short (min: 4), or too long (max: 40)")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{6,32}$", 
        ErrorMessage = "The password must be between 6 and 32 characters long, include at least 1 digit and 1 latin letter")]
    public string Password { get; set; }

    [Required (ErrorMessage = "Age cannot be empty")]
    [Range(16, 200, ErrorMessage = "Age must be between 16 and 200")]
    public int Age { get; set; }

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

    [Required(ErrorMessage = "Role cannot be empty")]
    public RoleType Role { get; set; }

    [Required(ErrorMessage = "IsAdmin cannot be empty")]
    public bool IsAdmin { get; set; }
}
