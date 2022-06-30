using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Persistence.Repositories;
using Algorithms.API.Domain.Services;
using Algorithms.API.Domain.Services.Communications;

namespace Algorithms.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods

        public async Task<IEnumerable<User>> ListAsync()
        {
            var all = await _userRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<UserResponse> DeleteAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User Not Found");

            try
            {
                existingUser.IsActive = false;
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting user: {ex.Message}");
            }

        }

        public async Task<UserResponse> GetByIdAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null || !existingUser.IsActive)
                return new UserResponse("User Not Found");

            return new UserResponse(existingUser);
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User Not Found");

            existingUser.Name = user.Name;
            existingUser.LastName = user.LastName;
            existingUser.IsActive = user.IsActive;
            existingUser.CreatedOn = user.CreatedOn;
            existingUser.ModifiedOn = user.ModifiedOn;

            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating user: {ex.Message}");
            }
        }

    }
}
