using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users;

public class AddSocialDTO
{
    [Required(ErrorMessage = "Name cannot be empty")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Link cannot be empty")]
    public string Link { get; set; }
}
