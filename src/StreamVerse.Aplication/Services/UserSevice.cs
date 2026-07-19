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

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.User.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _unitOfWork.User.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _unitOfWork.User.CreateAsync(user);
            _unitOfWork.complete();
            return user;
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
