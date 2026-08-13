using Microsoft.AspNetCore.Mvc;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Aplication.Services;
using StreamVerse.Domain.Entities;

namespace StreamVerseApi.Controllers;
public class RatingsController : BaseController
{
    private readonly RatingService _ratingService;

    public RatingsController(RatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<RatingDto>>> GetAll()
        => await _ratingService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ApiResponse<RatingDto>> GetById(int id)
        => await _ratingService.GetByIdAsync(id);

    [HttpPost]
    public async Task<ApiResponse<RatingDto>> Create(CreateRatingDto request)
    => await _ratingService.CreateAsync(request);

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Rating updatedRating)
    {
        await _ratingService.UpdateAsync(id, updatedRating);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _ratingService.DeleteAsync(id);
        return NoContent();
    }
}
