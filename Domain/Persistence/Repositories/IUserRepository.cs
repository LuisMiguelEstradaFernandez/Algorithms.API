using Algorithms.API.Domain.Models;

namespace Algorithms.API.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        // General Methods
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User user);
        Task<User> FindById(Guid id);
        void Update(User user);
        void Remove(User user);
    }
}
