using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TTBS.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DonemModel, Donem>();
            CreateMap<Donem, DonemModel>();
            CreateMap<YasamaModel, Yasama>();
            CreateMap<Yasama, YasamaModel>();
            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
            CreateMap<UserModel, UserEntity>();

            CreateMap<StenoPlan, StenoPlanModel>() 
                 .ForMember(dest => dest.BirlesimList, opt => opt.MapFrom(src => new SelectListItem { Text = src.Birlesim.BirlesimNo, Value = src.BirlesimId.ToString() }))
                 .ForMember(dest => dest.KomisyonList, opt => opt.MapFrom(src => new SelectListItem { Text = src.Komisyon.Ad, Value = src.KomisyonId.ToString() }));
            CreateMap<StenoPlanModel, StenoPlan>()
               .ForMember(dest => dest.BirlesimId, opt => opt.MapFrom(src => new Guid(src.BirlesimList.Value)))
              .ForMember(dest => dest.KomisyonId, opt => opt.MapFrom(src => new Guid(src.KomisyonList.Value)));

            CreateMap<StenoIzin, StenoIzinModel>();
            CreateMap<StenoIzinModel, StenoIzin>();

            CreateMap<BirlesimModel, Birlesim>();
            CreateMap<Birlesim, BirlesimModel>();

            CreateMap<KomisyonModel, Komisyon>();
            CreateMap<Komisyon, KomisyonModel>();

            CreateMap <StenoGorevAtamaModel, StenoGorev>();
            CreateMap<StenoGorev, StenoGorevAtamaModel>();

            CreateMap<StenoGorev, StenoGorevModel>()
                      .ForMember(dest => dest.GorevDakika, opt => opt.MapFrom(src => src.GorevDakika/60 + src.GorevDakika % 60 ));
            CreateMap<StenoGorevModel, StenoGorev>()
                 .ForMember(dest => dest.GorevDakika, opt => opt.MapFrom(src => src.GorevDakika.HasValue ? src.GorevDakika.Value.Hour*60+ src.GorevDakika.Value.Minute:0));
        }
    }
}
