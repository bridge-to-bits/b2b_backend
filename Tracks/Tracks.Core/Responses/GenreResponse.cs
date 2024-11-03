using System.ComponentModel.DataAnnotations;

namespace Tracks.Core.Responses
{
    public class GenreResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
