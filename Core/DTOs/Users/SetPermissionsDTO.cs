using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users;

public class SetPermissionsDTO
{
    [Required]
    public IEnumerable<string> Permissions { get; set; }
}