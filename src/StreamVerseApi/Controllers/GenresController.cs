using Microsoft.AspNetCore.Mvc;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;

namespace StreamVerseApi.Controllers
{
    public class GenresController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;

        public GenresController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GenreDto>>> GetAll()
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

        [HttpGet("{id}")]
        public async Task<ApiResponse<GenreDto>> GetById(int id)
        {
            var genre = await _unitOfWork.Genre.GetByIdAsync(id);
            if (genre == null)
            {
                return ApiResponse<GenreDto>.FailureResponse("Genre not found", 404);
            }
            return ApiResponse<GenreDto>.SuccessResponse(new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            });
        }

        [HttpPost]
        public async Task<ApiResponse<Genre>> Create(CreateGenreDto request)
        {
            var genre = new Genre
            {
                Name = request.Name,
                Description = request.Description
            };
            await _unitOfWork.Genre.CreateAsync(genre);
            _unitOfWork.complete();
            return ApiResponse<Genre>.SuccessResponse(genre, 201);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Genre updatedGenre)
        {
            await _unitOfWork.Genre.UpdateAsync(id, updatedGenre);
            _unitOfWork.complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.Genre.DeleteAsync(id);
            _unitOfWork.complete();
            return NoContent();
        }
    }
}