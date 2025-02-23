using System.ComponentModel.DataAnnotations;

namespace BookManagementApi.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int PublicationYear { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public int ViewsCount { get; set; } = 0;
        public bool IsDeleted { get; set; }
    }
}
