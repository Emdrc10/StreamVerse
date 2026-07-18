using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerse.Infraestructure;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SerieController : BaseController
    {
        private readonly UnitOfWork _unitOfWork;

        public SerieController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ApiResponse<IEnumerable<SerieDto>>> GetAll()
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

        [HttpGet("{id}")]
        public async Task<ApiResponse<SerieDto>> GetById(int id)
        {
            var serie = await _unitOfWork.Serie.GetByIdAsync(id);
            if (serie == null)
            {
                return ApiResponse<SerieDto>.FailureResponse("Serie not found", 404);
            }
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

        [HttpPost]
        public async Task<ApiResponse<Serie>> Create(CreateSerieDto request)
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Serie updatedSerie)
        {
            await _unitOfWork.Serie.UpdateAsync(id, updatedSerie);
            _unitOfWork.complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitOfWork.Serie.DeleteAsync(id);
            _unitOfWork.complete();
            return NoContent();
        }
    }
}