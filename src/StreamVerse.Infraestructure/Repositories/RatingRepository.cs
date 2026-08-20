using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;

namespace StreamVerse.Infraestructure.Repositories
{
    public class RatingRepository
    {
        private readonly DataContext _context;

        public RatingRepository(DataContext context)
        {
            _context = context;
        }       

        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            return await _context.Ratings
                .Include(r => r.user)
                .Include(r => r.movie)
                .Include(r => r.serie)
                .ToListAsync();
        }

        public async Task<Rating?> GetByIdAsync(int id)
        {
            return await _context.Ratings
                .Include(r => r.user)
                .Include(r => r.movie)
                .Include(r => r.serie)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rating> CreateAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task UpdateAsync(int id, Rating updatedRating)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return;
            rating.score = updatedRating.score;
            rating.review = updatedRating.review;
            rating.updated = DateTime.UtcNow.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return;
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }
    }
}
