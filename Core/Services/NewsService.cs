using Core.DTOs.News;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Responses.News;

namespace Core.Services;

public class NewsService (INewsRespository newsRespository, IUserService userService) : INewsService
{
    public Task AddArticle(AddArticleDTO addArticleDTO)
    {
        return newsRespository.AddArticle(addArticleDTO.ToArticle());
    }

    public Task AddArticleComment(Guid articleId, AddCommentDto addCommentDto, string userId)
    {
        return newsRespository.AddArticleComment(articleId, addCommentDto.Text, userId);
    }

    public Task AddInterview(AddInterviewDTO addinterviewDTO)
    {
        return newsRespository.AddInterview(addinterviewDTO.ToInterview());
    }

    public Task AddInterviewComment(Guid interviewId, AddCommentDto addCommentDto, string userId)
    {
        return newsRespository.AddInterviewComment(interviewId, addCommentDto.Text, userId);
    }

    public async Task<WeeklyArticlesResponse> GetAllWeeklyArticles()
    {
        var articles = await newsRespository.GetAllWeeklyArticles();
        return articles.ToWeeklyArticlesResponse();
    }

    public async Task<WeeklyInterviewsResponse> GetAllWeeklyInterviews()
    {
        var interviews = await newsRespository.GetAllWeeklyInterviews();
        return interviews.ToWeeklyInterviewsResponse();
    }

    public async Task<ArticleResponse?> GetArticle(Guid articleId)
    {
        var article = await newsRespository.GetArticle(articleId);
        return article?.ToArticleResponse();
    }

    public async Task<InterviewResponse?> GetInterview(Guid interviewId)
    {
        var interview = await newsRespository.GetInterview(interviewId);
        var interviewverRating = await userService.GetUserAverageRating(interview.SenderId.ToString());
        var respondentRating = await userService.GetUserAverageRating(interview.RespondentId.ToString());
        return interview?.ToInterviewResponse(interviewverRating, respondentRating);
    }
}
