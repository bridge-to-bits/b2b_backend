using System.ComponentModel.DataAnnotations;

namespace Users.Core.DTOs;

public class SetPermissionsDTO
{
    [Required]
    public IEnumerable<string> Permissions { get; set; }
}