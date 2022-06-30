using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Persistence.Repositories;
using Algorithms.API.Domain.Services;
using Algorithms.API.Domain.Services.Communications;

namespace Algorithms.API.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        private readonly IAlgorithmRepository _algorithmRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AlgorithmService(IAlgorithmRepository algorithmRepository, IUnitOfWork unitOfWork)
        {
            _algorithmRepository = algorithmRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods
        public async Task<AlgorithmResponse> SaveAsync(Algorithm algorithm)
        {
            try
            {
                await _algorithmRepository.AddAsync(algorithm);
                await _unitOfWork.CompleteAsync();

                return new AlgorithmResponse(algorithm);
            }
            catch (Exception ex)
            {
                return new AlgorithmResponse($"An error while saving Algorithm:{ex.Message}");
            }
        }

        public async Task<IEnumerable<Algorithm>> ListAsync()
        {
            var all = await _algorithmRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<AlgorithmResponse> GetById(Guid algorithmId)
        {
            var existingAlgorithm = await _algorithmRepository.GetById(algorithmId);
            if (existingAlgorithm == null || !existingAlgorithm.IsActive)
                return new AlgorithmResponse("Algorithm Not Found");
            return new AlgorithmResponse(existingAlgorithm);
        }

        public async Task<AlgorithmResponse> Update(Guid algorithmId, Algorithm algorithm)
        {
            var existingAlgorithm = await _algorithmRepository.GetById(algorithmId);
            if (existingAlgorithm == null)
                return new AlgorithmResponse("Algorithm Not Found");

            existingAlgorithm.Name = algorithm.Name;
            existingAlgorithm.Description = algorithm.Description;
            existingAlgorithm.IsActive = algorithm.IsActive;
            existingAlgorithm.CreatedOn = algorithm.CreatedOn;
            existingAlgorithm.ModifiedOn = algorithm.ModifiedOn;

            try
            {
                _algorithmRepository.Update(existingAlgorithm);
                await _unitOfWork.CompleteAsync();
                return new AlgorithmResponse(existingAlgorithm);
            }
            catch (Exception ex)
            {
                return new AlgorithmResponse($"An error while updating Algorithm: {ex.Message}");
            }
        }

        public async Task<AlgorithmResponse> Delete(Guid algorithmId)
        {
            var existingAlgorithm = await _algorithmRepository.GetById(algorithmId);
            if (existingAlgorithm == null)
                return new AlgorithmResponse("Algorithm Not Found");
            try
            {
                existingAlgorithm.IsActive = false;
                _algorithmRepository.Update(existingAlgorithm);
                await _unitOfWork.CompleteAsync();
                return new AlgorithmResponse(existingAlgorithm);
            }
            catch (Exception ex)
            {
                return new AlgorithmResponse($"An error ocurrend while deleting Algorithm: {ex.Message}");
            }
        }


        // Methods for User Entity
        public async Task<IEnumerable<Algorithm>> ListByUserId(Guid userId)
        {
            return await _algorithmRepository.ListByUserIdAsync(userId);
        }

        public async Task<AlgorithmResponse> AssignAlgorithmToUser(Guid userId, Guid algorithmId)
        {
            try
            {
                await _algorithmRepository.AssingAlgorithmToUser(userId, algorithmId);
                await _unitOfWork.CompleteAsync();
                Algorithm Algorithm = await _algorithmRepository.GetById(algorithmId);
                return new AlgorithmResponse(Algorithm);
            }
            catch (Exception ex)
            {
                return new AlgorithmResponse($"An error ocurrend while assigning Algorithm to user: {ex.Message}");
            }
        }

        public async Task<AlgorithmResponse> UnassignAlgorithmToUser(Guid userId, Guid algorithmId)
        {
            try
            {
                await _algorithmRepository.UnassingAlgorithmToUser(userId, algorithmId);
                await _unitOfWork.CompleteAsync();
                Algorithm Algorithm = await _algorithmRepository.GetById(algorithmId);
                return new AlgorithmResponse(Algorithm);
            }
            catch (Exception ex)
            {
                return new AlgorithmResponse($"An error ocurrend while unassigning Algorithm to user: {ex.Message}");
            }
        }

    }
}
