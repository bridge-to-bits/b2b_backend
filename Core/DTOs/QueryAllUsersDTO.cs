using Common.Models;

namespace Core.DTOs;

public class QueryAllUsersDTO : QueryAllDTO
{
    public List<string>? GenreIds { get; set; }
}
