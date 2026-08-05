using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;

namespace StreamVerse.Infraestructure.Repositories
{
    public class SerieRepository
    {
        private readonly DataContext _context;

        public SerieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Serie>> GetAllAsync()
        {
            return await _context.Series
                .Include(s => s.Genre)
                .ToListAsync();
        }

        public async Task<Serie?> GetByIdAsync(int id)
        {
            return await _context.Series
                .Include(s => s.Genre)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Serie> CreateAsync(Serie serie)
        {
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();
            return serie;
        }

        public async Task UpdateAsync(int id, Serie updatedSerie)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null) return;
            serie.Title = updatedSerie.Title;
            serie.Year = updatedSerie.Year;
            serie.Seasons = updatedSerie.Seasons;
            serie.Episodes = updatedSerie.Episodes;
            serie.Synopsis = updatedSerie.Synopsis;
            serie.Poster = updatedSerie.Poster;
            serie.GenreId = updatedSerie.GenreId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null) return;
            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();
        }
    }
}
