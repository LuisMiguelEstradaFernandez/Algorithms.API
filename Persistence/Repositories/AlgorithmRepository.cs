using Microsoft.EntityFrameworkCore;
using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Persistence.Contexts;
using Algorithms.API.Domain.Persistence.Repositories;

namespace Algorithms.API.Persistence.Repositories
{
    public class AlgorithmRepository : BaseRepository, IAlgorithmRepository
    {
        public AlgorithmRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods

        public async Task AddAsync(Algorithm algorithm)
        {
            algorithm.IsActive = true;
            await _context.Algorithms.AddAsync(algorithm);
        }

        public async Task<IEnumerable<Algorithm>> ListAsync()
        {
            return await _context.Algorithms.ToListAsync();
        }

        public async Task<Algorithm> GetById(Guid algorithmId)
        {
            return await _context.Algorithms.FindAsync(algorithmId);
        }

        public void Update(Algorithm algorithm)
        {
            _context.Algorithms.Update(algorithm);
        }

        public void Remove(Algorithm algorithm)
        {
            algorithm.IsActive = false;
            _context.Algorithms.Remove(algorithm);
        }

        // Methods for User Entity

        public async Task<IEnumerable<Algorithm>> ListByUserIdAsync(Guid userId)
        {
            return await _context.Algorithms.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task AssingAlgorithmToUser(Guid userId, Guid algorithmId)
        {
            User user = await _context.Users.FindAsync(userId);
            Algorithm algorithm = await _context.Algorithms.FindAsync(algorithmId);

            if (user != null && algorithm != null)
            {
                algorithm.UserId = userId;
                Update(algorithm);
            }
        }

        public async Task UnassingAlgorithmToUser(Guid userId, Guid algorithmId)
        {
            User user = await _context.Users.FindAsync(userId);
            Algorithm algorithm = await _context.Algorithms.FindAsync(algorithmId);
            var newId = Guid.Empty;

            if (user != null && algorithm != null)
            {
                algorithm.UserId = newId;
                Update(algorithm);
            }
        }

    }
}
