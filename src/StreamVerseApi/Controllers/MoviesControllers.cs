using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;
using StreamVerseApi.Data;
using StreamVerseApi.DTOs.Movies;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;


namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController : BaseController
    {
        public MoviesController(DataContext context) : base(context) { }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<MovieDto>>> GetAll()
        {
            var movies = await Context.Movies
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

            return ApiResponse<IEnumerable<MovieDto>>.SuccessResponse(movies);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<MovieDto>> GetById(int id)
        {
            var movie = await Context.Movies
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
                return ApiResponse<MovieDto>.FailureResponse("Movie not found", 404);
            }

            return ApiResponse<MovieDto>.SuccessResponse(movie);
        }

        [HttpPost]
        public async Task<ApiResponse<Movie>> Create(CreateMovieDto request)
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
            Context.Movies.Add(movie);
            await Context.SaveChangesAsync();
            return ApiResponse<Movie>.SuccessResponse(movie, 201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Movie updatedMovie)
        {
            var movie = await Context.Movies.FindAsync(id);
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
            await Context.SaveChangesAsync();
            return NoContent();
        }   

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await Context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            Context.Movies.Remove(movie);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}

