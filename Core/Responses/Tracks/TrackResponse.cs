﻿using Core.Models;

namespace Core.Responses.Tracks;

public class TrackResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
    public Guid PerformerId { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
}
