using Microsoft.EntityFrameworkCore;
using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Persistence.Contexts;
using Algorithms.API.Domain.Persistence.Repositories;

namespace Algorithms.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods
        public async Task AddAsync(User user)
        {
            user.IsActive = true;
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            user.IsActive = false;
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
