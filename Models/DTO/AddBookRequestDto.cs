namespace BookManagementApi.Models.DTO
{
    public class AddBookRequestDto
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string AuthorName { get; set; }
    }
}

