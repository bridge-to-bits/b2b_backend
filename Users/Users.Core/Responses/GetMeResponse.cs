using System.ComponentModel.DataAnnotations;

namespace Users.Core.Responses;

public class GetMeResponse
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }
    public string Avatar { get; set; }
}
