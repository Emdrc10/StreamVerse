using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerseApi.Data;
using StreamVerseApi.Models.Dtos;
using StreamVerseApi.Models.Entities;


namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Duration = m.Duration,
                    Synopsis = m.Synopsis,
                    GenreName = m.Genre.Name
                })
                .ToListAsync();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetById(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.Id == id)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Duration = m.Duration,
                    Synopsis = m.Synopsis,
                    GenreName = m.Genre.Name
                })
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Create(DTOs.Movies.CreateMovieDto request)
        {
            var movie = new Movie   
            {
                Title = request.Title,
                Year = request.Year,
                Duration = request.Duration,
                Synopsis = request.Synopsis,
                Poster = request.Poster,
                GenreId = request.GenreId,
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Movie updatedMovie)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            movie.Title = updatedMovie.Title;
            movie.Year = updatedMovie.Year;
            movie.Duration = updatedMovie.Duration;
            movie.Synopsis = updatedMovie.Synopsis;
            movie.Poster = updatedMovie.Poster;
            movie.GenreId = updatedMovie.GenreId;
            movie.Updated = DateTime.UtcNow.ToString();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

