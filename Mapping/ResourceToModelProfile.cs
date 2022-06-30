using AutoMapper;
using Algorithms.API.Domain.Models;
using Algorithms.API.Resources;

namespace Algorithms.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveUserLoginResource, UserLogin>();
            CreateMap<SaveAlgorithmResource, Algorithm>();
        }
    }
}
