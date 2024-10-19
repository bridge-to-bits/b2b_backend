using System.ComponentModel.DataAnnotations;

namespace Users.Core.Models
{
    public class SetPermissionsDTO
    {
        [Required]
        public IEnumerable<string> Permissions { get; set; }
    }
}