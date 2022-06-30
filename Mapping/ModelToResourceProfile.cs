using AutoMapper;
using Algorithms.API.Domain.Models;
using Algorithms.API.Resources;

namespace Algorithms.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<UserLogin, UserLoginResource>();
            CreateMap<Algorithm, AlgorithmResource>();
        }
    }
}
