using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;

namespace StreamVerseApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
        }
        public DbSet<Movie> Movies{ get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
