using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;
using StreamVerseApi.Data;
using StreamVerseApi.Models.Dtos;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenresController : ControllerBase
    {
        private readonly DataContext _context;

        public GenresController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            var genres = await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                })
                .ToListAsync();

            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _context.Genres
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
                return NotFound();
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> Create(CreateGenreDto request)
        {
            var genre = new Genre
            {
                Name = request.Name,
                Description = request.Description
            };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Genre updatedGenre)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = updatedGenre.Name;
            genre.Description = updatedGenre.Description;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}