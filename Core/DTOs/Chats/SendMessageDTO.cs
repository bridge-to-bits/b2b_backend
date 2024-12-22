namespace Core.DTOs.Chats;

public class SendMessageDTO
{
    public Guid SenderId { get; set; }
    public string Message { get; set; }
}
