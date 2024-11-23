namespace Users.Core.Responses;

public class UsersResponse: PaginationResponse
{
    public IEnumerable<UserInfoResponse> Data { get; set; }
}
