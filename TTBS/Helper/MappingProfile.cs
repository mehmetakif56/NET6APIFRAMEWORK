using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Models;

namespace TTBS.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DonemModel, DonemEntity>();
            CreateMap<DonemEntity, DonemModel>();
        }
    }
}
