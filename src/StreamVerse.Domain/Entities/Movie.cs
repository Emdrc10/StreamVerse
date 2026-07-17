using StreamVerse.Domain.Core;

namespace StreamVerse.Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; } 
    public int Year { get; set; }
    public int Duration { get; set; }
    public string Synopsis { get; set; }
    public string Poster { get; set; }
    public string Created { get; set; }
    public string Updated { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
}
