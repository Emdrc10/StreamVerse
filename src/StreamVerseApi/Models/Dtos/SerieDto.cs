namespace StreamVerseApi.Models.Dtos
{
    public class SerieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public string Synopsis { get; set; }
        public string GenreName { get; set; }
    }
}
