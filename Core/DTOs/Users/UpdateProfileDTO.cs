﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Users;

public class UpdateProfileDTO
{
    [StringLength(100, ErrorMessage = "Name is too long (max:100)")]
    public string? Username { get; set; }

    [StringLength(100, ErrorMessage = "City name is too long (max:100)")]
    public string? City { get; set; }

    [MaxLength(512, ErrorMessage = "AboutMe length must be less than 512")]
    public string? AboutMe { get; set; }

    public IEnumerable<AddSocialDTO>? Socials { get; set; }

    public IEnumerable<string>? GenreIds { get; set; }

    public IFormFile? AvatarFile { get; set; }
    public IFormFile? ProfileBackgroundFile { get; set; }
}
