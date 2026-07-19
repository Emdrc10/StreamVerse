using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerse.Application.Services
{
    public class GenreService
    {
        private readonly UnitOfWork _unitOfWork;

        public GenreService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _unitOfWork.Genre.GetAllAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Genre.GetByIdAsync(id);
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            await _unitOfWork.Genre.CreateAsync(genre);
            _unitOfWork.complete();
            return genre;
        }

        public async Task UpdateAsync(int id, Genre updatedGenre)
        {
            await _unitOfWork.Genre.UpdateAsync(id, updatedGenre);
            _unitOfWork.complete();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Genre.DeleteAsync(id);
            _unitOfWork.complete();
        }
    }
}
