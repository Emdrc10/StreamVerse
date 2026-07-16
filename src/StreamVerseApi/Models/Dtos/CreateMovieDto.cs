namespace StreamVerseApi.DTOs.Movies
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public string Synopsis { get; set; }
        public string Poster { get; set; }
        public int GenreId { get; set; }
    }
}
