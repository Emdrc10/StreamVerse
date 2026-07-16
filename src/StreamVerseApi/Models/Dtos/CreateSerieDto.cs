namespace StreamVerseApi.Models.Dtos
{
    public class CreateSerieDto
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public string Synopsis { get; set; }
        public string Poster { get; set; }
        public int GenreId { get; set; }
    }
}
