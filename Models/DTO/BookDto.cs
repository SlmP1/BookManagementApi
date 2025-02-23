namespace BookManagementApi.Models.DTO
{
    public class BookDto
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string AuthorName { get; set; }
        public int ViewsCount { get; set; }

        public int PopularityScore { get; set; }


    }
}
