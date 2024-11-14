using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class UserForCreation
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email { get; set; }
    }
}
