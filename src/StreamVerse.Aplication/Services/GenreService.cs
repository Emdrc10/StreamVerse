using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerse.Aplication.Models.Dtos;

namespace StreamVerse.Application.Services
{
    public class GenreService
    {
        private readonly UnitOfWork _unitOfWork;

        public GenreService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<GenreDto>>> GetAllAsync()
        {
            var genres = await _unitOfWork.Genre.GetAllAsync();
            var result = genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            });
            return ApiResponse<IEnumerable<GenreDto>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<GenreDto>> GetByIdAsync(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            if (genre == null)
                return ApiResponse<GenreDto>.FailureResponse("Genre not found", 404);
            return ApiResponse<GenreDto>.SuccessResponse(new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            });
        }

        public async Task<ApiResponse<Genre>> CreateAsync(CreateGenreDto request)
        {
            var genre = new Genre
            {
                Name = request.Name ?? "",
                Description = request.Description ?? ""
            };
            await _unitOfWork.Genre.CreateAsync(genre);
            _unitOfWork.complete();
            return ApiResponse<Genre>.SuccessResponse(genre, 201);
        }

        public async Task UpdateAsync(int id, CreateGenreDto request)
        {
            var genre = new Genre
            {
                Name = request.Name ?? "",
                Description = request.Description ?? ""
            };
            await _unitOfWork.Genre.UpdateAsync(id, genre);
            _unitOfWork.complete();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _unitOfWork.Genre.DeleteAsync(id);
            _unitOfWork.complete();
            return deleted;
        }
    }
}
