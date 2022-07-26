using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

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

            CreateMap<GorevAtama, ReportPlanModel>();
            CreateMap<ReportPlanModel, GorevAtama>();

            CreateMap<Birlesim, ReportPlanDetayModel>()
                 .ForMember(dest => dest.GorevTarihi, opt => opt.MapFrom(src => src.BaslangicTarihi.HasValue ? src.BaslangicTarihi.Value.ToShortDateString() : ""))
                 .ForMember(dest => dest.BasSaat, opt => opt.MapFrom(src => src.BaslangicTarihi.HasValue ? src.BaslangicTarihi.Value.ToShortTimeString() : ""))
                 .ForMember(dest => dest.Bitissaat, opt => opt.MapFrom(src => src.BitisTarihi.HasValue ? src.BitisTarihi.Value.ToShortTimeString() : ""))
                 .ForMember(dest => dest.ToplamSure, opt => opt.MapFrom(src => src.BitisTarihi.HasValue && src.BaslangicTarihi.HasValue ? (src.BitisTarihi.Value - src.BaslangicTarihi.Value).TotalMinutes : 0))
                 .ForMember(dest => dest.NetSure, opt => opt.MapFrom(src => 0))
                 .ForMember(dest => dest.Ara, opt => opt.MapFrom(src => 0));
            CreateMap<ReportPlanDetayModel, Birlesim>();

            CreateMap<BirlesimModel, Birlesim>();
            CreateMap<Birlesim, BirlesimModel>();

            CreateMap<OturumModel, Oturum>();
            CreateMap<Oturum, OturumModel>();

            CreateMap<KomisyonModel, Komisyon>();
            CreateMap<Komisyon, KomisyonModel>();

            CreateMap<KomisyonAltModel, Komisyon>();
            CreateMap<Komisyon, KomisyonAltModel>();

            CreateMap<GrupModel, Grup>();
            CreateMap<Grup, GrupModel>()
                      .ForMember(dest => dest.GidenGrupMu, opt => opt.MapFrom(src =>src.GidenGrups.FirstOrDefault().GidenGrupMu == DurumStatu.Hayır));

            CreateMap<GorevAtama, StenoGorevGüncelleModel>();
            CreateMap<StenoGorevGüncelleModel, GorevAtama>();

            CreateMap<StenoGorevAtamaModel, GorevAtama>();
            CreateMap<GorevAtama, StenoGorevAtamaModel>();

            CreateMap<Stenograf, StenoModel>()
                 .ForMember(dest => dest.SonGorevSuresi,
                            opt => opt.MapFrom(src => src.GorevAtamas != null && src.GorevAtamas.LastOrDefault(x => x.StenografId == src.Id) != null ?
                                               src.GorevAtamas.LastOrDefault(x => x.StenografId == src.Id).GorevDakika : 0));
            CreateMap<StenoModel, Stenograf>();

            CreateMap<StenoGrupModel, StenoGrup>();
            CreateMap<StenoGrup, StenoGrupModel>();

            CreateMap<AltKomisyonModel, AltKomisyon>();
            CreateMap<AltKomisyon, AltKomisyonModel>();

            CreateMap<StenoBeklemeSureModel, StenografBeklemeSure>();
            CreateMap<StenografBeklemeSure, StenoBeklemeSureModel>();

            CreateMap<OzelGorevTurModel, OzelGorevTur>();
            CreateMap<OzelGorevTur, OzelGorevTurModel>();

            CreateMap<GidenGrupOlusturModel, GidenGrup>()
                    .ForMember(dest => dest.GidenGrupTarihi, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<GidenGrup, GidenGrupOlusturModel>();
            
            CreateMap<Birlesim, BirlesimViewModel>()
                .ForMember(dest => dest.KomisyonAdı, opt => opt.MapFrom(src => src.Komisyon.Ad))
                .ForMember(dest => dest.AltKomisyonAdı, opt => opt.MapFrom(src => src.AltKomisyon.Ad))
                .ForMember(dest => dest.OturumId, opt => 
                opt.MapFrom(src => src.Oturums.Where(x=>!x.BitisTarihi.HasValue).FirstOrDefault().Id));
            CreateMap<BirlesimViewModel, Birlesim>();

            //CreateMap<OturumModel, Oturum>();
            //CreateMap<Oturum, OturumModel>()
            //     .ForMember(dest => dest.GorevAd, opt => opt.MapFrom(src => src.StenoPlan.GorevAd));

            //CreateMap<GorevAtama, StenoGorevPlanModel>()
            //     .ForMember(dest => dest.GorevYeri, opt => opt.MapFrom(src => src.StenoPlan.GorevYeri))
            //     .ForMember(dest => dest.GorevTuru, opt => opt.MapFrom(src => src.StenoPlan.GorevTuru))
            //     .ForMember(dest => dest.GorevAd, opt => opt.MapFrom(src => src.StenoPlan.GorevAd))
            //     .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad));
            //CreateMap<StenoGorevPlanModel, GorevAtama>();

            CreateMap<GorevAtama, StenoGorevModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo));
                //.ForMember(dest => dest.StenoToplantiVar, opt => 
                //                                       opt.MapFrom(src => src.Birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul &&  src.DifMin<=60 && src.DifMin>0 ? true:
                //                                       (src.Birlesim.ToplanmaTuru == ToplanmaTuru.Komisyon && src.DifMin<=src.Birlesim.StenoSure*9 && src.DifMin > 0) ? true:false));
            CreateMap<StenoGorevModel, GorevAtama>();
            //.ForMember(dest => dest.GorevDakika, opt => opt.MapFrom(src => src.GorevDakika.HasValue ? src.GorevDakika.Value.Hour*60+ src.GorevDakika.Value.Minute:0));



            CreateMap<GorevAtama, StenoModel>()
               .ForMember(dest => dest.AdSoyad, opt => opt.MapFrom(src => src.Stenograf.AdSoyad))
               .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.Stenograf.SiraNo))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Stenograf.Id));
            CreateMap<StenoModel, GorevAtama>();
            //.ForMember(dest => dest.GorevDakika, opt 

            //CreateMap<StenoGrupViewModel, StenoGrup>();
            //CreateMap<StenoGrup, StenoGrupViewModel>()
            //    .ForMember(x => x.StenoViews, c => c.MapFrom(x => new[] { 
            //                                                             new StenoViewModel 
            //                                                             {
            //                                                                 Id = x.Stenograf.Id,
            //                                                                 AdSoyad =x.Stenograf.AdSoyad
            //                                                                 //SonGorevSuresi =x.Stenograf.StenoGorevs.Where(x =>x.GorevBasTarihi.HasValue && x.GorevBasTarihi.Value >=DateTime.Now.AddDays(-7)).Sum(x=>x.GorevDakika)
            //                                                             } }))
            //    .ForMember(x => x.GrupName, c => c.MapFrom(x => x.Grup.Ad));

            CreateMap<Birlesim, HaftalikSureIStatistikModel>()
                .ForMember(dest => dest.Ad, opt => opt.MapFrom(src => src.ToplanmaTuru == ToplanmaTuru.Komisyon ? src.BaslangicTarihi.Value.ToShortDateString() + " " + src.Komisyon.Ad : src.BaslangicTarihi.Value.ToShortDateString() + " " + src.BirlesimNo + " nolu Birleşim"))
                .ForMember(dest => dest.toplanmaTuru, opt => opt.MapFrom(src => src.ToplanmaTuru));

            CreateMap<StenoToplamGenelSure, StenoToplamGenelSureModel>();
            CreateMap<StenoToplamGenelSureModel, StenoToplamGenelSure>();

        }
    }
}
