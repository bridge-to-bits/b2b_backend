namespace Core.DTOs.Users;

public class QueryAllUsersDTO : QueryAllDTO
{
    public string? Search {  get; set; }
    public List<string>? GenreIds { get; set; }
}
