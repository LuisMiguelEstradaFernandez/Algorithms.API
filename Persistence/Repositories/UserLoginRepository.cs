using Microsoft.EntityFrameworkCore;
using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Persistence.Contexts;
using Algorithms.API.Domain.Persistence.Repositories;

namespace Algorithms.API.Persistence.Repositories
{
    public class UserLoginRepository : BaseRepository, IUserLoginRepository
    {
        public UserLoginRepository(AppDbContext context) : base(context)
        {
        }

        // General Methods
        public async Task AddAsync(UserLogin userLogin)
        {
            userLogin.IsActive = true;
            await _context.UserLogins.AddAsync(userLogin);
        }

        public async Task<UserLogin> FindById(Guid id)
        {
            return await _context.UserLogins.FindAsync(id);
        }

        public async Task<IEnumerable<UserLogin>> ListAsync()
        {
            return await _context.UserLogins.ToListAsync();
        }

        public void Remove(UserLogin userLogin)
        {
            userLogin.IsActive = false;
            _context.UserLogins.Remove(userLogin);
        }
        public void Update(UserLogin userLogin)
        {
            _context.UserLogins.Update(userLogin);
        }

        // Methods for User Entity

        public async Task<UserLogin> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserLogins.Where(u => u.UserId == userId).SingleAsync();
        }

        public async Task AssingUserToUserLogin(Guid userId, Guid userLoginId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserLogin userLogin = await _context.UserLogins.FindAsync(userLoginId);

            if (user != null && userLogin != null)
            {
                userLogin.UserId = userId;
                Update(userLogin);
            }
        }

        public async Task UnassingUserToUserLogin(Guid userId, Guid userLoginId)
        {
            User user = await _context.Users.FindAsync(userId);
            UserLogin userLogin = await _context.UserLogins.FindAsync(userLoginId);
            var newId = Guid.Empty;

            if (user != null && userLogin != null)
            {
                userLogin.UserId = newId;
                Update(userLogin);
            }
        }
    }
}
