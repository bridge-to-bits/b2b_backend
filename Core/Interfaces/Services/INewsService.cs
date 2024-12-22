using Core.DTOs.News;
using Core.Responses.News;

namespace Core.Interfaces.Services;

public interface INewsService
{
    public Task AddArticle(AddArticleDTO addArticleDTO);
    Task AddArticleComment(Guid articleId, AddCommentDto addCommentDto, string userId);
    public Task<WeeklyArticlesResponse> GetAllWeeklyArticles();
    public Task<ArticleResponse?> GetArticle(Guid articleId);

    public Task AddInterview(AddInterviewDTO addArticleDTO);
    Task AddInterviewComment(Guid interviewId, AddCommentDto addCommentDto, string userId);
    public Task<WeeklyInterviewsResponse> GetAllWeeklyInterviews();
    public Task<InterviewResponse?> GetInterview(Guid interviewId);
}
