using System.ComponentModel.DataAnnotations;

namespace Users.Core.DTOs;

public class AddGenreDTO
{
    [Required]
    public string Name { get; set; }
}
