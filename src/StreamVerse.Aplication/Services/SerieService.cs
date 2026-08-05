using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
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

        public async Task<ApiResponse<IEnumerable<SerieDto>>> GetAllAsync()
        {
            var series = await _unitOfWork.Serie.GetAllAsync();
            var result = series.Select(s => new SerieDto
            {
                Id = s.Id,
                Title = s.Title,
                Year = s.Year,
                Seasons = s.Seasons,
                Episodes = s.Episodes,
                Synopsis = s.Synopsis,
                GenreName = s.Genre.Name
            });
            return ApiResponse<IEnumerable<SerieDto>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<SerieDto>> GetByIdAsync(int id)
        {
            var serie = await _unitOfWork.Serie.GetByIdAsync(id);
            if (serie == null)
                return ApiResponse<SerieDto>.FailureResponse("Serie not found", 404);
            return ApiResponse<SerieDto>.SuccessResponse(new SerieDto
            {
                Id = serie.Id,
                Title = serie.Title,
                Year = serie.Year,
                Seasons = serie.Seasons,
                Episodes = serie.Episodes,
                Synopsis = serie.Synopsis,
                GenreName = serie.Genre.Name
            });
        }
        public async Task<ApiResponse<Serie>> CreateAsync(CreateSerieDto request)
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
            await _unitOfWork.Serie.CreateAsync(serie);
            _unitOfWork.complete();
            return ApiResponse<Serie>.SuccessResponse(serie, 201);
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