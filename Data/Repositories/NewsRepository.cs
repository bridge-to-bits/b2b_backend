using Core.Interfaces.Repositories;
using Core.Models;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class NewsRepository(B2BDbContext context) : INewsRespository
{
    public Task AddArticle(Article article)
    {
        context.Article.Add(article);
        return context.SaveChangesAsync();
    }

    public Task AddArticleComment(Guid articleId, string text, string userId)
    {
        context.ArticleComment.Add(new ArticleComment
        {
            ArticleId = articleId,
            CreatedAt = DateTime.Now,
            UserId = Guid.Parse(userId),
            Text = text,
        });
        return context.SaveChangesAsync();
    }

    public Task AddInterview(Interview interview)
    {
        context.Interviews.Add(interview);
        return context.SaveChangesAsync();
    }

    public Task AddInterviewComment(Guid interviewId, string text, string userId)
    {
        context.InterviewComments.Add(new InterviewComment
        {
            InterviewId = interviewId,
            CreatedAt = DateTime.Now,
            UserId = Guid.Parse(userId),
            Text = text,
        });
        return context.SaveChangesAsync();
    }

    public Task<List<Article>> GetAllWeeklyArticles()
    {
        var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

        return context.Article
            .AsNoTracking()
            .Where(a => a.CreatedAt >= oneWeekAgo)
            .ToListAsync();
    }

    public Task<List<Interview>> GetAllWeeklyInterviews()
    {
        var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

        return context.Interviews
            .AsNoTracking()
            .Where(a => a.CreatedAt >= oneWeekAgo)
            .ToListAsync();
    }

    public Task<Article?> GetArticle(Guid articleId)
    {
        return context.Article
            .AsNoTracking()
            .Include(a => a.Author)
            .Include(a => a.Comments)
            .ThenInclude(c => c.User)
            .Include(a => a.Ratings)
            .FirstOrDefaultAsync(a => a.Id == articleId);
    }

    public Task<Interview?> GetInterview(Guid interviewId)
    {
        return context.Interviews
            .AsNoTracking()
            .Include(a => a.Respondent)
            .Include(a => a.Author)
            .Include(a => a.Comments)
                .ThenInclude(c => c.User)
            .Include(a => a.Ratings)
            .FirstOrDefaultAsync(a => a.Id == interviewId);
    }
}
