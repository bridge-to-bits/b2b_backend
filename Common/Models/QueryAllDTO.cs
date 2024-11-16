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

/* 
@ApiPropertyOptional({
description: 'Number of the page',
})
@IsNumberString({}, validationOptionsMsg('Page must be a number'))
@IsOptional()
page?: number;

@ApiPropertyOptional({
description: 'Amount of the elements in the page',
})
@IsNumberString({}, validationOptionsMsg('PageSize must be a number'))
@IsOptional()
pageSize?: number;

@ApiPropertyOptional({
description: 'Symbols that should be in a filter',
})
@IsOptional()
@IsString(validationOptionsMsg('Search must be a string'))
search?: string;

@ApiPropertyOptional({
description: 'Sorting parameter',
})
@IsOptional()
@IsString(validationOptionsMsg('Sort must be a string'))
sort?: string;

@ApiPropertyOptional({
description: 'Sorting order',
enum: ['asc', 'desc'],
})
@IsIn(['asc', 'desc'], validationOptionsMsg('Wrong value for order'))
@IsOptional()
order?: 'asc' | 'desc';
*/
