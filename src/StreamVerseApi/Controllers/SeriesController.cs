using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;
using StreamVerseApi.Data;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SerieController : BaseController
    {
        public SerieController(DataContext context) : base(context) { }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<SerieDto>>> GetAll()
        {
            var series = await Context.Series
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

            return ApiResponse<IEnumerable<SerieDto>>.SuccessResponse(series);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<SerieDto>> GetById(int id)
        {
            var serie = await Context.Series
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
                return ApiResponse<SerieDto>.FailureResponse("Serie not found", 404);
            }

            return ApiResponse<SerieDto>.SuccessResponse(serie);
        }

        [HttpPost]
        public async Task<ApiResponse<Serie>> Create(CreateSerieDto request)
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
            Context.Series.Add(serie);
            await Context.SaveChangesAsync();
            return ApiResponse<Serie>.SuccessResponse(serie, 201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Serie updatedSerie)
        {
            var serie = await Context.Series.FindAsync(id);
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
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var serie = await Context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            Context.Series.Remove(serie);
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}