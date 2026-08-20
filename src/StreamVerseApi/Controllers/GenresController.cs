using Microsoft.AspNetCore.Mvc;
using StreamVerse.Application.Services;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;

namespace StreamVerseApi.Controllers
{
    public class GenresController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly GenreService _genreService;

        public GenresController(UnitOfWork unitOfWork, GenreService genreService)
        {
            _unitOfWork = unitOfWork;   
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<GenreDto>>> GetAll()
        {
            return await _genreService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<GenreDto>> GetById(int id)
            => await _genreService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<Genre>> Create(CreateGenreDto request)
          => await _genreService.CreateAsync(request);

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateGenreDto updatedGenre)
        {
            await _genreService.UpdateAsync(id, updatedGenre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _genreService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("No se puede eliminar el género porque tiene películas o series asociadas.");
            return NoContent();
        }
    }
}