using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class SetPermissionsDTO
{
    [Required]
    public IEnumerable<string> Permissions { get; set; }
}