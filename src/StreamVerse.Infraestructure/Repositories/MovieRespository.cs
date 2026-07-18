using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;

namespace StreamVerse.Infraestructure.Repositories
{
    public class MovieRepository
    {
        private readonly DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task UpdateAsync(int id, Movie updatedMovie)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return;
            movie.Title = updatedMovie.Title;
            movie.Year = updatedMovie.Year;
            movie.Duration = updatedMovie.Duration;
            movie.Synopsis = updatedMovie.Synopsis;
            movie.Poster = updatedMovie.Poster;
            movie.GenreId = updatedMovie.GenreId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return;
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}
