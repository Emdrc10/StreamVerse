using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Application.Services;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerseApi.Controllers
{
    public class SerieController : BaseController
    {
        private readonly SerieService _serieService;

        public SerieController(SerieService serieService)
        {
            _serieService = serieService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<SerieDto>>> GetAll()
             => await _serieService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<SerieDto>> GetById(int id)
            => await _serieService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<Serie>> Create(CreateSerieDto request)
            => await _serieService.CreateAsync(request);
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Serie updatedSerie)
        {
            await _serieService.UpdateAsync(id, updatedSerie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _serieService.DeleteAsync(id);
            return NoContent();
        }
    }
}