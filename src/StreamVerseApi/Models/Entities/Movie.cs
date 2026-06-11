using System.Runtime.CompilerServices;

namespace StreamVerseApi.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public int Year { get; set; }
        public int Duration { get; set; }
        public string Synopsis { get; set; }
        public string Poster { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
    }
}
