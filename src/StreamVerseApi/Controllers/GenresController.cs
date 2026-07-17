using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerseApi.Data;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;
using StreamVerseApi.Models.Entities;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenresController : BaseController
    {
        public GenresController(DataContext context) : base(context) { }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GenreDto>>> GetAll()
        {
            var genres = await Context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                })
                .ToListAsync();

            return ApiResponse<IEnumerable<GenreDto>>.SuccessResponse(genres);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<GenreDto>> GetById(int id)
        {
            var genre = await Context.Genres
                .Where(g => g.Id == id)
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                })
                .FirstOrDefaultAsync();

            if (genre == null)
            {
                return ApiResponse<GenreDto>.FailureResponse("Genre not found", 404);
            }

            return ApiResponse<GenreDto>.SuccessResponse(genre);
        }

        [HttpPost]
        public async Task<ApiResponse<Genre>> Create(CreateGenreDto request)
        {
            var genre = new Genre
            {
                Name = request.Name,
                Description = request.Description
            };
            Context.Genres.Add(genre);
            await Context.SaveChangesAsync();
            return ApiResponse<Genre>.SuccessResponse(genre, 201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Genre updatedGenre)
        {
            var genre = await Context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = updatedGenre.Name;
            genre.Description = updatedGenre.Description;
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await Context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            Context.Genres.Remove(genre);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}