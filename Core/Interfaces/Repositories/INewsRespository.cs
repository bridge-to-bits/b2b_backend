using Core.DTOs.News;
using Core.Models;
namespace Core.Interfaces.Repositories;

public interface INewsRespository
{
    public Task AddArticle(Article article);
    Task AddArticleComment(Guid articleId, string text, string userId);
    public Task<List<Article>> GetAllWeeklyArticles();
    public Task<Article?> GetArticle(Guid articleId);

    public Task AddInterview(Interview interview);
    Task AddInterviewComment(Guid interviewId, string text, string userId);
    public Task<List<Interview>> GetAllWeeklyInterviews();
    public Task<Interview?> GetInterview(Guid interviewId);
}
