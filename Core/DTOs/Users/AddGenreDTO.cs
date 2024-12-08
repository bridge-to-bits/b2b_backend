using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users;

public class AddGenreDTO
{
    [Required]
    public string Name { get; set; }
}
