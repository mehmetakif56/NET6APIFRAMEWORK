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
            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name)));
            CreateMap<UserModel, UserEntity>();

            CreateMap<StenoPlan, StenoPlanModel>()
                 .ForMember(dest => dest.GorevList , opt => opt.MapFrom(src => new SelectListItem { Text = src.GorevTuru.Ad, Value = src.GorevTuruId.ToString() }))
                 .ForMember(dest => dest.BirlesimList, opt => opt.MapFrom(src => new SelectListItem { Text = src.Birlesim.BirlesimNo, Value = src.BirlesimId.ToString() }))
                 .ForMember(dest => dest.KomisyonList, opt => opt.MapFrom(src => new SelectListItem { Text = src.Komisyon.Ad, Value = src.KomisyonId.ToString() }));
            CreateMap<StenoPlanModel, StenoPlan>()
                .ForMember(dest => dest.GorevTuruId, opt => opt.MapFrom(src => new Guid(src.GorevList.Value)))
               .ForMember(dest => dest.BirlesimId, opt => opt.MapFrom(src => new Guid(src.BirlesimList.Value)))
              .ForMember(dest => dest.KomisyonId, opt => opt.MapFrom(src => new Guid(src.KomisyonList.Value)));

            CreateMap<StenoIzin, StenoIzinModel>();
            CreateMap<StenoIzinModel, StenoIzin>();

            CreateMap<BirlesimModel, Birlesim>();
            CreateMap<Birlesim, BirlesimModel>();

            CreateMap<KomisyonModel, Komisyon>();
            CreateMap<Komisyon, KomisyonModel>();

            CreateMap<GorevTuruModel, GorevTuru>();
            CreateMap<GorevTuru, GorevTuruModel>();

            CreateMap<StenoGorev, StenoGorevModel>()
                      .ForMember(dest => dest.GorevSaati, opt => opt.MapFrom(src => src.GorevSaati/60 + src.GorevSaati%60 ));
            CreateMap<StenoGorevModel, StenoGorev>()
                 .ForMember(dest => dest.GorevSaati, opt => opt.MapFrom(src => src.GorevSaati.HasValue ? src.GorevSaati.Value.Hour*60+ src.GorevSaati.Value.Minute:0));
        }
    }
}
