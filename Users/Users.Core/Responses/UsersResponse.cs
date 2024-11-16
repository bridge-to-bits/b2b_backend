namespace Users.Core.Responses;

public class UsersResponse
{
    public int TotalRecords;
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }
    public int? PrevPage { get; set; }
    public IEnumerable<UserInfoResponse> Data { get; set; }
}
