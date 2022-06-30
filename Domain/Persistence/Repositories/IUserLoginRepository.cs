using Algorithms.API.Domain.Models;

namespace Algorithms.API.Domain.Persistence.Repositories
{
    public interface IUserLoginRepository
    {
        // General Methods
        Task<IEnumerable<UserLogin>> ListAsync();
        Task AddAsync(UserLogin userLogin);
        Task<UserLogin> FindById(Guid id);
        void Update(UserLogin userLogin);
        void Remove(UserLogin userLogin);

        // Methods for User Entity
        Task<UserLogin> GetByUserIdAsync(Guid userId);
        Task AssingUserToUserLogin(Guid userId, Guid userLoginId);
        Task UnassingUserToUserLogin(Guid userId, Guid userLoginId);
    }
}
