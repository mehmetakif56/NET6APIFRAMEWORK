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


            CreateMap<StenoIzinModel, StenoIzin>();
            CreateMap<StenoIzin, StenoIzinModel>()
                .ForMember(dest => dest.StenografAdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));

            CreateMap<StenoPlan, StenoPlanOlusturModel>();
            CreateMap<StenoPlanOlusturModel, StenoPlan>();
            CreateMap<StenoPlan, StenoPlanGüncelleModel>();
            CreateMap<StenoPlanGüncelleModel, StenoPlan>();

            CreateMap<StenoGorev, ReportPlanModel>();                
            CreateMap<ReportPlanModel, StenoGorev>();

            CreateMap<StenoGorev, ReportPlanDetayModel>()
                 .ForMember(dest => dest.GorevTarihi, opt => opt.MapFrom(src => src.StenoPlan.PlanlananBaslangicTarihi.HasValue ? src.StenoPlan.PlanlananBaslangicTarihi.Value.ToShortDateString() : ""))
                 .ForMember(dest => dest.BasSaat, opt => opt.MapFrom(src => src.StenoPlan.PlanlananBaslangicTarihi.HasValue ? src.StenoPlan.PlanlananBaslangicTarihi.Value.ToShortTimeString() : ""))
                 .ForMember(dest => dest.Bitissaat, opt => opt.MapFrom(src => src.StenoPlan.PlanlananBitisTarihi.HasValue ? src.StenoPlan.PlanlananBitisTarihi.Value.ToShortTimeString() : ""))
                 .ForMember(dest => dest.ToplamSure, opt => opt.MapFrom(src => src.StenoPlan.PlanlananBitisTarihi.HasValue && src.StenoPlan.PlanlananBaslangicTarihi.HasValue ? (src.StenoPlan.PlanlananBitisTarihi.Value - src.StenoPlan.PlanlananBaslangicTarihi.Value).TotalMinutes : 0))
                 .ForMember(dest => dest.NetSure, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.Ara, opt => opt.MapFrom(src => 0));
            CreateMap<ReportPlanDetayModel, StenoGorev>();

            CreateMap<BirlesimModel, Birlesim>();
            CreateMap<Birlesim, BirlesimModel>();

            CreateMap<KomisyonModel, Komisyon>();
            CreateMap<Komisyon, KomisyonModel>();

            CreateMap<KomisyonAltModel, Komisyon>();
            CreateMap<Komisyon, KomisyonAltModel>();

            CreateMap<GrupModel, Grup>();
            CreateMap<Grup, GrupModel>();
          
            CreateMap<StenoGorev, StenoGorevGüncelleModel>();
            CreateMap<StenoGorevGüncelleModel, StenoGorev>();

            CreateMap<StenoGorevAtamaModel, StenoGorev>();
            CreateMap<StenoGorev, StenoGorevAtamaModel>();

            CreateMap<Stenograf, StenoModel>();
            CreateMap<StenoModel, Stenograf>();

            CreateMap<StenoGrupModel, StenoGrup>();
            CreateMap <StenoGrup, StenoGrupModel>();

            CreateMap<AltKomisyonModel, AltKomisyon>();
            CreateMap<AltKomisyon, AltKomisyonModel>();

            CreateMap<StenoBeklemeSureModel, StenografBeklemeSure>();
            CreateMap<StenografBeklemeSure, StenoBeklemeSureModel>();


            CreateMap<OzelGorevTurModel, OzelGorevTur>();
            CreateMap<OzelGorevTur, OzelGorevTurModel>();


            CreateMap<StenoGorev, StenoGorevPlanModel>()
                 .ForMember(dest => dest.GorevYeri, opt => opt.MapFrom(src => src.StenoPlan.GorevYeri))
                 .ForMember(dest => dest.GorevAd, opt => opt.MapFrom(src => src.StenoPlan.GorevAd))
                 .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));
            CreateMap<StenoGorevPlanModel, StenoGorev>();

            CreateMap<StenoGorev, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
            CreateMap<StenoGorevModel, StenoGorev>();
                 //.ForMember(dest => dest.GorevDakika, opt => opt.MapFrom(src => src.GorevDakika.HasValue ? src.GorevDakika.Value.Hour*60+ src.GorevDakika.Value.Minute:0));
        }
    }
}
