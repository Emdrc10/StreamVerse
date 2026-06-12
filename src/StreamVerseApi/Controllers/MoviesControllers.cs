using Microsoft.AspNetCore.Mvc;
using StreamVerseApi.Models.Entities;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController : ControllerBase
    {
        private static readonly List<Movie> _movies = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Avengers: Endgame",
                Year = 2019,
                Duration = 181,
                Synopsis = "The Avengers assemble once more to reverse Thanos's actions.",
                Poster = "avengers-endgame.jpg",
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            },
            new Movie
            {
                Id = 2,
                Title = "Interstellar",
                Year = 2014,
                Duration = 169,
                Synopsis = "A team of explorers travel through a wormhole in space.",
                Poster = "interstellar.jpg",
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAll()
        {
            return Ok(_movies);
        }

        [HttpGet]
            [Route("{id}")]

            public ActionResult<Movie> GetById(int id)
            {
                var movie = _movies.FirstOrDefault(m => m.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
            [HttpPost]

            public ActionResult<Movie> Create(Movie movie)
            {
            movie.Id = _movies.Max(m => m.Id) + 1;
            movie.Created = DateTime.UtcNow.ToString();
            movie.Updated = DateTime.UtcNow.ToString();
            _movies.Add(movie);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
            }

        [HttpPut]
            [Route("{id}")]
            public ActionResult Update(int Id, Movie updateMovie)
            {
                var movie = _movies.FirstOrDefault(m => m.Id == Id);
                if (movie == null)
                {
                    return NotFound();
                }
                movie.Title = updateMovie.Title;
                movie.Year = updateMovie.Year;
                movie.Duration = updateMovie.Duration;
                movie.Synopsis = updateMovie.Synopsis;
                movie.Poster = updateMovie.Poster;
                movie.Updated = DateTime.UtcNow.ToString();
                return NoContent();
            }
        [HttpDelete]
            [Route("{id}")]
            public ActionResult Delete(int Id)
            { 
                var movie = _movies.FirstOrDefault(m => m.Id == Id);
                if (movie == null)
                {
                    return NotFound();
                }
            _movies.Remove(movie);
            return NoContent();
            }
    }
}
