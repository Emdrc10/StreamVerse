using Microsoft.AspNetCore.Mvc;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerseApi.DTOs.Movies;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;


namespace StreamVerseApi.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;

        public MoviesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<MovieDto>>> GetAll()
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

        [HttpGet("{id}")]
        public async Task<ApiResponse<MovieDto>> GetById(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(id);
            if (movie == null)
            {
                return ApiResponse<MovieDto>.FailureResponse("Movie not found", 404);
            }
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

        [HttpPost]
        public async Task<ApiResponse<Movie>> Create(CreateMovieDto request)
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Movie updatedMovie)
        {
            await _unitOfWork.Movie.UpdateAsync(id, updatedMovie);
            _unitOfWork.complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.Movie.DeleteAsync(id);
            _unitOfWork.complete();
            return NoContent();
        }
    }
}

