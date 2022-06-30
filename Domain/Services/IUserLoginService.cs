using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Services.Communications;

namespace Algorithms.API.Domain.Services
{
    public interface IUserLoginService
    {
        // General Methods
        Task<IEnumerable<UserLogin>> ListAsync();
        Task<UserLoginResponse> GetByIdAsync(Guid id);
        Task<UserLoginResponse> SaveAsync(UserLogin userLogin);
        Task<UserLoginResponse> UpdateAsync(Guid id, UserLogin userLogin);
        Task<UserLoginResponse> DeleteAsync(Guid id);

        // Methods for User Entity
        Task<UserLogin> GetByUserId(Guid userId);
        Task<UserLoginResponse> AssingUserToUserLogin(Guid userId, Guid userLoginId);
        Task<UserLoginResponse> UnassingUserToUserLogin(Guid userId, Guid userLoginId);
    }
}
