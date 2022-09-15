using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.Models;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoIzin> GetAllStenoIzin();
        IEnumerable<StenoIzin> GetStenoIzinByStenografId(Guid id);
        IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad);
        StenoIzınCountModel GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi, string? field, string? sortOrder, int? izinTur, Guid? stenografId, int pageIndex, int pagesize);
        IzınTuru GetStenoIzinTodayByStenoId(Guid? stenoId);
        IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
        void CreateStenoIzin(StenoIzin stenoGorev);
        IEnumerable<Birlesim> GetBirlesimByDateAndTur(DateTime gorevTarihi, DateTime gorevBitTarihi, int gorevTuru);
        IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId);
        void CreateStenograf(List<Stenograf> stenograf);
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int? gorevTuru);
        IEnumerable<Stenograf> GetAllStenoGrupNotInclueded();
        void UpdateStenoSiraNo(List<Stenograf> steno);
        IEnumerable<Grup> GetAllStenografGroup(int gorevTuru);
        IEnumerable<Birlesim> GetBirlesimByDate(DateTime basTarihi, int toplanmaTuru);
        IEnumerable<Birlesim> GetKomisyonByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup);
        IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup);
        void CreateStenoGroup(Guid stenoId, Guid grupId);

        Stenograf GetStenoBySiraNoAndGorevTuru(int siraNo, StenoGorevTuru stenoGorevTuru);
        IEnumerable<Stenograf> GetAllStenografWithStatisticsByGroupId(Guid? groupId);
    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<Birlesim> _birlesimRepo;

        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenografBeklemeSure> _stenoBeklemeSure;
        private IRepository<Grup> _grupRepo;
        private IRepository<Oturum> _oturumRepo;
        private IRepository<GorevAtamaGenelKurul> _genelKurulAtamaRepo;
        private IRepository<GorevAtamaKomisyon> _komisyonAtamaRepo;
        private IRepository<GorevAtamaOzelToplanma> _ozelToplanmaAtamaRepo;
        private readonly IGorevAtamaGKMBusiness _gorevAtamaGKMRepo;
        private readonly IGorevAtamaKomMBusiness _gorevAtamaKomMRepo;
        public readonly IMapper _mapper;
        public StenografService(IRepository<StenoIzin> stenoIzinRepo, 
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenografRepo,
                                IRepository<StenografBeklemeSure> stenoBeklemeSure,
                                IRepository<Grup> grupRepo,
                                IRepository<Birlesim> birlesimRepo,
                                IRepository<Oturum> oturumRepo,
                                IGorevAtamaGKMBusiness gorevAtamaGKMRepo,
                                IGorevAtamaKomMBusiness gorevAtamaKomMRepo,
                                IRepository<GorevAtamaGenelKurul> genelKurulAtamaRepo,
                                IRepository<GorevAtamaKomisyon> komisyonAtamaRepo,
                                IRepository<GorevAtamaOzelToplanma> ozelToplanmaAtamaRepo,
                                IMapper mapper,
                                IServiceProvider provider) : base(provider)
        {
            _stenoIzinRepo = stenoIzinRepo;
            _unitWork = unitWork;
            _stenografRepo = stenografRepo;
            _stenoBeklemeSure = stenoBeklemeSure;
            _birlesimRepo = birlesimRepo;
            _oturumRepo = oturumRepo;
            _grupRepo = grupRepo;
            _gorevAtamaGKMRepo = gorevAtamaGKMRepo;
            _gorevAtamaKomMRepo = gorevAtamaKomMRepo;
            _genelKurulAtamaRepo = genelKurulAtamaRepo;
            _ozelToplanmaAtamaRepo = ozelToplanmaAtamaRepo;
            _komisyonAtamaRepo = komisyonAtamaRepo;
            _mapper = mapper;
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

        public StenoIzınCountModel GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi, string? field, string? sortOrder, int? izinTur, Guid? stenografId, int pageIndex, int pagesize)
        {
            var stenoIzinList = _stenoIzinRepo.Get(x => basTarihi <= x.BaslangicTarihi && bitTarihi >= x.BaslangicTarihi, includeProperties: "Stenograf");

            //izin başlangıç tarihi daha geriden başlamış fakat aralık dönemi içinde izni tanımlı ise filtre listesine eklenir
            #region filter permission period before filter's start date
            if (stenoIzinList != null && stenoIzinList.Count() > 0)
            {
                var endDateMatchedValues = _stenoIzinRepo.Get(x => x.BitisTarihi >= basTarihi, includeProperties: "Stenograf");
                if (endDateMatchedValues != null)
                {
                    stenoIzinList.Concat(endDateMatchedValues);
                }
            }
            else
            {
                stenoIzinList = _stenoIzinRepo.Get(x => x.BitisTarihi >= basTarihi, includeProperties: "Stenograf");//x.BitisTarihi <= bitTarihi && x.BitisTarihi >= basTarihi
            }
            #endregion

            #region İzin Tür Filtreleme
            if (izinTur != null)
                stenoIzinList = stenoIzinList.Where(x => (int)x.IzinTuru == izinTur);
            if (stenografId != null && stenografId != Guid.Empty)
                stenoIzinList = stenoIzinList.Where(x => x.StenografId == stenografId);
            #endregion

            #region field sort

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
                    if (field == "stenografAdSoyad")
                    {
                        stenoIzinList = sortOrder == "desc" ? stenoIzinList.OrderByDescending(x => x.Stenograf.AdSoyad) : stenoIzinList.OrderBy(x => x.Stenograf.AdSoyad);
                    }
                }

                stenoIzinList.ToList().ForEach(x => x.StenografCount = stenoIzinList.Count());
            }
            #endregion

            if (stenoIzinList != null && stenoIzinList.Count() > 0)
            {
                stenoIzinList = stenoIzinList.Distinct();
            }

            StenoIzınCountModel stenoIzınCountModel = new StenoIzınCountModel();
            stenoIzınCountModel.StenoIzinModels = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoIzinList != null && stenoIzinList.Count() > 0 ? stenoIzinList.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList() : new List<StenoIzin> { });
            stenoIzınCountModel.count = stenoIzinList.Count();
            return stenoIzınCountModel;
        }

        public IzınTuru GetStenoIzinTodayByStenoId(Guid? stenoId)
        {
            try
            {
                var date = DateTime.Today;
                var stenoIzinList = _stenoIzinRepo.Get(x => x.StenografId == stenoId && x.BaslangicTarihi <= date && x.BitisTarihi >= date, includeProperties: "Stenograf").FirstOrDefault();

                return stenoIzinList == null ? IzınTuru.Bulunmuyor : stenoIzinList.IzinTuru;
            }
            catch (Exception ex)
            {
                return IzınTuru.Bulunmuyor;
            }
        }
       
        public DurumStatu GetStenoGidenGrupDurum(Guid stenoId)
        {
            //var result = _stenoGrupRepo.Get(x => x.StenoId == stenoId);
            //if (result != null && result.Count() > 0)
            //    return result.FirstOrDefault().GidenGrupMu;
            return DurumStatu.Hayır;
        }

        public void CreateStenoIzin(StenoIzin entity)
        {
            _stenoIzinRepo.Create(entity, CurrentUser.Id);
            _stenoIzinRepo.Save();

            var result = _genelKurulAtamaRepo.Get(x =>x.StenografId == entity.StenografId && x.GorevBasTarihi.Value >= entity.BaslangicTarihi && x.GorevBasTarihi.Value <= entity.BitisTarihi);
            var resultKom = _komisyonAtamaRepo.Get(x => x.StenografId == entity.StenografId && x.GorevBasTarihi.Value >= entity.BaslangicTarihi && x.GorevBasTarihi.Value <= entity.BitisTarihi);
            var resultOzel = _ozelToplanmaAtamaRepo.Get(x => x.StenografId == entity.StenografId && x.GorevBasTarihi.Value >= entity.BaslangicTarihi && x.GorevBasTarihi.Value <= entity.BitisTarihi);

            if (result != null && result.Count() > 0)
            {
                var modelList = new List<GorevAtamaModel>();
                result.ToList().ForEach(x => x.StenoIzinTuru = entity.IzinTuru);
                _genelKurulAtamaRepo.Update(result);

                foreach (var item in result.GroupBy(x => x.BirlesimId))
                {
                    var allResult = _genelKurulAtamaRepo.Get(x => x.BirlesimId == item.FirstOrDefault().BirlesimId);
                    _mapper.Map(allResult, modelList);
                    //BirlesimIzinHesaplama();
                }






            }

            if (resultKom != null && resultKom.Count() > 0)
            {
                var atamaList = new List<GorevAtamaKomisyon>();
                foreach (var item in resultKom)
                {
                    item.StenoIzinTuru = entity.IzinTuru;
                    atamaList.Add(item);
                }
                var modelList = BirlesimIzinHesaplama(_mapper.Map<List<GorevAtamaModel>>(atamaList));
                var entityList = _mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                _komisyonAtamaRepo.Update(entityList);
            }

            if (resultOzel != null && resultOzel.Count() > 0)
            {
                resultOzel.Where(x => x.StenografId == entity.StenografId && x.GorevBasTarihi >= entity.BaslangicTarihi && x.GorevBasTarihi <= entity.BitisTarihi).ToList().ForEach(x => x.StenoIzinTuru = entity.IzinTuru);
                var modelList = BirlesimIzinHesaplama(_mapper.Map<List<GorevAtamaModel>>(resultOzel));
                var entityList = _mapper.Map<List<GorevAtamaOzelToplanma>>(modelList);
                _ozelToplanmaAtamaRepo.Update(entityList);
            }
        }

        private List<GorevAtamaModel> BirlesimIzinHesaplama(List<GorevAtamaModel> atamaList)
        {
            var gorevBasTarihi = atamaList.FirstOrDefault().GorevBasTarihi.Value;
            var gorevBitTarihi = atamaList.FirstOrDefault().GorevBitisTarihi.Value;
            var lst = new List<GorevAtamaModel>();
            foreach (var item in atamaList)
            {
                if (!string.IsNullOrEmpty(item.KomisyonAd) || item.GorevStatu == GorevStatu.Iptal || item.StenoIzinTuru != IzınTuru.Bulunmuyor || item.GidenGrupMu)
                {
                    item.GorevStatu = GorevStatu.Iptal;
                }
                else
                {
                    if (item.GorevBasTarihi != gorevBasTarihi)
                    {
                        item.GorevBasTarihi = gorevBasTarihi;
                    }
                    if (item.GorevBitisTarihi != gorevBitTarihi)
                    {
                        item.GorevBitisTarihi = gorevBitTarihi;
                    }
                    gorevBasTarihi = item.GorevBitisTarihi.Value;
                    gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value.AddMinutes(item.StenoSure) : DateTime.MinValue;
                }
                lst.Add(item);
            }
            return lst;
        }

     
        public IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId)
        {
            if(groupId == null)
                return _stenografRepo.Get();

            return _stenografRepo.Get(x => x.GrupId == groupId);
        }

        public IEnumerable<Stenograf> GetAllStenografWithStatisticsByGroupId(Guid? groupId)
        {
            if (groupId == null)
                return _stenografRepo.Get(x => !x.GorevAtamaKomisyons.Where(x => x.GorevStatu != GorevStatu.Planlandı || x.GorevStatu != GorevStatu.DevamEdiyor).Select(x => x.StenografId).Contains(x.Id));

            return _stenografRepo.Get(x => !x.GorevAtamaKomisyons.Where(x=>x.GorevStatu!= GorevStatu.Planlandı || x.GorevStatu != GorevStatu.DevamEdiyor).Select(x=>x.StenografId).Contains(x.Id) &&  x.GrupId == groupId);
        }

        public IEnumerable<Stenograf> GetStenoGorevByTur(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
        }

    
        public IEnumerable<Birlesim> GetBirlesimByDateAndTur(DateTime basTarihi, DateTime bitTarihi, int toplanmaTuru)
        {
            return _birlesimRepo.Get(x => x.BaslangicTarihi >= basTarihi && x.BaslangicTarihi <= bitTarihi && (int)x.ToplanmaTuru == toplanmaTuru, includeProperties: "Oturums");
        }

        public IEnumerable<Birlesim> GetBirlesimByDate(DateTime basTarihi, int toplanmaTuru)
        {
            if(toplanmaTuru == 1)
            {
                return _birlesimRepo.Get(x => (int)x.ToplanmaTuru == toplanmaTuru, includeProperties: "Oturums,Komisyon").Where(x => x.BaslangicTarihi.Value.ToShortDateString() == basTarihi.ToShortDateString());
            }

            return _birlesimRepo.Get(x => (int)x.ToplanmaTuru == toplanmaTuru, includeProperties: "Oturums").Where(x => x.BaslangicTarihi.Value.ToShortDateString() == basTarihi.ToShortDateString());
        }

        public void CreateStenograf(List<Stenograf> entity)
        {
            _stenografRepo.Create(entity);
            _stenografRepo.Save();
        }
        public IEnumerable<Stenograf> GetAllStenografByGorevTuru(int? gorevTuru)
        {
            if (gorevTuru != null)
                return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
            else
                return _stenografRepo.Get();
        }

        public IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            if (stenografId != null)
            {
                var birlesim = _genelKurulAtamaRepo.Get(x => x.StenografId == stenografId && x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
                var komisyon = _komisyonAtamaRepo.Get(x => x.StenografId == stenografId && x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
                var ozel = _ozelToplanmaAtamaRepo.Get(x => x.StenografId == stenografId && x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");

                var model = _mapper.Map<IEnumerable<StenoGorevModel>>(birlesim)
                    .Concat(_mapper.Map<IEnumerable<StenoGorevModel>>(komisyon))
                    .Concat(_mapper.Map<IEnumerable<StenoGorevModel>>(ozel));

                return model.OrderBy(x => x.GorevBasTarihi);
            }
            else
            {
                var birlesim = _genelKurulAtamaRepo.Get(x => x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
                var komisyon = _komisyonAtamaRepo.Get(x => x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
                var ozel = _ozelToplanmaAtamaRepo.Get(x => x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");

                var model = _mapper.Map<IEnumerable<StenoGorevModel>>(birlesim)
                   .Concat(_mapper.Map<IEnumerable<StenoGorevModel>>(komisyon))
                   .Concat(_mapper.Map<IEnumerable<StenoGorevModel>>(ozel));

                return model.OrderBy(x => x.GorevBasTarihi);
            }
        }

        public IEnumerable<Stenograf> GetAllStenoGrupNotInclueded()
        {
            return _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf);
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
            if (yasamaId == null)
            {
                // TODO : Toplantı ve görev statülerini == yap
                return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.Komisyon && x.BaslangicTarihi >= baslangic && x.BaslangicTarihi <= bitis, includeProperties: "Komisyon").Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);
            }

            // TODO : Toplantı ve görev statülerini == yap
            return _birlesimRepo.Get(x => x.ToplanmaTuru == ToplanmaTuru.Komisyon && x.YasamaId == yasamaId, includeProperties: "Komisyon").Where(x => x.ToplanmaDurumu != ToplanmaStatu.Tamamlandı);

        }

        public IEnumerable<Birlesim> GetBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grup)
        {
            if (yasamaId == null)
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
            if (steno != null)
            {
                steno.GrupId = grupId;
                _stenografRepo.Update(steno);
                _stenografRepo.Save();
            }
        }

        public Stenograf GetStenoBySiraNoAndGorevTuru(int siraNo, StenoGorevTuru stenoGorevTuru)
        {
            return _stenografRepo.Get(p => p.SiraNo == siraNo && p.StenoGorevTuru == stenoGorevTuru).First();
        }
    }
}
