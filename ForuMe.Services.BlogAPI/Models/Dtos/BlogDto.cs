namespace ForuMe.Services.BlogAPI.Models.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
    }
}
    