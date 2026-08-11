using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;

namespace StreamVerse.Infraestructure.Repositories
{
    public class FavoriteRepository
    {
        private readonly DataContext _context;

        public FavoriteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetAllAsync()
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Movie)
                .Include(f => f.Serie)
                .ToListAsync();
        }

        public async Task<Favorite?> GetByIdAsync(int id)
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Include(f => f.Movie)
                .Include(f => f.Serie)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Favorite> CreateAsync(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task DeleteAsync(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null) return;
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }
    }
}
