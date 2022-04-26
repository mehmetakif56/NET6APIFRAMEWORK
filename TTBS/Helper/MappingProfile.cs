using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Models;

namespace TTBS.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DonemModel, Donem>();
            CreateMap<Donem, DonemModel>();
            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
            CreateMap<UserModel, UserEntity>();

            CreateMap<StenoPlan, StenoPlanModel>();
            CreateMap<StenoPlanModel, StenoPlan>();

            CreateMap<StenoIzin, StenoIzinModel>();
            CreateMap<StenoIzinModel, StenoIzin>();

            CreateMap<BirlesimModel, Birlesim>();
            CreateMap<Birlesim, BirlesimModel>();

            CreateMap<KomisyonModel, Komisyon>();
            CreateMap<Komisyon, KomisyonModel>();

            CreateMap<StenoGorev, StenoGorevModel>()
                      .ForMember(dest => dest.GorevSaati, opt => opt.MapFrom(src => src.GorevSaati/60 + src.GorevSaati%60 ));
            CreateMap<StenoGorevModel, StenoGorev>()
                 .ForMember(dest => dest.GorevSaati, opt => opt.MapFrom(src => src.GorevSaati.HasValue ? src.GorevSaati.Value.Hour*60+ src.GorevSaati.Value.Minute:0));
        }
    }
}
