using System.ComponentModel.DataAnnotations;

namespace Users.Core.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(40)]
        public string Username { get; set; }

        public string Email { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        public int Age { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        public string Avatar { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
