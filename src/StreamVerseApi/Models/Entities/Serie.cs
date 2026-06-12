namespace StreamVerseApi.Models.Entities
{
    public class Serie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public string Synopsis { get; set; }
        public string Poster { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }
}
