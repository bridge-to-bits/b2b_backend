using System.ComponentModel.DataAnnotations;
using Users.Core.Models;

namespace Users.Core.DTOs;

public class MainRegistrationDTO
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
}
