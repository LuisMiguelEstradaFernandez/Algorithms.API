using Algorithms.API.Domain.Models;

namespace Algorithms.API.Domain.Services.Communications
{
    public class AlgorithmResponse : BaseResponse<Algorithm>
    {
        public AlgorithmResponse(Algorithm resource) : base(resource)
        {
        }

        public AlgorithmResponse(string message) : base(message)
        {
        }
    }
}
