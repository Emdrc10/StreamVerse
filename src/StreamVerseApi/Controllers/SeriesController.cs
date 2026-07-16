using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerseApi.Data;
using StreamVerseApi.Models.Dtos;
using StreamVerseApi.Models.Entities;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SerieController : ControllerBase
    {
        private readonly DataContext _context;

        public SerieController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerieDto>>> GetAll()
        {
            var series = await _context.Series
                .Include(s => s.Genre)
                .Select(s => new SerieDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Year = s.Year,
                    Seasons = s.Seasons,
                    Episodes = s.Episodes,
                    Synopsis = s.Synopsis,
                    GenreName = s.Genre.Name
                })
                .ToListAsync();

            return Ok(series);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SerieDto>> GetById(int id)
        {
            var serie = await _context.Series
                .Include(s => s.Genre)
                .Where(s => s.Id == id)
                .Select(s => new SerieDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Year = s.Year,
                    Seasons = s.Seasons,
                    Episodes = s.Episodes,
                    Synopsis = s.Synopsis,
                    GenreName = s.Genre.Name
                })
                .FirstOrDefaultAsync();

            if (serie == null)
            {
                return NotFound();
            }

            return Ok(serie);
        }

        [HttpPost]
        public async Task<ActionResult<Serie>> Create(CreateSerieDto request)
        {
            var serie = new Serie
            {
                Title = request.Title,
                Year = request.Year,
                Seasons = request.Seasons,
                Episodes = request.Episodes,
                Synopsis = request.Synopsis,
                Poster = request.Poster,
                GenreId = request.GenreId,
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            };
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = serie.Id }, serie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Serie updatedSerie)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            serie.Title = updatedSerie.Title;
            serie.Year = updatedSerie.Year;
            serie.Seasons = updatedSerie.Seasons;
            serie.Episodes = updatedSerie.Episodes;
            serie.Synopsis = updatedSerie.Synopsis;
            serie.Poster = updatedSerie.Poster;
            serie.GenreId = updatedSerie.GenreId;
            serie.Updated = DateTime.UtcNow.ToString();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}