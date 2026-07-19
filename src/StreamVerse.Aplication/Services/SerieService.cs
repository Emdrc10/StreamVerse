using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;


namespace StreamVerse.Application.Services
{
    public class SerieService
    {
        private readonly UnitOfWork _unitOfWork;

        public SerieService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Serie>> GetAllAsync()
        {
            return await _unitOfWork.Serie.GetAllAsync();
        }

        public async Task<Serie?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Serie.GetByIdAsync(id);
        }

        public async Task<Serie> CreateAsync(Serie serie)
        {
            await _unitOfWork.Serie.CreateAsync(serie);
            _unitOfWork.complete();
            return serie;
        }

        public async Task UpdateAsync(int id, Serie updatedSerie)
        {
            await _unitOfWork.Serie.UpdateAsync(id, updatedSerie);
            _unitOfWork.complete();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Serie.DeleteAsync(id);
            _unitOfWork.complete();
        }
    }
}