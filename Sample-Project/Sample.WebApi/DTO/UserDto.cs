using System.ComponentModel.DataAnnotations;

namespace Sample.WebApi.DTO
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
