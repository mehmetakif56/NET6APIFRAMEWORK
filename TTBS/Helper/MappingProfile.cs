using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;
using TTBS.MongoDB;

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

            CreateMap<StenoIzinModel, StenoIzin>();
            CreateMap<StenoIzin, StenoIzinModel>()
                .ForMember(dest => dest.StenografAdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));

         
            CreateMap<IEnumerable<GorevAtamaGenelKurul>, ReportPlanModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.First().Stenograf.AdSoyad))
                .ForMember(dest => dest.GorevlendirmeSure, opt => opt.MapFrom(src => src.Sum(x => x.GorevBitisTarihi.Value.Subtract(x.GorevBasTarihi.Value).Minutes)))
                .ForMember(dest => dest.ToplanmaTuru, opt => opt.MapFrom(src => src.First().Stenograf.StenoGorevTuru))
                .ForMember(dest => dest.GorevlendirmeSay, opt => opt.MapFrom(src => src.Count()));
            CreateMap<GorevAtamaGenelKurul, ReportPlanModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));
            CreateMap<ReportPlanModel, GorevAtamaGenelKurul>();

            CreateMap<IEnumerable<GorevAtamaKomisyon>, ReportPlanModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.First().Stenograf.AdSoyad))
                .ForMember(dest => dest.GorevlendirmeSure, opt => opt.MapFrom(src => src.Sum(x => x.GorevBitisTarihi.Value.Subtract(x.GorevBasTarihi.Value).Minutes)))
                .ForMember(dest => dest.ToplanmaTuru, opt => opt.MapFrom(src => src.First().Stenograf.StenoGorevTuru))
                .ForMember(dest => dest.GorevlendirmeSay, opt => opt.MapFrom(src => src.Count()));
            CreateMap<GorevAtamaKomisyon, ReportPlanModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));
            CreateMap<ReportPlanModel, GorevAtamaKomisyon>();

            CreateMap<IEnumerable<GorevAtamaOzelToplanma>, ReportPlanModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.First().Stenograf.AdSoyad))
                .ForMember(dest => dest.GorevlendirmeSure, opt => opt.MapFrom(src => src.Sum(x => x.GorevBitisTarihi.Value.Subtract(x.GorevBasTarihi.Value).Minutes)))
                .ForMember(dest => dest.ToplanmaTuru, opt => opt.MapFrom(src => src.First().Stenograf.StenoGorevTuru))
                .ForMember(dest => dest.GorevlendirmeSay, opt => opt.MapFrom(src => src.Count()));
            CreateMap<GorevAtamaOzelToplanma, ReportPlanModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));
            CreateMap<ReportPlanModel, GorevAtamaOzelToplanma>();

            CreateMap<Birlesim, ReportPlanDetayModel>()
                 .ForMember(dest => dest.GorevTarihi, opt => opt.MapFrom(src => src.BaslangicTarihi.HasValue ? src.BaslangicTarihi.Value.ToShortDateString() : ""))
                 .ForMember(dest => dest.BasSaat, opt => opt.MapFrom(src => src.BaslangicTarihi.HasValue ? src.BaslangicTarihi.Value.ToShortTimeString() : ""))
                 .ForMember(dest => dest.Bitissaat, opt => opt.MapFrom(src => src.BitisTarihi.HasValue ? src.BitisTarihi.Value.ToShortTimeString() : ""))
                 .ForMember(dest => dest.ToplamSure, opt => opt.MapFrom(src => src.BitisTarihi.HasValue && src.BaslangicTarihi.HasValue ? (src.BitisTarihi.Value - src.BaslangicTarihi.Value).TotalMinutes : 0))
                 .ForMember(dest => dest.NetSure, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.GorevAd, opt => opt.MapFrom(src => src.ToplanmaTuru == ToplanmaTuru.Komisyon ? src.Komisyon.Ad + " (" + src.BaslangicTarihi.Value.ToShortDateString() + ")" : (src.ToplanmaTuru == ToplanmaTuru.OzelToplanti ? src.OzelToplanma.Ad + " (" + src.BaslangicTarihi.Value.ToShortDateString() + ")" : src.BirlesimNo + ". Birleşim (" + src.BaslangicTarihi.Value.ToShortDateString() + ")")))
                 .ForMember(dest => dest.Ara, opt => opt.MapFrom(src => 0));
            CreateMap<ReportPlanDetayModel, Birlesim>();

            CreateMap<BirlesimModel, Birlesim>();
            CreateMap<Birlesim, BirlesimModel>();

            CreateMap<GorevAtamaModel, GorevAtamaGenelKurul>();
            CreateMap<GorevAtamaGenelKurul, GorevAtamaModel>()
                             .ForMember(dest => dest.StenoAdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
                             .ForMember(dest => dest.StenoGorevTuru, opt => opt.MapFrom(src => src.Stenograf.StenoGorevTuru))
                             .ForMember(dest => dest.BirlesimKapatanMı, opt => opt.MapFrom(src => src.Birlesim.ToplanmaDurumu == ToplanmaStatu.Tamamlandı ||  src.Birlesim.ToplanmaDurumu == ToplanmaStatu.DevamEdiyor || src.GorevStatu != GorevStatu.Iptal? true : src.Stenograf.BirlesimKapatanMi))
                             .ForMember(dest => dest.StenoSiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));

            CreateMap<GorevAtamaModel, GorevAtamaKomisyon>();
            CreateMap<GorevAtamaKomisyon, GorevAtamaModel>()
                    .ForMember(dest => dest.StenoAdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
                    .ForMember(dest => dest.StenoGorevTuru, opt => opt.MapFrom(src => src.Stenograf.StenoGorevTuru))
                    .ForMember(dest => dest.BirlesimKapatanMı, opt => opt.MapFrom(src => src.Stenograf.BirlesimKapatanMi))
                      .ForMember(dest => dest.StenoSiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));


            CreateMap<GorevAtamaModel, GorevAtamaKomisyonOnay>();
            CreateMap<GorevAtamaKomisyonOnay, GorevAtamaModel>()
                    .ForMember(dest => dest.StenoAdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
                    .ForMember(dest => dest.StenoGorevTuru, opt => opt.MapFrom(src => src.Stenograf.StenoGorevTuru))
                    .ForMember(dest => dest.BirlesimKapatanMı, opt => opt.MapFrom(src => src.Stenograf.BirlesimKapatanMi))
                      .ForMember(dest => dest.StenoSiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));

            CreateMap<OturumModel, Oturum>();
            CreateMap<Oturum, OturumModel>();
            CreateMap<GrupModel, Grup>();
            CreateMap<Grup, GrupModel>();
            CreateMap<GrupGuncelleModel, GrupDetay>();
            CreateMap<GrupDetay, GrupGuncelleModel>();    

            CreateMap<GrupDetayModel, GrupDetay>();
            CreateMap<GrupDetay, GrupDetayModel>()
                            .ForMember(dest => dest.GrupAd, opt => opt.MapFrom(src => src.Grup.Ad));

            CreateMap<KomisyonModel, Komisyon>();
            CreateMap<Komisyon, KomisyonModel>();

            CreateMap<KomisyonAltModel, Komisyon>();
            CreateMap<Komisyon, KomisyonAltModel>();

            CreateMap<Stenograf, StenoModel>()
                 .ForMember(dest => dest.HaftalikGorevSuresi,
                            opt => opt.MapFrom(src => 0));
            CreateMap<StenoModel, Stenograf>();

            CreateMap<Stenograf, StenoCreateModel>();          
            CreateMap<StenoCreateModel, Stenograf>();

            CreateMap<AltKomisyonModel, AltKomisyon>();
            CreateMap<AltKomisyon, AltKomisyonModel>();

            CreateMap<StenoBeklemeSureModel, StenografBeklemeSure>();
            CreateMap<StenografBeklemeSure, StenoBeklemeSureModel>();

            CreateMap<OzelGorevTurModel, OzelGorevTur>();
            CreateMap<OzelGorevTur, OzelGorevTurModel>();

                   
            CreateMap<Birlesim, BirlesimViewModel>()
                .ForMember(dest => dest.KomisyonAdı, opt => opt.MapFrom(src => src.Komisyon.Ad))
                .ForMember(dest => dest.AltKomisyonAdı, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.OturumId, opt => 
                opt.MapFrom(src => src.Oturums.Where(x=>!x.BitisTarihi.HasValue).FirstOrDefault().Id))
                .ForMember(dest => dest.YasamaId, opt => opt.MapFrom(src => src.YasamaId));
            CreateMap<BirlesimViewModel, Birlesim>();

            CreateMap<GorevAtamaOzelToplanma, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
          
            CreateMap<GorevAtamaOzelToplanma, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
         
            CreateMap<GorevAtamaOzelToplanma, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
           
            CreateMap<StenoGorevModel, GorevAtamaOzelToplanma>();
          
            CreateMap<GorevAtamaGenelKurul, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
            CreateMap<StenoGorevModel, GorevAtamaGenelKurul>();
          
            CreateMap<GorevAtamaKomisyon, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
         
            CreateMap<StenoGorevModel, GorevAtamaKomisyon>();

            CreateMap<GorevAtamaKomisyon, StenoModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Stenograf.Id));
            CreateMap<StenoModel, GorevAtamaKomisyon>();

            CreateMap<GorevAtamaGenelKurul, StenoModel>()
        .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
        .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo))
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Stenograf.Id));
            CreateMap<StenoModel, GorevAtamaGenelKurul>();

            CreateMap<Birlesim, HaftalikSureIStatistikModel>()
                .ForMember(dest => dest.Ad, opt => opt.MapFrom(src => src.ToplanmaTuru == ToplanmaTuru.Komisyon ? src.BaslangicTarihi.Value.ToShortDateString() + " " + src.Komisyon.Ad : src.BaslangicTarihi.Value.ToShortDateString() + " " + src.BirlesimNo + " nolu Birleşim"))
                .ForMember(dest => dest.toplanmaTuru, opt => opt.MapFrom(src => src.ToplanmaTuru));

            CreateMap<StenoToplamGenelSure, StenoToplamGenelSureModel>()
                .ForMember(dest => dest.StenografAd, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));
            CreateMap<StenoToplamGenelSureModel, StenoToplamGenelSure>();

        }
    }
}
