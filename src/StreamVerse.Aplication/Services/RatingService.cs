using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerse.Aplication.Services
{
    public class RatingService
    {
        private readonly UnitOfWork _unitOfWork;

        public RatingService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<RatingDto>>> GetAllAsync() 
        {
            var ratings = await _unitOfWork.Rating.GetAllAsync();
            var result = ratings.Select(r => new RatingDto
            {
                Id = r.Id,
                Score = r.score,
                Review = r.review,
                UserName = r.user?.Name,
                MovieTitle = r.movie?.Title,
                SerieTitle = r.serie?.Title
            });
            return ApiResponse<IEnumerable<RatingDto>>.SuccessResponse(result);
        }

        public async Task<ApiResponse<RatingDto>> GetByIdAsync(int id)
        {
            var rating = await _unitOfWork.Rating.GetByIdAsync(id);
            if (rating == null)
                return ApiResponse<RatingDto>.FailureResponse("Rating not found", 404);
            return ApiResponse<RatingDto>.SuccessResponse(new RatingDto
            {
                Id = rating.Id,
                Score = rating.score,
                Review = rating.review,
                UserName = rating.user?.Name,
                MovieTitle = rating.movie?.Title,
                SerieTitle = rating.serie?.Title
            });
        }

        public async Task<ApiResponse<RatingDto>> CreateAsync(CreateRatingDto request)
        {
            var rating = new Rating
            {
                score = request.Score,
                review = request.Review,
                userId = request.UserId,
                movieId = request.MovieId,
                serieId = request.SerieId,
                created = DateTime.UtcNow.ToString(),
                updated = DateTime.UtcNow.ToString()
            };
            await _unitOfWork.Rating.CreateAsync(rating);
            _unitOfWork.complete();

            var created = await _unitOfWork.Rating.GetByIdAsync(rating.Id);
            return ApiResponse<RatingDto>.SuccessResponse(new RatingDto
            {
                Id = created.Id,
                Score = created.score,
                Review = created.review,
                UserName = created.user?.Name,
                MovieTitle = created.movie?.Title,
                SerieTitle = created.serie?.Title
            }, 201);
        }


        public async Task UpdateAsync(int id, Rating updatedRating)
        {
            await _unitOfWork.Rating.UpdateAsync(id, updatedRating);
            _unitOfWork.complete();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Rating.DeleteAsync(id);
            _unitOfWork.complete();
        }
    }
}
