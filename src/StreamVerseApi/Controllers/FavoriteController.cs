using Microsoft.AspNetCore.Mvc;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Aplication.Services;

namespace StreamVerseApi.Controllers
{
    public class FavoritesController : BaseController
    {
        private readonly FavoriteService _favoriteService;

        public FavoritesController(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<FavoriteDto>>> GetAll()
            => await _favoriteService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<FavoriteDto>> GetById(int id)
            => await _favoriteService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<FavoriteDto>> Create(CreateFavoriteDto request)
            => await _favoriteService.CreateAsync(request);

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _favoriteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
