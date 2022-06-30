using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Services.Communications;

namespace Algorithms.API.Domain.Services
{
    public interface IAlgorithmService
    {
        // General Methods
        Task<AlgorithmResponse> SaveAsync(Algorithm algorithm);
        Task<IEnumerable<Algorithm>> ListAsync();
        Task<AlgorithmResponse> GetById(Guid algorithmId);
        Task<AlgorithmResponse> Update(Guid algorithmId, Algorithm algorithm);
        Task<AlgorithmResponse> Delete(Guid algorithmId);

        // Methods for User Entity
        Task<IEnumerable<Algorithm>> ListByUserId(Guid userId);
        Task<AlgorithmResponse> AssignAlgorithmToUser(Guid userId, Guid algorithmId);
        Task<AlgorithmResponse> UnassignAlgorithmToUser(Guid userId, Guid algorithmId);
    }
}
