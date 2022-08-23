using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoIzin> GetAllStenoIzin();
        IEnumerable<StenoIzin> GetStenoIzinByStenografId(Guid id);
        IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad);
        IEnumerable<StenoIzin> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi, string? field, string? sortOrder, int? izinTur, Guid? stenografId, int pageIndex, int pagesize);
        IEnumerable<GorevAtama> GetStenoGorevById(Guid id);
        IEnumerable<GorevAtama> GetStenoGorevByName(string adSoyad);
        IEnumerable<GorevAtama> GetStenoGorevByDateAndStatus(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int status);
        IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
        void UpdateStenoGorev(List<GorevAtama> stenoGorev);
        void CreateStenoIzin(StenoIzin stenoGorev);
        IEnumerable<GorevAtama> GetStenoGorevByGorevTuru(int gorevTuru);
        IEnumerable<GorevAtama> GetStenoGorevByBirlesimIdAndGorevTuru(Guid birlesimId, int gorevTuru);
        //List<StenoPlan> GetStenoPlanByStatus(int status);
        IEnumerable<Birlesim> GetBirlesimByDateAndTur(DateTime gorevTarihi, DateTime gorevBitTarihi, int gorevTuru);
        List<GorevAtama> GetStenoGorevBySatatus(int status);
        IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId);
        void CreateStenograf(Stenograf stenograf);
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int? gorevTuru);
        void DeleteStenoGorev(Guid stenoGorevId);
        IEnumerable<GorevAtama> GetStenoGorevByGrupId(Guid id);
        IEnumerable<Stenograf> GetAllStenoGrupNotInclueded();
        //List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int stenografId, int toplantiTur);
        //void UpdateStenoPlan(StenoPlan plan);
        //IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur);
        IEnumerable<GorevAtama> GetAssignedStenoByBirlesimId(Guid birlesimId);
        //IEnumerable<GorevAtama> GetIntersectStenoPlan(Guid stenoPlanId, Guid stenoId);
        void UpdateStenoSiraNo(List<Stenograf> steno);
        IEnumerable<Grup> GetAllStenografGroup(int gorevTuru);      
        IEnumerable<Birlesim> GetBirlesimByDate(DateTime basTarihi, int toplanmaTuru);
        IEnumerable<Birlesim> GetKomisyonByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup);
        IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup);
        void CreateStenoGroup(Guid stenoId,Guid grupId);

    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenografBeklemeSure> _stenoBeklemeSure;
        private IRepository<Grup> _grupRepo;
        private IRepository<Oturum> _oturumRepo;
        private readonly IGorevAtamaGKMBusiness _gorevAtamaGKMRepo;
        private readonly IGorevAtamaKomMBusiness _gorevAtamaKomMRepo;
        public StenografService(IRepository<StenoIzin> stenoIzinRepo, IRepository<GorevAtama> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenografRepo,
                                IRepository<StenografBeklemeSure> stenoBeklemeSure,
                                IRepository<Grup> grupRepo,
                                IRepository<Birlesim> birlesimRepo,
                                IRepository<Oturum> oturumRepo,
                                IGorevAtamaGKMBusiness gorevAtamaGKMRepo,
                                IGorevAtamaKomMBusiness gorevAtamaKomMRepo,
                                IServiceProvider provider) : base(provider)
        {
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
            _stenografRepo = stenografRepo;
            _stenoBeklemeSure = stenoBeklemeSure;
            _birlesimRepo = birlesimRepo;
            _oturumRepo = oturumRepo;
            _grupRepo = grupRepo;
            _gorevAtamaGKMRepo = gorevAtamaGKMRepo;
            _gorevAtamaKomMRepo = gorevAtamaKomMRepo;
        }

        public IEnumerable<StenoIzin> GetAllStenoIzin()
        {
            return _stenoIzinRepo.GetAll(includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinByStenografId(Guid id)
        {
            return _stenoIzinRepo.Get(x => x.StenografId == id, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad)
        {
            return _stenoIzinRepo.Get(x => x.Stenograf.AdSoyad == adSoyad, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi, string? field, string? sortOrder, int? izinTur, Guid? stenografId, int pageIndex, int pagesize)
        {
            var stenoIzinList = _stenoIzinRepo.Get(x => basTarihi <= x.BaslangicTarihi && bitTarihi >= x.BaslangicTarihi, includeProperties: "Stenograf");
            if (izinTur != null)
                stenoIzinList = stenoIzinList.Where(x => (int)x.IzinTuru == izinTur);
            if (stenografId != null && stenografId != Guid.Empty)
                stenoIzinList = stenoIzinList.Where(x => x.StenografId == stenografId);

            if (stenoIzinList != null && stenoIzinList.Count() > 0)
            {
                stenoIzinList.ToList().ForEach(x => x.StenografCount = stenoIzinList.Count());
            }
            return stenoIzinList != null && stenoIzinList.Count() > 0 ? stenoIzinList.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList() : new List<StenoIzin> { };
        }

        public IEnumerable<GorevAtama> GetStenoGorevById(Guid id)
        {
            return _stenoGorevRepo.Get(x => x.Id == id, includeProperties: "Stenograf");
        }

        public IEnumerable<GorevAtama> GetStenoGorevByDateAndStatus(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int statu)
        {
            return _stenoGorevRepo.Get(x => x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi && (int)x.GorevStatu == statu, includeProperties: "Stenograf");
        }

        private int GetSaatFarkStenograf(List<Guid> stenoList, Guid birlesimId,DateTime gorevBitTarihi)
        {
            //var gk = _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.GenelKurul && (x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı || x.ToplanmaDurumu == ToplanmaStatu.Iptal));
            //if(gk != null)
            //{
            //    var gorevBitTarihi = gk.FirstOrDefault()..Value;
            //}
            //var gorevBitTarihi = model.FirstOrDefault().GorevBitisTarihi.Value;

            //var resultMax = GetMaxbitTarihStenograf(stenoList, birlesim.Id);
            //var resultFark = GetSaatFarkStenograf(stenoList, birlesim.Id, birlesim.BaslangicTarihi.Value);
            var result = _stenoGorevRepo.Get(x => x.BirlesimId != birlesimId &&
                                             stenoList.Contains(x.StenografId) &&
                                             x.GorevStatu != GorevStatu.Iptal &&
                                             x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes > 0 &&
                                             x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes <= 60);
             return result !=null && result.Count() >0 ? result.Count() : 0;
        }

        private List<KeyValuePair<Guid, DateTime?>> GetMaxbitTarihStenograf(List<Guid> stenoList, Guid birlesimId)
        {
            var listKeys = new List<KeyValuePair<Guid, DateTime?>>();

            var result = _stenoGorevRepo.Get(x => x.BirlesimId != birlesimId && stenoList.Contains(x.StenografId) && x.GorevStatu != GorevStatu.Iptal).GroupBy(x => x.StenografId).Select(g => new
            {
                StenografId = g.Key,
                MaxDate = g.Max(row => row.GorevBitisTarihi)
            });
            result.ToList().ForEach(x => listKeys.Add(new KeyValuePair<Guid, DateTime?>(x.StenografId, x.MaxDate)));
            return listKeys;
        }

        public DurumStatu GetStenoGidenGrupDurum(Guid stenoId)
        {
            //var result = _stenoGrupRepo.Get(x => x.StenoId == stenoId);
            //if (result != null && result.Count() > 0)
            //    return result.FirstOrDefault().GidenGrupMu;
            return DurumStatu.Hayır;
        }

        public void UpdateGidenGrup(List<GorevAtama> list)
        {
            //var hasGidenGrup = list.Where(x => x.GorevStatu == GorevStatu.GidenGrup);
            //if (hasGidenGrup != null && hasGidenGrup.Count() > 0)
            //{
            //    var grpId = _stenoGrupRepo.Get(x => x.StenoId == hasGidenGrup.FirstOrDefault().StenografId).Select(x => x.GrupId);
            //    if (grpId != null)
            //    {
            //        var gidenGrup =_gidenGrupRepo.Get(x => x.GrupId == grpId.FirstOrDefault());
            //        if (gidenGrup != null)
            //        {
            //            var allgrps = _grupRepo.Get(x=>x.Id !=grpId.FirstOrDefault()).OrderBy(x=>x.Ad);
            //            gidenGrup.FirstOrDefault().IsDeleted = true;
            //            _gidenGrupRepo.Update(gidenGrup.FirstOrDefault());
            //            _gidenGrupRepo.Save();

            //            var newGidenGrup = new GidenGrup();
            //            newGidenGrup.GrupId = allgrps.FirstOrDefault().Id;
            //            newGidenGrup.GidenGrupTarihi =DateTime.Today.AddDays(1);
            //            _gidenGrupRepo.Create(newGidenGrup);
            //            _gidenGrupRepo.Save();
            //        }
            //    }
            //}
        }

        public void UpdateStenoGorev(List<GorevAtama> entityList)
        {
            _stenoGorevRepo.Update(entityList, CurrentUser.Id);
            _stenoGorevRepo.Save();
        }

        public void CreateStenoIzin(StenoIzin entity)
        {
            _stenoIzinRepo.Create(entity, CurrentUser.Id);
            _stenoIzinRepo.Save();
        }

        public IEnumerable<GorevAtama> GetStenoGorevByBirlesimIdAndGorevTuru(Guid birlesimId, int gorevTuru)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && (int)x.Stenograf.StenoGorevTuru == gorevTuru, includeProperties: "Stenograf,Birlesim").OrderBy(x => x.GorevBasTarihi);
        }

        public IEnumerable<GorevAtama> GetStenoGorevByGorevTuru(int gorevTuru)
        {
            return _stenoGorevRepo.Get(includeProperties: "Stenograf.StenoIzins,Birlesim");
        }

        public List<GorevAtama> GetStenoGorevBySatatus(int status)
        {
            return _stenoGorevRepo.Get(x => (int)x.GorevStatu == status, includeProperties: "Stenograf").ToList();
        }

        public IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId)
        {
            return _stenografRepo.Get(x=>x.GrupId== groupId);
        }

        public IEnumerable<Stenograf> GetStenoGorevByTur(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
        }

        public IEnumerable<GorevAtama> GetStenoGorevByName(string adSoyad)
        {
            return _stenoGorevRepo.Get(x => x.Stenograf.AdSoyad == adSoyad, includeProperties: "Stenograf");
        }

        public IEnumerable<Birlesim> GetBirlesimByDateAndTur(DateTime basTarihi, DateTime bitTarihi, int toplanmaTuru)
        {
            return _birlesimRepo.Get(x => x.BaslangicTarihi >= basTarihi && x.BaslangicTarihi <= bitTarihi && (int)x.ToplanmaTuru == toplanmaTuru, includeProperties: "Oturums");
        }

        public IEnumerable<Birlesim> GetBirlesimByDate(DateTime basTarihi, int toplanmaTuru)
        {
            return _birlesimRepo.Get(x => (int)x.ToplanmaTuru == toplanmaTuru, includeProperties: "Oturums").Where(x => x.BaslangicTarihi.Value.ToShortDateString() == basTarihi.ToShortDateString());
        }

        public void CreateStenograf(Stenograf entity)
        {
            _stenografRepo.Create(entity, CurrentUser.Id);
            _stenografRepo.Save();
        }
        public IEnumerable<Stenograf> GetAllStenografByGorevTuru(int? gorevTuru)
        {
            if(gorevTuru != null)
                  return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru, includeProperties: "GorevAtamas");
            else
                return _stenografRepo.Get(includeProperties: "GorevAtamas");
        }

        public void DeleteStenoGorev(Guid stenoGorevId)
        {
            _stenoGorevRepo.Delete(stenoGorevId);
            _stenoGorevRepo.Save();
        }

        public IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            if (stenografId != null)
                return _stenoGorevRepo.Get(x => x.StenografId == stenografId && x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
            else
                return _stenoGorevRepo.Get(x => x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
        }

        public IEnumerable<GorevAtama> GetStenoGorevByGrupId(Guid id)
        {
            return _stenografRepo.Get(x=>x.GrupId == id, includeProperties: "GorevAtamas").SelectMany(x => x.GorevAtamas);
        }

        public IEnumerable<Stenograf> GetAllStenoGrupNotInclueded()
        {
            return _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf );
        }
    
        public IEnumerable<GorevAtama> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf,Birlesim");
        }

        public void UpdateStenoSiraNo(List<Stenograf> stenoList)
        {
            foreach (var steno in stenoList)
            {
                _stenografRepo.Update(steno, CurrentUser.Id);
                _stenografRepo.Save();
            }

        }

        public IEnumerable<Grup> GetAllStenografGroup(int gorevTuru)
        {
            return _grupRepo.Get(x => (int)x.StenoGrupTuru == gorevTuru, includeProperties: "Stenografs").OrderBy(x => x.Ad);
        }


        public IEnumerable<Birlesim> GetKomisyonByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup)
        {
            if(yasamaId == null)
            {
                // TODO : Toplantı ve görev statülerini == yap
                return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.Komisyon && x.BaslangicTarihi >= baslangic && x.BaslangicTarihi <= bitis, includeProperties: "Komisyon").Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);
            }
            
            // TODO : Toplantı ve görev statülerini == yap
            return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.Komisyon && x.YasamaId == yasamaId, includeProperties: "Komisyon").Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);
            
        }

        public IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup)
        {
            if(yasamaId == null)
            {
                // TODO : Toplantı ve görev statülerini == yap
                return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.GenelKurul && x.BaslangicTarihi >= baslangic && x.BaslangicTarihi <= bitis).Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);
            }

            // TODO : Toplantı ve görev statülerini == yap
            return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.GenelKurul && x.YasamaId == yasamaId).Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);

        }
        public void CreateStenoGroup(Guid stenoId, Guid grupId)
        {
            var steno = _stenografRepo.GetById(stenoId);
            if(steno != null)
            {
                steno.GrupId = grupId;
                _stenografRepo.Update(steno);
                _stenografRepo.Save();
            }
        }
    }
}
