namespace Core.Responses;

public class SendInvitationPerformersResponse
{
    public IEnumerable<SendInvitationPerformerResponse> Data { get; set; }
}

public class SendInvitationPerformerResponse
{
    public string Avatar { get; set; }
    public string Username { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
    public double Rating { get; set; }
    public string UserId { get; set; }
}
