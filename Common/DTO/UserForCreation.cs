using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class UserForCreation
    {
        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Password { get; set; }
        [Required]
        [MaxLength(320)]
        public required string Email { get; set; }
    }
}
