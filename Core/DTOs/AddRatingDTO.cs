using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class AddRatingDTO
{
    [Required(ErrorMessage = "Rating cannot be empty")]
    [Range(1, 5, ErrorMessage = "Invalid rating value. Rating value should be between 1 and 5")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "InitiatorUserId cannot be empty")]
    public string InitiatorUserId { get; set; }
}
