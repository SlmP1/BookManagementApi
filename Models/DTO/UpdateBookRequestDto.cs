namespace BookManagementApi.Models.DTO
{
    public class UpdateBookRequestDto
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string AuthorName { get; set; }
    }
}
