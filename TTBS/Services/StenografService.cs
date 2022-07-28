using TTBS.Core.Entities;
using TTBS.Core.Interfaces;
using System.Linq;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;

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
        void CreateStenoGorevAtama(GorevAtama stenoGorev);
        void AddStenoGorevAtamaKomisyon(GorevAtama stenoGorev);
        void UpdateStenoGorev(List<GorevAtama> stenoGorev);
        void CreateStenoIzin(StenoIzin stenoGorev);
        IEnumerable<GorevAtama> GetStenoGorevByGorevTuru(int gorevTuru,Guid birlesimId);
        IEnumerable<GorevAtama> GetStenoGorevByBirlesimIdAndGorevTuru(Guid birlesimId, int gorevTuru);
        //List<StenoPlan> GetStenoPlanByStatus(int status);
        IEnumerable<Birlesim> GetBirlesimByDateAndTur(DateTime gorevTarihi, DateTime gorevBitTarihi, int gorevTuru);
        List<GorevAtama> GetStenoGorevBySatatus(int status);
        IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId);
        void CreateStenograf(Stenograf stenograf);
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru);
        void DeleteStenoGorev(Guid stenoGorevId);
        void DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId);
        IEnumerable<GorevAtama> GetStenoGorevByGrupId(Guid id);
        void CreateStenoGroup(StenoGrup stenograf);
        void DeleteStenoGroup(StenoGrup stenograf);
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
        void ChangeOrderStenografKomisyon(Guid kaynakBirlesimId, Guid kaynakStenografId, Guid hedefBirlesimId, Guid hedefStenografId);

        IEnumerable<Birlesim> GetBirlesimByDate(DateTime basTarihi, int toplanmaTuru);
        void ChangeSureStenografKomisyon(Guid gorevAtamaId, double sure, bool digerAtamalarDahil = false);

        IEnumerable<Birlesim> GetKomisyonByDateAndGroup(DateTime baslangic, DateTime bitis, Guid grup);
        IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime baslangic, DateTime bitis, Guid grup);
        void CreateStenoGorevDonguEkle(Guid birlesimId, Guid oturumId, IEnumerable<GorevAtama> stenoEntity, int gorevTur);
    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenoGrup> _stenoGrupRepo;
        private IRepository<StenografBeklemeSure> _stenoBeklemeSure;
        private IRepository<Grup> _grupRepo;
        private IRepository<Oturum> _oturumRepo;
        private IRepository<GidenGrup> _gidenGrupRepo;
        public StenografService(IRepository<StenoIzin> stenoIzinRepo, IRepository<GorevAtama> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenografRepo,
                                IRepository<StenoGrup> stenoGrupRepo,
                                IRepository<StenografBeklemeSure> stenoBeklemeSure,
                                IRepository<Grup> grupRepo,
                                IRepository<Birlesim> birlesimRepo,
                                IRepository<Oturum> oturumRepo,
                                IRepository<GidenGrup> gidenGrupRepo,
                                IServiceProvider provider) : base(provider)
        {
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
            _stenografRepo = stenografRepo;
            _stenoGrupRepo = stenoGrupRepo;
            _stenoBeklemeSure = stenoBeklemeSure;
            _birlesimRepo = birlesimRepo;
            _oturumRepo = oturumRepo;
            _grupRepo = grupRepo;
            _gidenGrupRepo = gidenGrupRepo;
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

        private void UpdateBirlesimGorevAtama(Birlesim birlesim)
        {
            birlesim.ToplanmaDurumu = ToplanmaStatu.Planlandı;
            birlesim.TurAdedi = birlesim.TurAdedi;
            _birlesimRepo.Update(birlesim);
            _birlesimRepo.Save();
        }

        private Guid CreateOturumGorevAtama(Oturum oturum)
        {
            _oturumRepo.Create(oturum, CurrentUser.Id);
            _oturumRepo.Save();
            return oturum.Id;
        }

        public void ChangeOrderStenografKomisyon(Guid kaynakBirlesimId, Guid kaynakStenografId, Guid hedefBirlesimId, Guid hedefStenografId)
        {
            if (kaynakBirlesimId != hedefBirlesimId)
            {
                var stenoGrevKaynak = _stenoGorevRepo.Get(x => x.BirlesimId == kaynakBirlesimId && x.StenografId == kaynakStenografId);
                if (stenoGrevKaynak != null && stenoGrevKaynak.Count() > 0)
                {
                    stenoGrevKaynak.ToList().ForEach(x => x.GorevStatu = GorevStatu.YerDegistirme);
                    _stenoGorevRepo.Update(stenoGrevKaynak);
                    _stenoGorevRepo.Save();
                }

                var stenoGrevHedef = _stenoGorevRepo.Get(x => x.BirlesimId == hedefBirlesimId && x.GorevStatu != GorevStatu.YerDegistirme, includeProperties: "Stenograf.StenoGrups").OrderBy(x => x.GorevBasTarihi);
                if (stenoGrevHedef != null && stenoGrevHedef.Count() > 0)
                {
                    var minStenoGorev = stenoGrevHedef.Where(x => x.StenografId == hedefStenografId);
                    //var hedefStenoGorev = stenoGrevHedef.Where(x => x.GorevBasTarihi >= minStenoGorev.GorevBasTarihi);

                    foreach (var item in minStenoGorev)
                    {
                        var newEntity = new GorevAtama();
                        newEntity.BirlesimId = hedefBirlesimId;
                        newEntity.OturumId = item.OturumId;
                        newEntity.StenografId = kaynakStenografId;
                        newEntity.GorevBasTarihi = item.GorevBasTarihi;
                        newEntity.GorevBitisTarihi = item.GorevBitisTarihi;
                        newEntity.StenoSure = item.StenoSure;
                        newEntity.GorevStatu = item.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                        _stenoGorevRepo.Create(newEntity);
                        _stenoGorevRepo.Save();

                        var hedefStenoGorev = stenoGrevHedef.Where(x => x.GorevBasTarihi >= item.GorevBasTarihi);
                        foreach (var hedef in hedefStenoGorev)
                        {
                            hedef.GorevBasTarihi = hedef.GorevBasTarihi.Value.AddMinutes(hedef.StenoSure);
                            hedef.GorevBitisTarihi = hedef.GorevBasTarihi.Value.AddMinutes(hedef.StenoSure);
                            hedef.GorevStatu = hedef.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet &&  hedef.GorevBasTarihi.Value.AddMinutes(9 * hedef.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                            _stenoGorevRepo.Update(hedef);
                            _stenoGorevRepo.Save();
                        }
                    }
                }
            }
            else
            {
                var stenoGrevHedef = _stenoGorevRepo.Get(x => x.BirlesimId == hedefBirlesimId && x.GorevStatu != GorevStatu.YerDegistirme).OrderBy(x => x.GorevBasTarihi);
                if (stenoGrevHedef != null && stenoGrevHedef.Count() > 0)
                {
                    var stenoList = new List<GorevAtama>();
                    var hedefSteno = stenoGrevHedef.Where(x => x.StenografId == hedefStenografId).OrderBy(x => x.GorevBasTarihi).ToArray();

                    var kaynakSteno = stenoGrevHedef.Where(x => x.StenografId == kaynakStenografId).OrderBy(x => x.GorevBasTarihi).ToArray();

                    for (int i = 0; i < hedefSteno.Count(); i++)
                    {
                        var hedefGorevBasTarihi = hedefSteno[i].GorevBasTarihi;
                        var hedefGorevBitisTarihi = hedefSteno[i].GorevBitisTarihi;
                        var hedefSure = hedefSteno[i].StenoSure;

                        hedefSteno[i].GorevBasTarihi = kaynakSteno[i].GorevBasTarihi;
                        hedefSteno[i].GorevBitisTarihi = kaynakSteno[i].GorevBitisTarihi;
                        hedefSteno[i].StenoSure = kaynakSteno[i].StenoSure;
                        stenoList.Add(hedefSteno[i]);

                        kaynakSteno[i].GorevBasTarihi = hedefGorevBasTarihi;
                        kaynakSteno[i].GorevBitisTarihi = hedefGorevBitisTarihi;
                        kaynakSteno[i].StenoSure = hedefSure;
                        stenoList.Add(kaynakSteno[i]);
                    }
                    _stenoGorevRepo.Update(stenoList);
                    _stenoGorevRepo.Save();
                }
            }
        }

        public async void ChangeSureStenografKomisyon(Guid gorevAtamaId, double sure, bool digerAtamalarDahil = false)
        {
            try
            {
                var stenoGorev = _stenoGorevRepo.GetById(gorevAtamaId);
                var hedefStenoGorev = await GetHedefStenoGorevs(stenoGorev);
                var gorevBas = hedefStenoGorev.FirstOrDefault().GorevBasTarihi;
                var gorevBit = hedefStenoGorev.FirstOrDefault().GorevBitisTarihi;
                int firstRec = 0;
                if (digerAtamalarDahil)
                {

                    foreach (var item in hedefStenoGorev)
                    {
                        item.StenoSure = sure;
                        item.GorevBasTarihi = firstRec == 0 ? gorevBas : gorevBas.Value.AddMinutes(sure);
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(sure);
                        item.GorevStatu = item.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet &&  item.GorevBasTarihi.Value.AddMinutes(9 * item.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                        gorevBas = item.GorevBasTarihi;
                        _stenoGorevRepo.Update(item);
                        _stenoGorevRepo.Save();
                        firstRec++;
                    }
                }
                else
                {

                    foreach (var item in hedefStenoGorev)
                    {
                        item.StenoSure = firstRec == 0 ? sure : item.StenoSure;
                        item.GorevBasTarihi = firstRec == 0 ? gorevBas : gorevBit;
                        item.GorevBitisTarihi = item.GorevBasTarihi.Value.AddMinutes(item.StenoSure);
                        item.GorevStatu = item.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet &&  item.GorevBasTarihi.Value.AddMinutes(9 * item.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                        gorevBit = item.GorevBitisTarihi;
                        _stenoGorevRepo.Update(item);
                        _stenoGorevRepo.Save();
                        firstRec++;
                    }
                }
            }
           catch (Exception ex)
            {

            }

        }

        public async Task<List<GorevAtama>> GetHedefStenoGorevs(GorevAtama atama)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == atama.BirlesimId && x.GorevBasTarihi >= atama.GorevBasTarihi && x.GorevStatu != GorevStatu.YerDegistirme, includeProperties: "Stenograf.StenoGrups").OrderBy(x => x.GorevBasTarihi).ToList();
        }

        private void CreateStenoGorev(Birlesim birlesim, Guid oturumId, List<Guid> stenoList, int turAdedi)
        {
            int firstRec = 0;
            for (int i = 0; i < turAdedi; i++)
            {
                var atamaList = new List<GorevAtama>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtama();
                    newEntity.BirlesimId = birlesim.Id;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.StenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.StenoSure) : null;
                    newEntity.StenoSure = birlesim.StenoSure;
                    newEntity.GorevStatu = GetStenoGidenGrupDurum(item) == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                _stenoGorevRepo.Save();

               //UpdateGidenGrup(atamaList);
            }
        }

        public void CreateStenoGorevAtama(GorevAtama entity)
        {
            var birlesim = _birlesimRepo.GetById(entity.BirlesimId);
            UpdateBirlesimGorevAtama(birlesim);

            var oturumId = CreateOturumGorevAtama(new Oturum
            {
                BirlesimId = birlesim.Id,
                BaslangicTarihi = birlesim.BaslangicTarihi
            });

            
            CreateStenoGorev(birlesim, oturumId, entity.StenografIds, entity.TurAdedi);
        }

        public void AddStenoGorevAtamaKomisyon(GorevAtama entity)
        {
            var stenoList = GetStenoGorevByBirlesimIdAndGorevTuru(entity.BirlesimId, 0);
            if (stenoList != null && stenoList.Count() > 0)
            {
                var grpListCnt = stenoList.GroupBy(c => new
                {
                    c.StenografId,
                }).Count();
              

                var atamaList = new List<GorevAtama>();
                for (int i = 1; i <= stenoList.Count() / grpListCnt; i++)
                {
                    var grpList = stenoList.Take(i * grpListCnt);
                    var maxDate = grpList.Max(x => x.GorevBitisTarihi);
                    var maxSure = grpList.Max(x => x.StenoSure);
                    int firstRec = 0;
                    foreach (var item in entity.StenografIds)
                    {
                        var newEntity = new GorevAtama();
                        newEntity.BirlesimId = entity.BirlesimId;
                        newEntity.OturumId = 
                        newEntity.StenografId = item;
                        newEntity.GorevBasTarihi = maxDate.Value.AddMinutes(firstRec * maxSure);
                        newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.Value.AddMinutes(maxSure);
                        newEntity.StenoSure = maxSure;
                        var durum = maxDate.Value > stenoList.FirstOrDefault().Birlesim.BaslangicTarihi.Value ? GorevStatu.GorevZamanAsim : GorevStatu.Planlandı;
                        if (durum != GorevStatu.GorevZamanAsim)
                            newEntity.GorevStatu = (GetStenoGidenGrupDurum(item) == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı);
                        else
                            newEntity.GorevStatu = durum;
                        atamaList.Add(newEntity);
                        firstRec++;
                        maxDate = newEntity.GorevBasTarihi;
                        var upateList = stenoList.Where(x => x.GorevBasTarihi >= maxDate);
                        if (upateList != null && upateList.Count() > 0)
                        {
                            var firstRecord = upateList.OrderBy(x => x.GorevBasTarihi).FirstOrDefault();
                            var firstDate = firstRecord.GorevBasTarihi.Value.AddMinutes(firstRecord.StenoSure);
                            foreach (var hedef in upateList)
                            {
                                hedef.GorevBasTarihi = firstDate;
                                hedef.GorevBitisTarihi = hedef.GorevBasTarihi.Value.AddMinutes(hedef.StenoSure);
                                hedef.GorevStatu = hedef.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && hedef.GorevBasTarihi.Value.AddMinutes(9 * hedef.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                                firstDate = hedef.GorevBitisTarihi.Value;
                                _stenoGorevRepo.Update(hedef);
                                _stenoGorevRepo.Save();
                            }
                        }
                    }
                }
                _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                _stenoGorevRepo.Save();

                //UpdateGidenGrup(atamaList);
            }
            else if (entity.StenografIds != null && entity.StenografIds.Count > 0)
            {
                var birlesim = _birlesimRepo.GetById(entity.BirlesimId);
                if (birlesim != null)
                {
                    var minDate = birlesim.BaslangicTarihi;
                    var atamaList = new List<GorevAtama>();
                    int firstRec = 1;
                    foreach (var item in entity.StenografIds)
                    {
                        var newEntity = new GorevAtama();
                        newEntity.BirlesimId = entity.BirlesimId;
                        newEntity.OturumId = entity.OturumId;
                        newEntity.StenografId = item;
                        newEntity.GorevBasTarihi = minDate.Value;
                        newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.Value.AddMinutes(firstRec*birlesim.StenoSure);
                        newEntity.StenoSure = birlesim.StenoSure;
                        newEntity.GorevStatu = GetStenoGidenGrupDurum(item) == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                        atamaList.Add(newEntity);
                        minDate = newEntity.GorevBitisTarihi;
                        firstRec++;
                    }
                    _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                    _stenoGorevRepo.Save();

                   // UpdateGidenGrup(atamaList);
                }
            }
        }

        public DurumStatu GetStenoGidenGrupDurum(Guid stenoId)
        {
            var result = _stenoGrupRepo.Get(x => x.StenoId == stenoId);
            if (result != null && result.Count() > 0)
                return result.FirstOrDefault().GidenGrupMu;
            return DurumStatu.Hayır;
        }

        public void CreateStenoGorevDonguEkle(Guid birlesimId, Guid oturumId, IEnumerable<GorevAtama> stenoList, int gorevturu)
        {
            int firsRec = 0;
            var grpListCnt = stenoList.GroupBy(c => new {
                c.StenografId,
            }).Count();
            var grpList = stenoList.TakeLast(grpListCnt);
            var lastStenoSure = grpList.LastOrDefault().StenoSure;
            var maxDate = stenoList.Max(x => x.GorevBitisTarihi);
            var birlesimtur = stenoList.Select(x => x.Birlesim).FirstOrDefault().ToplanmaTuru;
            var sure = gorevturu == (int)StenoGorevTuru.Stenograf ? stenoList.Select(x => x.Birlesim).FirstOrDefault().StenoSure : stenoList.Select(x => x.Birlesim).FirstOrDefault().UzmanStenoSure;
            var atamaList = new List<GorevAtama>();
            foreach (var item in grpList)
            {
                var newEntity = new GorevAtama();
                newEntity.BirlesimId = birlesimId;
                newEntity.OturumId = oturumId;
                newEntity.StenografId = item.StenografId;
                sure = birlesimtur == ToplanmaTuru.GenelKurul ? sure : lastStenoSure;
                newEntity.GorevBasTarihi = maxDate.HasValue ? maxDate.Value.AddMinutes(firsRec * sure) : null;
                newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(sure) : null;
                newEntity.StenoSure = sure;
                newEntity.GorevStatu = item.Stenograf?.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet &&  newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                atamaList.Add(newEntity);
                firsRec++;

            }

            
            _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
            _stenoGorevRepo.Save();

            //UpdateGidenGrup(atamaList);

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
            return _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && (int)x.Stenograf.StenoGorevTuru == gorevTuru && x.GorevStatu != GorevStatu.YerDegistirme, includeProperties: "Stenograf.StenoGrups,Birlesim").OrderBy(x => x.GorevBasTarihi);
        }

        public IEnumerable<GorevAtama> GetStenoGorevByGorevTuru(int gorevTuru,Guid BirlesimId)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == BirlesimId);
        }

        //public List<StenoPlan> GetStenoPlanByStatus(int status)
        //{
        //    return _stenoPlanRepo.Get(x => (int)x.GorevStatu == status).ToList();
        //}

        public List<GorevAtama> GetStenoGorevBySatatus(int status)
        {
            return _stenoGorevRepo.Get(x => (int)x.GorevStatu == status, includeProperties: "Stenograf").ToList();
        }

        public IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId)
        {
            if (groupId != null)
                return _stenoGrupRepo.Get(x => x.GrupId == groupId, includeProperties: "Stenograf").Select(x => x.Stenograf);
            else
                return _stenoGrupRepo.Get(includeProperties: "Stenograf").Select(x => x.Stenograf);
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
        public IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru, includeProperties: "GorevAtamas");
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
            return _stenoGrupRepo.Get(x => x.GrupId == id, includeProperties: "Stenograf.GorevAtamas").SelectMany(x => x.Stenograf.GorevAtamas);
        }

        public void CreateStenoGroup(StenoGrup entity)
        {
            _stenoGrupRepo.Create(entity, CurrentUser.Id);
            _stenoGrupRepo.Save();
        }
        public IEnumerable<Stenograf> GetAllStenoGrupNotInclueded()
        {
            return _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf && !x.StenoGrups.Select(x => x.StenoId).Contains(x.Id));
        }
        public void DeleteStenoGroup(StenoGrup entity)
        {
            _stenoGrupRepo.Delete(entity);
            _stenoGrupRepo.Save();
        }

        //public List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int stenoGorevTuru,int toplantiTur)
        //{
        //    var lst = _stenoBeklemeSure.Get(x => (int)x.PlanTuru == toplantiTur);
        //    //if(lst !=null && lst.Count()>0)
        //    //{
        //    //    basTarihi = basTarihi.AddMinutes(-lst.FirstOrDefault().GorevOnceBeklemeSuresi);
        //    //    bitTarihi = bitTarihi.AddMinutes(lst.FirstOrDefault().GorevSonraBeklemeSuresi);
        //    //}

        //    var allList=  new List<Stenograf>();
        //    var stList = _stenografRepo.Get(x => (int)x.StenoGorevTuru == stenoGorevTuru);
        //    foreach (var st in stList)
        //    {
        //        var cnt = _stenoGorevRepo.Get(x => (basTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && basTarihi <= x.StenoPlan.PlanlananBitisTarihi) || 
        //                                           (bitTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && bitTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                           (x.StenoPlan.PlanlananBaslangicTarihi>= basTarihi && x.StenoPlan.PlanlananBaslangicTarihi <= bitTarihi) ||
        //                                           (x.StenoPlan.PlanlananBitisTarihi >= basTarihi && x.StenoPlan.PlanlananBitisTarihi <= bitTarihi),
        //                                           includeProperties: "StenoPlan,Stenograf").Where(x=>x.StenografId == st.Id);
        //        st.StenoGorevDurum = cnt!=null && cnt.Count() > 0 ? true : false;
        //      allList.Add(st);
        //    }
        //    return allList;

        //}

        //public IEnumerable<GorevAtama> GetIntersectStenoPlan(Guid stenoPlanId,Guid stenoId)
        //{
        //    var plan = _stenoPlanRepo.GetById(stenoPlanId);
        //    var basTarihi = plan.PlanlananBaslangicTarihi;
        //    var bitTarihi =plan.PlanlananBitisTarihi;

        //    return _stenoGorevRepo.Get(x => (basTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && basTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                           (bitTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && bitTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                           (x.StenoPlan.PlanlananBaslangicTarihi >= basTarihi && x.StenoPlan.PlanlananBaslangicTarihi <= bitTarihi) ||
        //                                           (x.StenoPlan.PlanlananBitisTarihi >= basTarihi && x.StenoPlan.PlanlananBitisTarihi <= bitTarihi),
        //                                           includeProperties: "StenoPlan,Stenograf").Where(x => x.StenografId == stenoId);

        //}

        //public IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur)
        //{
        //    var lst = _stenoBeklemeSure.Get(x => (int)x.PlanTuru == toplantiTur);
        //    //if (lst != null && lst.Count() > 0)
        //    //{
        //    //    basTarihi = basTarihi.AddMinutes(-lst.FirstOrDefault().GorevOnceBeklemeSuresi);
        //    //    bitTarihi = bitTarihi.AddMinutes(lst.FirstOrDefault().GorevSonraBeklemeSuresi);
        //    //}

        //    var allList = new List<Stenograf>();
        //    var stList = _stenografRepo.Get(x => x.StenoGrups.Select(x => x.GrupId).Contains(groupId), includeProperties: "StenoGrups");
        //    foreach (var st in stList)
        //    {
        //        var cnt = _stenoGorevRepo.Get(x => (basTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && basTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                          (bitTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && bitTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                          (x.StenoPlan.PlanlananBaslangicTarihi >= basTarihi && x.StenoPlan.PlanlananBaslangicTarihi <= bitTarihi) ||
        //                                          (x.StenoPlan.PlanlananBitisTarihi >= basTarihi && x.StenoPlan.PlanlananBitisTarihi <= bitTarihi),
        //                                          includeProperties: "StenoPlan,Stenograf").Where(x => x.StenografId == st.Id);
        //        st.StenoGorevDurum = cnt != null && cnt.Count() > 0 ? true : false;
        //        allList.Add(st);
        //    }
        //    return allList;
        //}

        public IEnumerable<GorevAtama> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId, includeProperties: "Stenograf");
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
            return _grupRepo.Get(x => (int)x.StenoGrupTuru == gorevTuru, includeProperties: "StenoGrups.Stenograf.GorevAtamas").OrderBy(x => x.Ad);
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
            var result = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId && x.StenoGorevTuru == stenoGorevTur, includeProperties: "Birlesim,Stenograf").OrderBy(x => x.GorevBasTarihi);
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

        public IEnumerable<Birlesim> GetKomisyonByDateAndGroup(DateTime baslangic, DateTime bitis, Guid grup)
        {
            // TODO : Toplantı ve görev statülerini == yap
            return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.Komisyon && x.BaslangicTarihi >= baslangic && x.BaslangicTarihi <= bitis, includeProperties: "Komisyon").Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);
        }

        public IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime baslangic, DateTime bitis, Guid grup)
        {
            // TODO : Toplantı ve görev statülerini == yap
            return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.GenelKurul && x.BaslangicTarihi >= baslangic && x.BaslangicTarihi <= bitis).Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);
        }

    }
}
