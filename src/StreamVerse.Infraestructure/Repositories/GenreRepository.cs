using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;

namespace StreamVerse.Infraestructure.Repositories
{
    public class GenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task UpdateAsync(int id, Genre updatedGenre)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return;
            genre.Name = updatedGenre.Name;
            genre.Description = updatedGenre.Description;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return;
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
