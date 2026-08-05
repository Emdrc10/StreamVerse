using StreamVerse.Aplication.Models.Dtos;
using StreamVerse.Aplication.Models.Responses;
using StreamVerse.Domain.Entities;
using StreamVerse.Infraestructure.Repositories;

namespace StreamVerse.Application.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllAsync()
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

        public async Task<ApiResponse<UserDto>> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null)
                return ApiResponse<UserDto>.FailureResponse("User not found", 404);
            return ApiResponse<UserDto>.SuccessResponse(new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }
        public async Task<ApiResponse<User>> CreateAsync(CreateUserDto request)
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

        public async Task UpdateAsync(int id, User updatedUser)
        {
            await _unitOfWork.User.UpdateAsync(id, updatedUser);
            _unitOfWork.complete();
        }
        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.User.DeleteAsync(id);
            _unitOfWork.complete();
        }
    }
}
