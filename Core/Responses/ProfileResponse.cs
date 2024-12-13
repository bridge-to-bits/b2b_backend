﻿namespace Core.Responses;

public class ProfileResponse
{
    public string Banner { get; set; }
    public string Avatar { get; set; }
    public string Username { get; set; }
    public double Rating { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
    public IEnumerable<SocialResponse> Socials { get; set; }
}