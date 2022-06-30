using Algorithms.API.Domain.Models;

namespace Algorithms.API.Domain.Services.Communications
{
    public class UserLoginResponse : BaseResponse<UserLogin>
    {
        public UserLoginResponse(UserLogin resource) : base(resource)
        {
        }

        public UserLoginResponse(string message) : base(message)
        {
        }
    }
}
