using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tracks.Core.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<Track> Tracks { get; set; }
    }
}
