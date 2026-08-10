using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerse.Aplication.Services
{
    public class FavoriteService
    {
        private readonly UnitOfWork _unitOfWork;

        public FavoriteService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<FavoriteDto>>> GetAllAsync()
        {
            var favorites = await _unitOfWork.Favorite.GetAllAsync();
            var result = favorites.Select(f => new FavoriteDto
            {
                Id = f.Id,
                UserName = f.User?.Name,
                MovieTitle = f.Movie?.Title,
                SerieTitle = f.Serie?.Title,
                Created = f.Created
            });
            return ApiResponse<IEnumerable<FavoriteDto>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<FavoriteDto>> GetByIdAsync(int id)
        {
            var favorite = await _unitOfWork.Favorite.GetByIdAsync(id);
            if (favorite == null)
                return ApiResponse<FavoriteDto>.FailureResponse("Favorite not found", 404);
            return ApiResponse<FavoriteDto>.SuccessResponse(new FavoriteDto
            {
                Id = favorite.Id,
                UserName = favorite.User?.Name,
                MovieTitle = favorite.Movie?.Title,
                SerieTitle = favorite.Serie?.Title,
                Created = favorite.Created
            });
        }

        public async Task<ApiResponse<FavoriteDto>> CreateAsync(CreateFavoriteDto request)
        {
            var favorite = new Favorite
            {
                UserId = request.UserId,
                MovieId = request.MovieId,
                SerieId = request.SerieId,
                Created = DateTime.UtcNow.ToString()
            };
            await _unitOfWork.Favorite.CreateAsync(favorite);
            _unitOfWork.complete();

            var created = await _unitOfWork.Favorite.GetByIdAsync(favorite.Id);
            return ApiResponse<FavoriteDto>.SuccessResponse(new FavoriteDto
            {
                Id = created.Id,
                UserName = created.User?.Name,
                MovieTitle = created.Movie?.Title,
                SerieTitle = created.Serie?.Title,
                Created = created.Created
            }, 201);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Favorite.DeleteAsync(id);
            _unitOfWork.complete();
        }
    }
}