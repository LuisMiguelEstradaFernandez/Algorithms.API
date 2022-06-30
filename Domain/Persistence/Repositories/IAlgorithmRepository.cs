using Algorithms.API.Domain.Models;

namespace Algorithms.API.Domain.Persistence.Repositories
{
    public interface IAlgorithmRepository
    {
        // General Methods
        Task AddAsync(Algorithm algorithm);
        Task<IEnumerable<Algorithm>> ListAsync();
        Task<Algorithm> GetById(Guid algorithmId);
        void Update(Algorithm algorithm);
        void Remove(Algorithm algorithm);

        // Methods for User Entity
        Task<IEnumerable<Algorithm>> ListByUserIdAsync(Guid userId);
        Task AssingAlgorithmToUser(Guid userId, Guid algorithmId);
        Task UnassingAlgorithmToUser(Guid userId, Guid algorithmId);
    }
}
