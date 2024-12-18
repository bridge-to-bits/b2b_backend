using Core.DTOs.News;
using Core.Filters;
using Core.Interfaces.Services;
using Core.Models;
using Core.Responses.News;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController (INewsService newsService) : ControllerBase
    {
        [ProducesResponseType(typeof(WeeklyArticlesResponse), StatusCodes.Status200OK)]
        [HttpGet("/articles")]
        public async Task<IActionResult> GetArticles()
        {
            return Ok(await newsService.GetAllWeeklyArticles());
        }

        [ProducesResponseType(typeof(ArticleResponse), StatusCodes.Status200OK)]
        [HttpGet("/articles/{articleId}")]
        public async Task<IActionResult> GetArticle(Guid articleId)
        {
            return Ok(await newsService.GetArticle(articleId));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("/articles")]
        public async Task<IActionResult> AddArticle([FromBody] AddArticleDTO addArticleDTO)
        {
            await newsService.AddArticle(addArticleDTO);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [TokenAuthorize]
        [HttpPost("/articles/{articleId}/comments")]
        public async Task<IActionResult> AddArticleComment(Guid articleId, [FromBody] AddCommentDto addCommentDto)
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId == null)
                return Unauthorized(new { message = "userId not found in token" });

            await newsService.AddArticleComment(articleId, addCommentDto, userId);
            return Ok();
        }

        [ProducesResponseType(typeof(WeeklyInterviewsResponse), StatusCodes.Status200OK)]
        [HttpGet("/interviews")]
        public async Task<IActionResult> GetInterviews()
        {
            return Ok(await newsService.GetAllWeeklyInterviews());
        }

        [ProducesResponseType(typeof(InterviewResponse),StatusCodes.Status200OK)]
        [HttpGet("/interviews/{interviewId}")]
        public async Task<IActionResult> GetInterview(Guid interviewId)
        {
            return Ok(await newsService.GetInterview(interviewId));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("/interviews")]
        public async Task<IActionResult> AddInterview([FromBody] AddInterviewDTO addInterviewDTO)
        {
            await newsService.AddInterview(addInterviewDTO);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("/interviews/{interviewId}/comments")]
        public async Task<IActionResult> AddInterviewComments(Guid interviewId, [FromBody] AddCommentDto addCommentDto)
        {
            var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId == null)
                return Unauthorized(new { message = "userId not found in token" });

            await newsService.AddInterviewComment(interviewId, addCommentDto, userId);
            return Ok(interviewId);
        }
    }
}
