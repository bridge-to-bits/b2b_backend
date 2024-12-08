using Common.Models;

namespace Core.DTOs.Users;

public class QueryAllUsersDTO : QueryAllDTO
{
    public List<string>? GenreIds { get; set; }
}
