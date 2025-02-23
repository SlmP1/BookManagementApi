using System.ComponentModel.DataAnnotations;

namespace BookManagementApi.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}