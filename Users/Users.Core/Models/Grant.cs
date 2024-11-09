using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Core.Models;

public class Grant
{
    [Key]
    public Guid Id { get; set; }

    public string Permission {  get; set; }

    public Guid RoleId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}
