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
        void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId);
        IEnumerable<GorevAtama> GetStenoGorevByGrupId(Guid id);
        IEnumerable<Stenograf> GetAllStenoGrupNotInclueded();
        //List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int stenografId, int toplantiTur);
        //void UpdateStenoPlan(StenoPlan plan);
        //IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur);
        IEnumerable<GorevAtama> GetAssignedStenoByBirlesimId(Guid birlesimId);
        //IEnumerable<GorevAtama> GetIntersectStenoPlan(Guid stenoPlanId, Guid stenoId);
        void UpdateStenoSiraNo(List<Stenograf> steno);
        IEnumerable<Grup> GetAllStenografGroup(int gorevTuru);
        void UpdateBirlesimStenoGorevBaslama(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTur);
        void UpdateBirlesimStenoGorevDevamEtme(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTuru, DateTime oturumKapanmaTarihi, Guid oturumId);
        void UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId);
        void UpdateGorevDurumById(Guid id);
        void UpdateStenoGorevTamamla(Guid birlesimId, StenoGorevTuru stenoGorevTur);
        IEnumerable<Birlesim> GetBirlesimByDate(DateTime basTarihi, int toplanmaTuru);
        IEnumerable<Birlesim> GetKomisyonByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup);
        IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup);

       
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
            var stenoIzinList = _stenoIzinRepo.Get(x => (basTarihi >= x.BaslangicTarihi && basTarihi <= x.BitisTarihi) ||
                                                   (bitTarihi >= x.BaslangicTarihi && bitTarihi <= x.BitisTarihi), includeProperties: "Stenograf");
            if (izinTur != null)
                stenoIzinList = stenoIzinList.Where(x => (int)x.IzinTuru == izinTur);
            if (stenografId != null && stenografId != Guid.Empty)
                stenoIzinList = stenoIzinList.Where(x => x.StenografId == stenografId);

            if (stenoIzinList != null && stenoIzinList.Count() > 0)
            {
                if (!string.IsNullOrEmpty(field))
                {
                    if (field == "baslangicTarihi")
                    {
                        stenoIzinList = sortOrder == "desc" ? stenoIzinList.OrderByDescending(x => x.BaslangicTarihi) : stenoIzinList.OrderBy(x => x.BaslangicTarihi);
                    }
                    if (field == "bitisTarihi")
                    {
                        stenoIzinList = sortOrder == "desc" ? stenoIzinList.OrderByDescending(x => x.BitisTarihi) : stenoIzinList.OrderBy(x => x.BitisTarihi);
                    }
                    if (field == "izinTuru")
                    {
                        stenoIzinList = sortOrder == "desc" ? stenoIzinList.OrderByDescending(x => x.IzinTuru) : stenoIzinList.OrderBy(x => x.IzinTuru);
                    }
                }

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
            var gkGorev = _gorevAtamaGKMRepo.Get(x => x.StenografId == entity.StenografId.ToString() &&
                                             DateTime.ParseExact(x.GorevBasTarihi, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) >= entity.BaslangicTarihi.Value &&
                                             DateTime.ParseExact(x.GorevBasTarihi, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) <= entity.BitisTarihi.Value);
            if (gkGorev != null && gkGorev.Count()>0)
            {
                foreach (var item in gkGorev)
                {
                    item.StenoIzinTuru = entity.IzinTuru;
                    item.GorevStatu = GorevStatu.Iptal;
                    _gorevAtamaGKMRepo.UpdateAsync(item.Id, item);
                }
            }
            var komGorev = _gorevAtamaKomMRepo.Get(x => x.StenografId == entity.StenografId.ToString() && DateTime.Parse(x.GorevBasTarihi) >= entity.BaslangicTarihi && DateTime.Parse(x.GorevBasTarihi) <= entity.BitisTarihi);
            if (komGorev != null && komGorev.Count() > 0)
            {
                foreach (var item in komGorev)
                {
                    item.StenoIzinTuru = entity.IzinTuru;
                    item.GorevStatu = GorevStatu.Iptal;
                    _gorevAtamaKomMRepo.UpdateAsync(item.Id, item);
                }
            }
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
            return _stenografRepo.Get();
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
            return _stenografRepo.Get(includeProperties: "GorevAtamas").SelectMany(x => x.GorevAtamas);
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
            return _grupRepo.Get(x => (int)x.StenoGrupTuru == gorevTuru, includeProperties: "Stenograf.GorevAtamas").OrderBy(x => x.Ad);
        }

        public void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId)
        {
            var result = new Result();
            var birlesim = _birlesimRepo.GetById(birlesimId);
            if (birlesim != null && (birlesim.ToplanmaDurumu == ToplanmaStatu.Oluşturuldu || birlesim.ToplanmaDurumu == ToplanmaStatu.Planlandı))
            {
                var gorevler = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Birlesim.BitisTarihi == null, includeProperties: "Birlesim").OrderBy(x => x.GorevBasTarihi);
                var gorevSteno = gorevler.Where(x => x.StenografId == stenografId);
                if (gorevSteno != null && gorevSteno.Count() > 0)
                {
                    _stenoGorevRepo.Delete(gorevSteno);
                    _stenoGorevRepo.Save();

                    gorevler = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Birlesim.BitisTarihi == null, includeProperties: "Birlesim").OrderBy(x => x.GorevBasTarihi);
                    if (gorevler != null && gorevler.Count() > 0)
                    {
                        var minDate = gorevler.FirstOrDefault().Birlesim.BaslangicTarihi;
                        var updateList = new List<GorevAtama>();
                        foreach (var item in gorevler)
                        {
                            item.GorevBasTarihi = minDate.Value;
                            item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(item.StenoSure);
                            minDate = item.GorevBitisTarihi;
                            updateList.Add(item);
                        }
                        _stenoGorevRepo.Update(updateList);
                        _stenoGorevRepo.Save();
                    }
                    else
                    {
                        var oturum = _oturumRepo.Get(x => x.BirlesimId == birlesimId && x.BitisTarihi == null).FirstOrDefault();
                        if (oturum != null)
                        {
                            _oturumRepo.Delete(oturum);
                            _oturumRepo.Save();
                        }

                        if (birlesim != null)
                        {
                            birlesim.ToplanmaDurumu = ToplanmaStatu.Oluşturuldu;
                            _birlesimRepo.Update(birlesim);
                            _birlesimRepo.Save();
                        }
                    }
                }
            }
        }

        public void UpdateBirlesimStenoGorevDevamEtme(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTur, DateTime oturumKapanmaTarihi, Guid oturumId)
        {
            var updateList = new List<GorevAtama>();
            var resultList = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Stenograf.StenoGorevTuru == stenoGorevTur && oturumKapanmaTarihi <= x.GorevBasTarihi, includeProperties: "Birlesim,Stenograf");
            if (resultList != null && resultList.Count() > 0)
            {
                var result = resultList.FirstOrDefault();
                var ilkGorevBitisTarihi = result.GorevBasTarihi.Value;
                var mindateDiff = basTarih.Subtract(result.GorevBasTarihi.Value).TotalMinutes;
                var modResult = stenoGorevTur == StenoGorevTuru.Stenograf ? result.Birlesim.StenoSure : result.Birlesim.UzmanStenoSure;
                if (modResult != null && modResult > 0)
                {
                    var sonuc = modResult - (mindateDiff % modResult);
                    result.GorevBasTarihi = basTarih;
                    result.GorevBitisTarihi = result.GorevBasTarihi.Value.AddMinutes(sonuc);
                    result.GorevStatu = GorevStatu.DevamEdiyor;
                    result.OturumId = oturumId;
                    updateList.Add(result);

                    var remain = result.GorevBitisTarihi.Value.Subtract(ilkGorevBitisTarihi).TotalMinutes;
                    var remainResult = resultList.Where(x => x.Id != result.Id);
                    foreach (var item in remainResult)
                    {
                        item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult);
                        item.GorevStatu = GorevStatu.DevamEdiyor;
                        item.OturumId = oturumId;
                        updateList.Add(result);
                    }
                    _stenoGorevRepo.Update(updateList);
                    _stenoGorevRepo.Save();
                }
            }
        }

        public void UpdateStenoGorevTamamla(Guid birlesimId, StenoGorevTuru stenoGorevTur)
        {
            var result = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Stenograf.StenoGorevTuru == stenoGorevTur, includeProperties: "Birlesim,Stenograf").OrderBy(x => x.GorevBasTarihi);
            if (result != null && result.Count() > 0)
            {
                result.ToList().ForEach(x => x.GorevStatu = GorevStatu.Tamamlandı);
                _stenoGorevRepo.Update(result);
                _stenoGorevRepo.Save();
            }
        }

        public void UpdateBirlesimStenoGorevBaslama(Guid birlesimId, DateTime basTarih, StenoGorevTuru stenoGorevTur)
        {
            var updateList = new List<GorevAtama>();
            var result = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.Stenograf.StenoGorevTuru == stenoGorevTur, includeProperties: "Birlesim,Stenograf").OrderBy(x => x.GorevBasTarihi);
            var resultFirst = result.FirstOrDefault();
            var mindate = resultFirst.GorevBasTarihi.Value;
            var gorevId = resultFirst.Id;
            var mindateDiff = basTarih.Subtract(result.Min(x => x.GorevBasTarihi).Value).TotalMinutes;

            var birlesim = result.Select(x => x.Birlesim).FirstOrDefault();
            birlesim.BaslangicTarihi = birlesim.BaslangicTarihi.Value.AddMinutes(mindateDiff);
            birlesim.ToplanmaDurumu = ToplanmaStatu.DevamEdiyor;
            _birlesimRepo.Update(birlesim);
            _birlesimRepo.Save();

            var modResult = stenoGorevTur == StenoGorevTuru.Stenograf ? result.Select(x => x.Birlesim.StenoSure) : result.Select(x => x.Birlesim.UzmanStenoSure);
            if (modResult != null && modResult.Count() > 0)
            {
                var sonuc = modResult.FirstOrDefault() - (mindateDiff % modResult.FirstOrDefault());
                var minStenoResult = result.Where(x => x.GorevBasTarihi == mindate).FirstOrDefault();
                minStenoResult.GorevBasTarihi = mindate.AddMinutes(mindateDiff);
                minStenoResult.GorevBitisTarihi = minStenoResult.GorevBasTarihi.Value.AddMinutes(sonuc);
                minStenoResult.GorevStatu = GorevStatu.DevamEdiyor;
                var gorevBasPlan = minStenoResult.GorevBasTarihi.Value.AddMinutes(-(mindateDiff % modResult.FirstOrDefault()));
                var remain = gorevBasPlan.Subtract(mindate).TotalMinutes;
                updateList.Add(minStenoResult);

                var remainResult = result.Where(x => x.Id != gorevId);
                foreach (var item in remainResult)
                {
                    item.GorevBasTarihi = item.GorevBasTarihi.Value.AddMinutes(remain);
                    item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(modResult.FirstOrDefault());
                    minStenoResult.GorevStatu = GorevStatu.DevamEdiyor;
                    updateList.Add(item);
                }
                _stenoGorevRepo.Update(updateList);
                _stenoGorevRepo.Save();
            }
        }

        public void UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId)
        {
            var result = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.StenografId == stenoId && x.GorevBasTarihi >= DateTime.Now);
            if (result != null && result.Count() > 0)
            {
                var statuDevam = result.Where(x => x.GorevStatu == GorevStatu.Planlandı);
                if (statuDevam != null && statuDevam.Count() > 0)
                    result.ToList().ForEach(x => x.GorevStatu = GorevStatu.Iptal);
                else
                {
                    var statuIptal = result.Where(x => x.GorevStatu == GorevStatu.Iptal);
                    if (statuIptal != null && statuIptal.Count() > 0)
                        result.ToList().ForEach(x => x.GorevStatu = GorevStatu.Planlandı);
                }
                _stenoGorevRepo.Update(result, CurrentUser.Id);
                _stenoGorevRepo.Save();
            }
        }

        public void UpdateGorevDurumById(Guid id)
        {
            var result = _stenoGorevRepo.GetById(id);
            if (result != null && result.GorevBasTarihi >= DateTime.Now)
            {
                if (result.GorevStatu == GorevStatu.Iptal)
                    result.GorevStatu = GorevStatu.Planlandı;
                else if (result.GorevStatu == GorevStatu.Planlandı)
                    result.GorevStatu = GorevStatu.Iptal;
                _stenoGorevRepo.Update(result, CurrentUser.Id);
                _stenoGorevRepo.Save();
            }
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

    }
}
