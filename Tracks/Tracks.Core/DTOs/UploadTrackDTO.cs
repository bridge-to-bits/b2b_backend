namespace Tracks.Core.DTOs
{
    public class UploadTrackDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string PerformerId { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Content { get; set; }
    }
}
