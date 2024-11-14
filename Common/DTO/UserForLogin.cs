using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class UserForLogin
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
