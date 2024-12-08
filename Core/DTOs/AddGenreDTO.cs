using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class AddGenreDTO
{
    [Required]
    public string Name { get; set; }
}
