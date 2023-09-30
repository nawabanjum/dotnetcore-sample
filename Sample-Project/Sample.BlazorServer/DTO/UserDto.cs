using System.ComponentModel.DataAnnotations;

namespace Sample.BlazorServer.DTO
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
