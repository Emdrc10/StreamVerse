using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
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

        public async Task<ApiResponse<IEnumerable<MovieDto>>> GetAllAsync()
        {
            var movies = await _unitOfWork.Movie.GetAllAsync();
            var result = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Duration = m.Duration,
                Synopsis = m.Synopsis,
                GenreName = m.Genre.Name
            });
            return ApiResponse<IEnumerable<MovieDto>>.SuccessResponse(result);
        }
        public async Task<ApiResponse<MovieDto>> GetByIdAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(id);
            if (movie == null)
                return ApiResponse<MovieDto>.FailureResponse("Movie not found", 404);
            return ApiResponse<MovieDto>.SuccessResponse(new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Duration = movie.Duration,
                Synopsis = movie.Synopsis,
                GenreName = movie.Genre.Name
            });
        }

        public async Task<ApiResponse<Movie>> CreateAsync(CreateMovieDto request)
        {
            var movie = new Movie
            {
                Title = request.Title,
                Year = request.Year,
                Duration = request.Duration,
                Synopsis = request.Synopsis,
                Poster = request.Poster,
                GenreId = request.GenreId,
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            };
            await _unitOfWork.Movie.CreateAsync(movie);
            _unitOfWork.complete();
            return ApiResponse<Movie>.SuccessResponse(movie, 201);
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

        public async Task<ApiResponse<IEnumerable<MovieDto>>> SearchAsync(string? title, int? genreId)
        {
            var query = await _unitOfWork.Movie.GetAllAsync();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (genreId.HasValue)
                query = query.Where(m => m.GenreId == genreId);

            var result = query.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Duration = m.Duration,
                Synopsis = m.Synopsis,
                GenreName = m.Genre?.Name
            });

            return ApiResponse<IEnumerable<MovieDto>>.SuccessResponse(result);
        }   
    }
}