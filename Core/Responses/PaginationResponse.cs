namespace Core.Responses;

public class PaginationResponse
{
    public int TotalRecords;
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int? NextPage { get; set; }
    public int? PrevPage { get; set; }
}
