namespace Common.Models;

public class QueryAllDTO
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; } = "asc";
    public string? FilterBy { get; set; }
    public string? FilterValue { get; set; }

    public int Skip => (PageNumber - 1) * PageSize;
}
