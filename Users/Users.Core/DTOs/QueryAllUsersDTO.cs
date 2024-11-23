using Common.Models;

namespace Users.Core.DTOs;

public class QueryAllUsersDTO : QueryAllDTO
{
    public List<string>? GenreIds { get; set; }
}
