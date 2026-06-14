namespace StreamVerseApi.Models.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public string Synopsis { get; set; }
        public string GenreName { get; set; }
    }
}
