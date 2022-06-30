using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Services.Communications;

namespace Algorithms.API.Domain.Services
{
    public interface IUserService
    {
        // General Methods
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> GetByIdAsync(Guid id);
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(Guid id, User user);
        Task<UserResponse> DeleteAsync(Guid id);
    }
}
