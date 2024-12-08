using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class LoginDTO
{
    [Required (ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{6,32}$", 
        ErrorMessage = "The password must be between 6 and 32 characters long, include at least 1 digit and 1 latin letter")]
    public string Password { get; set; }
}
