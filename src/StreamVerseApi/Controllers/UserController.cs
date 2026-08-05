using Microsoft.AspNetCore.Mvc;
using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Application.Services;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerseApi.Controllers
{
    public class UsersController : BaseController
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAll()
             => await _userService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ApiResponse<UserDto>> GetById(int id)
           => await _userService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResponse<User>> Create(CreateUserDto request)
            => await _userService.CreateAsync(request);
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, User updatedUser)
        {
            await _userService.UpdateAsync(id, updatedUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
