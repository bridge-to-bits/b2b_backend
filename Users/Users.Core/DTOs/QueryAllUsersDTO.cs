using Common.Models;
using System.ComponentModel.DataAnnotations;
using Users.Core.Models;

namespace Users.Core.DTOs;

public class QueryAllUsersDTO : QueryAllDTO
{
    [Required(ErrorMessage = "UserType cannot be empty")]
    public UserType UserType { get; set; }
    public List<string>? GenreIds { get; set; }
}
