using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerse.Application.Services
{
    public class MovieService
    {
        private readonly UnitOfWork _unitOfWork;

        public MovieService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _unitOfWork.Movie.GetAllAsync();
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Movie.GetByIdAsync(id);
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _unitOfWork.Movie.CreateAsync(movie);
            _unitOfWork.complete();
            return movie;
        }

        public async Task UpdateAsync(int id, Movie updatedMovie)
        {
            await _unitOfWork.Movie.UpdateAsync(id, updatedMovie);
            _unitOfWork.complete();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Movie.DeleteAsync(id);
            _unitOfWork.complete();
        }
    }
}