using System.ComponentModel.DataAnnotations;

namespace Users.Core.DTOs;

public class AddSocialDTO
{
    [Required(ErrorMessage = "Name cannot be empty")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Link cannot be empty")]
    public string Link { get; set; }

    [Required(ErrorMessage = "UserId cannot be empty")]
    public string UserId { get; set; }
}
