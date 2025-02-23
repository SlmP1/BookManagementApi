using System.ComponentModel.DataAnnotations;


namespace BookManagementApi.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}