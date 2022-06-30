using Algorithms.API.Domain.Models;
using Algorithms.API.Domain.Persistence.Repositories;
using Algorithms.API.Domain.Services;
using Algorithms.API.Domain.Services.Communications;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Algorithms.API.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserLoginService(IUserLoginRepository userLoginRepository, IUnitOfWork unitOfWork)
        {
            _userLoginRepository = userLoginRepository;
            _unitOfWork = unitOfWork;
        }

        // General Methods

        public async Task<IEnumerable<UserLogin>> ListAsync()
        {
            var all = await _userLoginRepository.ListAsync();
            return all.Where(x => x.IsActive);
        }

        public async Task<UserLoginResponse> DeleteAsync(Guid id)
        {
            var existingUserLogin = await _userLoginRepository.FindById(id);

            if (existingUserLogin == null)
                return new UserLoginResponse("UserLogin Not Found");

            try
            {
                existingUserLogin.IsActive = false;
                _userLoginRepository.Update(existingUserLogin);
                await _unitOfWork.CompleteAsync();

                return new UserLoginResponse(existingUserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurred while deleting UserLogin: {ex.Message}");
            }

        }

        public async Task<UserLoginResponse> GetByIdAsync(Guid id)
        {
            var existingUserLogin = await _userLoginRepository.FindById(id);

            if (existingUserLogin == null || !existingUserLogin.IsActive)
                return new UserLoginResponse("UserLogin Not Found");

            return new UserLoginResponse(existingUserLogin);
        }

        public async Task<UserLoginResponse> SaveAsync(UserLogin userLogin)
        {
            try
            {
                userLogin.Password = Hashpassword(userLogin.Password);
                await _userLoginRepository.AddAsync(userLogin);
                await _unitOfWork.CompleteAsync();

                return new UserLoginResponse(userLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurred while saving the UserLogin: {ex.Message}");
            }
        }

        private string Hashpassword(string password)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values

            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public async Task<UserLoginResponse> UpdateAsync(Guid id, UserLogin userLogin)
        {
            var existingUserLogin = await _userLoginRepository.FindById(id);

            if (existingUserLogin == null)
                return new UserLoginResponse("UserLogin Not Found");

            existingUserLogin.Username = userLogin.Username;
            existingUserLogin.Password = userLogin.Password;
            existingUserLogin.IsActive = userLogin.IsActive;
            existingUserLogin.CreatedOn = userLogin.CreatedOn;
            existingUserLogin.ModifiedOn = userLogin.ModifiedOn;

            try
            {
                _userLoginRepository.Update(existingUserLogin);
                await _unitOfWork.CompleteAsync();

                return new UserLoginResponse(existingUserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurred while updating UserLogin: {ex.Message}");
            }
        }

        // Methods for UserLoginType Entity
        public async Task<UserLogin> GetByUserId(Guid userId)
        {
            return await _userLoginRepository.GetByUserIdAsync(userId);
        }

        public async Task<UserLoginResponse> AssingUserToUserLogin(Guid userId, Guid userLoginId)
        {
            try
            {
                await _userLoginRepository.AssingUserToUserLogin(userId, userLoginId);
                await _unitOfWork.CompleteAsync();
                UserLogin UserLogin = await _userLoginRepository.FindById(userLoginId);
                return new UserLoginResponse(UserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurrend while assigning User to UserLogin: {ex.Message}");
            }
        }

        public async Task<UserLoginResponse> UnassingUserToUserLogin(Guid userId, Guid userLoginId)
        {
            try
            {
                await _userLoginRepository.UnassingUserToUserLogin(userId, userLoginId);
                await _unitOfWork.CompleteAsync();
                UserLogin UserLogin = await _userLoginRepository.FindById(userLoginId);
                return new UserLoginResponse(UserLogin);
            }
            catch (Exception ex)
            {
                return new UserLoginResponse($"An error ocurrend while unassigning User to UserLogin: {ex.Message}");
            }
        }
    }
}
