namespace Core.Responses.Users;

public class UsersResponse : PaginationResponse
{
    public IEnumerable<UserInfoResponse> Data { get; set; }
}
