using Microsoft.AspNetCore.Mvc;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;
using StreamVerseApi.Models;
using StreamVerseApi.Models.Dtos;

namespace StreamVerseApi.Controllers
{
    namespace StreamVerseApi.Controllers
    {
        public class UsersController : BaseController
        {
            private readonly UnitOfWork _unitOfWork;

            public UsersController(UnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            [HttpGet]
            public async Task<ApiResponse<IEnumerable<UserDto>>> GetAll()
            {
                var users = await _unitOfWork.User.GetAllAsync();
                var result = users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                });
                return ApiResponse<IEnumerable<UserDto>>.SuccessResponse(result);
            }

            [HttpGet("{id}")]
            public async Task<ApiResponse<UserDto>> GetById(int id)
            {
                var user = await _unitOfWork.User.GetByIdAsync(id);
                if (user == null)
                {
                    return ApiResponse<UserDto>.FailureResponse("User not found", 404);
                }
                return ApiResponse<UserDto>.SuccessResponse(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                });
            }

            [HttpPost]
            public async Task<ApiResponse<User>> Create(CreateUserDto request)
            {
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Created = DateTime.UtcNow.ToString(),
                    Updated = DateTime.UtcNow.ToString()
                };
                await _unitOfWork.User.CreateAsync(user);
                _unitOfWork.complete();
                return ApiResponse<User>.SuccessResponse(user, 201);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> Update(int id, User updatedUser)
            {
                await _unitOfWork.User.UpdateAsync(id, updatedUser);
                _unitOfWork.complete();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(int id)
            {
                await _unitOfWork.User.DeleteAsync(id);
                _unitOfWork.complete();
                return NoContent();
            }
        }
    }
}
