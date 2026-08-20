using Microsoft.AspNetCore.Mvc;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Application.Services;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerse.Aplication.Models;
using StreamVerse.Aplication.Models.Responses;

namespace StreamVerseApi.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public async Task<ApiResponse<IEnumerable<MovieDto>>> GetAll()
        {
            return await _movieService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<MovieDto>> GetById(int id)
            => await _movieService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<Movie>> Create(CreateMovieDto request)
            => await _movieService.CreateAsync(request);

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateMovieDto updatedMovie)
        {
            await _movieService.UpdateAsync(id, updatedMovie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _movieService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ApiResponse<IEnumerable<MovieDto>>> Search([FromQuery] string? title, [FromQuery] int? genreId)
        => await _movieService.SearchAsync(title, genreId);
    }
}

