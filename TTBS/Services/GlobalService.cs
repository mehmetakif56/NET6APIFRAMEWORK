﻿using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IGlobalService
    {
        IEnumerable<Donem> GetAllDonem();
        IEnumerable<Yasama> GetAllYasama();
        IEnumerable<Birlesim> GetAllBirlesim();
        IEnumerable<Komisyon> GetAllKomisyon();
        IEnumerable<Birlesim> GetBirlesimById(Guid id);
        Komisyon GetKomisyonById(Guid id);
        Donem GetDonemById(Guid id);
        Yasama GetYasamaById(Guid id);
        public IEnumerable<Yasama> GetYasamaByDonemId(Guid id);
        void CreateDonem(Donem donem);
        void CreateYasama(Yasama donem);
        void DeleteBirlesim(Guid id);
        void CreateKomisyon(Komisyon komisyon);
        void CreateGrup(Grup grup);
        void CreateGrupDetay(GrupDetay grup);
        void UpdateGrupDetay(string gidenSaat);
        GrupDetay GetGrupDetay();
        IEnumerable<GrupDetay> GetGrupDetayLast();
        IEnumerable<Grup> GetAllGrup(int grupTuru);
        Grup GetGrupById(Guid id);
        void CreateAltKomisyon(AltKomisyon komisyon);
        void DeleteAltKomisyon(Guid id);
        void UpdateAltKomisyon(AltKomisyon komisyon);
        IEnumerable<Komisyon> GetAllAltKomisyon();
        IEnumerable<AltKomisyon> GetAltKomisyon();
        AltKomisyon GetAltKomisyonById(Guid id);
        void DeleteGroup(Grup grup);
        void CreateStenografBeklemeSure(List<StenografBeklemeSure> stenografBeklemeSure);
        IEnumerable<StenografBeklemeSure> GetAllStenografBeklemeSure();
        void UpdateStenografBeklemeSure(List<StenografBeklemeSure> stenografBeklemeSure);
        void DeleteOzelGorevTur(Guid Id);
        void CreateOzelGorevTur(OzelGorevTur ozelGorev);
        IEnumerable<OzelGorevTur> GetAllOzelGorevTur();
        void UpdateOzelGorevTur(OzelGorevTur ozelGorev);
        OzelGorevTur GetOzelGorevTurById(Guid id);
        void DeleteOturum(Oturum oturum);
        void UpdateOturum(Oturum oturum);
        void UpdateBirlesim(Birlesim birlesim);
        IEnumerable<Oturum> GetOturumByBirlesimId(Guid id);
        Guid InsertStenoToplamSure(StenoToplamGenelSure stenoToplamGenelSure);
        void DeleteStenoToplamSure(Guid id);
        IEnumerable<StenoToplamGenelSure> GetGrupToplamSureByDate(Guid groupId, DateTime? baslangic, DateTime? bitis, Guid? yasamaId);
        public double GetStenoSureWeeklyById(Guid? stenoId);
        public double GetStenoSureYearlyById(Guid? stenoId, Guid? yasamaId);
        public double GetStenoSureDailyById(Guid? stenoId);
        bool GetBirlesimByDate();
    }
    public class GlobalService : BaseService, IGlobalService
    {
        private IRepository<Donem> _donemRepo;
        private IRepository<Yasama> _yasamaRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<Komisyon> _komisyonRepo;
        private IRepository<AltKomisyon> _altkomisyonRepo;
        private IRepository<Grup> _grupRepo;
        private IRepository<GrupDetay> _grupDetayRepo;
        private IRepository<StenografBeklemeSure> _stenoBeklemeSure;
        private IRepository<OzelGorevTur> _ozelGorevTurRepo;
        private IRepository<Oturum> _oturumRepo;
        private IUnitOfWork _unitWork;
        private IRepository<StenoToplamGenelSure> _stenoToplamSureRepo;

        public GlobalService(IRepository<Donem> donemRepo,
                             IRepository<Yasama> yasamaRepo,
                             IRepository<Birlesim> birlesimRepo,
                             IRepository<Komisyon> komisyonRepo,
                             IRepository<Grup> grupRepo,
                             IRepository<AltKomisyon> altkomisyonRepo,
                             IRepository<StenografBeklemeSure> stenoBeklemeSure,
                             IRepository<OzelGorevTur> ozelGorevTurRepo,
                             IRepository<Oturum> oturumRepo,
                             IRepository<GrupDetay> grupDetayRepo,
                             IUnitOfWork unitWork,
                             IRepository<StenoToplamGenelSure> stenoToplamSureRepo,
                             IServiceProvider provider) : base(provider)
        {
            _donemRepo = donemRepo;
            _birlesimRepo = birlesimRepo;
            _komisyonRepo = komisyonRepo;
            _grupRepo = grupRepo;
            _unitWork = unitWork;
            _yasamaRepo = yasamaRepo;
            _stenoBeklemeSure = stenoBeklemeSure;
            _altkomisyonRepo = altkomisyonRepo;
            _ozelGorevTurRepo = ozelGorevTurRepo;
            _oturumRepo = oturumRepo;
            _stenoToplamSureRepo = stenoToplamSureRepo;
            _grupDetayRepo = grupDetayRepo;
        }
        public IEnumerable<Donem> GetAllDonem()
        {
            return _donemRepo.GetAll();
        }

        public IEnumerable<Birlesim> GetAllBirlesim()
        {
            return _birlesimRepo.GetAll(includeProperties: "Yasama");
        }

        public IEnumerable<Komisyon> GetAllKomisyon()
        {
            return _komisyonRepo.GetAll();
        }

        public IEnumerable<Birlesim> GetBirlesimById(Guid id)
        {
            return _birlesimRepo.Get(x=>x.Id ==id, includeProperties: "Yasama");
        }

        public Komisyon GetKomisyonById(Guid id)
        {
            return _komisyonRepo.GetById(id);
        }

        public Donem GetDonemById(Guid id)
        {
            var result = _donemRepo.GetById(id);
            return result;
        }

        public void CreateDonem(Donem donem)
        {
            _donemRepo.Create(donem, CurrentUser.Id);
            _donemRepo.Save();
        }       
        public void CreateKomisyon(Komisyon komisyon)
        {
            _komisyonRepo.Create(komisyon, CurrentUser.Id);
            _komisyonRepo.Save();
        }
        public void CreateAltKomisyon(AltKomisyon komisyon)
        {
            _altkomisyonRepo.Create(komisyon, CurrentUser.Id);
            _altkomisyonRepo.Save();
        }

        public IEnumerable<Komisyon> GetAllAltKomisyon()
        {
            return _komisyonRepo.Get( includeProperties: "AltKomisyons");
        }

        public IEnumerable<AltKomisyon> GetAltKomisyon()
        {
            return _altkomisyonRepo.Get();
        }

        public IEnumerable<Yasama> GetAllYasama()
        {
            return _yasamaRepo.GetAll();
        }

        public Yasama GetYasamaById(Guid id)
        {
            return _yasamaRepo.GetById(id);
        }

        public IEnumerable<Yasama> GetYasamaByDonemId(Guid id)
        {
            return _yasamaRepo.Get(x => x.DonemId == id).OrderBy(x => x.BaslangicTarihi);
        }

        public void CreateYasama(Yasama yasama)
        {
            _yasamaRepo.Create(yasama, CurrentUser.Id);
            _yasamaRepo.Save();
        }

        public void CreateGrup(Grup grup)
        {
            _grupRepo.Create(grup, CurrentUser.Id);
            _grupRepo.Save();
        }


        public IEnumerable<Grup> GetAllGrup(int grupTuru)
        {
            return _grupRepo.Get(x=>(int)x.StenoGrupTuru == grupTuru);
        }

        public Grup GetGrupById(Guid id)
        {
            return _grupRepo.GetById(id);
        }

        public void DeleteGroup(Grup grup)
        {
            _grupRepo.Delete(grup);
            _grupRepo.Save();
        }

        public void DeleteAltKomisyon(Guid id)
        {
            _altkomisyonRepo.Delete(id);
            _altkomisyonRepo.Save();
        }

        public void UpdateAltKomisyon(AltKomisyon komisyon)
        {
            _altkomisyonRepo.Update(komisyon);
            _altkomisyonRepo.Save();
        }

        public AltKomisyon GetAltKomisyonById(Guid id)
        {
            return _altkomisyonRepo.GetById(id);
        }

        public void CreateStenografBeklemeSure(List<StenografBeklemeSure> stenografBeklemeSure)
        {
            _stenoBeklemeSure.Create(stenografBeklemeSure);
            _stenoBeklemeSure.Save();
        }

        public void UpdateStenografBeklemeSure(List<StenografBeklemeSure> stenografBeklemeSure)
        {
            _stenoBeklemeSure.Update(stenografBeklemeSure);
            _stenoBeklemeSure.Save();
        }

        public IEnumerable<StenografBeklemeSure> GetAllStenografBeklemeSure()
        {
            return _stenoBeklemeSure.GetAll();

        }

        public void DeleteOzelGorevTur(Guid Id)
        {
            _ozelGorevTurRepo.Delete(Id);
            _ozelGorevTurRepo.Save();
        }

        public void CreateOzelGorevTur(OzelGorevTur ozelGorevTur)
        {
            _ozelGorevTurRepo.Create(ozelGorevTur, CurrentUser.Id);
            _ozelGorevTurRepo.Save();
        }

        public IEnumerable<OzelGorevTur> GetAllOzelGorevTur()
        {
            return _ozelGorevTurRepo.Get();
        }

        public void UpdateOzelGorevTur(OzelGorevTur ozelGorevTur)
        {
            _ozelGorevTurRepo.Update(ozelGorevTur, CurrentUser.Id);
            _ozelGorevTurRepo.Save();
        }

        public OzelGorevTur GetOzelGorevTurById(Guid id)
        {
            return _ozelGorevTurRepo.GetById(id);
        }

      

        public void UpdateOturum(Oturum oturum)
        {
            _oturumRepo.Update(oturum, CurrentUser.Id);
            _oturumRepo.Save();
        }

        public IEnumerable<Oturum> GetOturumByBirlesimId(Guid id)
        {
            return _oturumRepo.Get(x => x.BirlesimId == id,includeProperties: "Birlesim");
        }

        public void DeleteOturum(Oturum oturum)
        {
            _oturumRepo.Delete(oturum);
            _oturumRepo.Save();
        }

        public void UpdateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Update(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();
        }

        public bool GetBirlesimByDate()
        {
            var result = _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.GenelKurul && (x.ToplanmaDurumu == Core.Enums.ToplanmaStatu.DevamEdiyor || x.ToplanmaDurumu == Core.Enums.ToplanmaStatu.Planlandı || x.ToplanmaDurumu == Core.Enums.ToplanmaStatu.Oluşturuldu));
            return result!=null && result.Count()>0 ? true : false;                
        }

        public void DeleteBirlesim(Guid id)
        {
            var result = _birlesimRepo.Get(x => x.Id == id && (x.ToplanmaDurumu == ToplanmaStatu.Oluşturuldu || x.ToplanmaDurumu == ToplanmaStatu.Planlandı));
            if(result !=null)
            {
                _birlesimRepo.Delete(result);
                _birlesimRepo.Save();
            }
        }

        public Guid InsertStenoToplamSure(StenoToplamGenelSure stenoToplamGenelSure)
        {
            _stenoToplamSureRepo.Create(stenoToplamGenelSure, CurrentUser.Id);
            _stenoToplamSureRepo.Save();

            return stenoToplamGenelSure.Id;
        }

        public void DeleteStenoToplamSure(Guid id)
        {
            var result = _stenoToplamSureRepo.Get(x => x.Id == id);
            if(result != null)
            {
                _stenoToplamSureRepo.Delete(result);
                _stenoToplamSureRepo.Save();
            }
        }

        public IEnumerable<StenoToplamGenelSure> GetGrupToplamSureByDate(Guid groupId, DateTime? baslangic, DateTime? bitis, Guid? yasamaId)
        {
            if(yasamaId == null)
            {
                return _stenoToplamSureRepo.Get(x => x.GroupId == groupId && x.Tarih >= baslangic && x.Tarih <= bitis, includeProperties:"Stenograf");
            }
            return _stenoToplamSureRepo.Get(x => x.GroupId == groupId && x.YasamaId == yasamaId, includeProperties: "Stenograf");
        }

        public double GetStenoSureDailyById(Guid? stenoId)
        {
            DateTime now = DateTime.Now.Date;
            var result = _stenoToplamSureRepo.Get(x => x.StenografId == stenoId && x.Tarih < now && x.Tarih >= now.AddDays(-1)).Select(x => x.Sure).Sum();
            return result;
            //return 0;
        }

        public double GetStenoSureWeeklyById(Guid? stenoId)
        {
            DateTime now = DateTime.Now.Date;
            var result = _stenoToplamSureRepo.Get(x => x.StenografId == stenoId && x.Tarih < now.AddDays(1) && x.Tarih >= now.AddDays(-7)).Select(x => x.Sure).Sum();
            return result;
            //return 0;
        }

        public double GetStenoSureYearlyById(Guid? stenoId, Guid? yasamaId)
        {
            var result = _stenoToplamSureRepo.Get(x => x.StenografId == stenoId && x.YasamaId == yasamaId).Select(x => x.Sure).Sum();
            return result;
        }

        public void CreateGrupDetay(GrupDetay detay)
        {
            var grpDetay = _grupDetayRepo.GetFirst();
            if(grpDetay != null)
            {
                grpDetay.IsDeleted = true;
                 _grupDetayRepo.Update(grpDetay);
                _grupDetayRepo.Save();
            }
            _grupDetayRepo.Create(detay);
            _grupDetayRepo.Save();
        }

        public void UpdateGrupDetay(string gidenSaat)
        {
            var grpDetay = _grupDetayRepo.GetFirst();
            if (grpDetay != null)
            {
                grpDetay.GidenGrupSaat = gidenSaat;
                _grupDetayRepo.Update(grpDetay);
                _grupDetayRepo.Save();
            }
        }
        public IEnumerable<GrupDetay> GetGrupDetayLast()
        {
            return _grupDetayRepo.Get(x => x.GidenGrupPasif == DurumStatu.Hayır, includeProperties: "Grup").OrderByDescending(x=>x.GidenGrupTarih);
                                
        }
        public GrupDetay GetGrupDetay()
        {
            return _grupDetayRepo.GetFirst( includeProperties: "Grup");
        }
    }
}
