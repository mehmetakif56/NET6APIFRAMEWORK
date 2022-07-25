using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;

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
        void CreateDonem(Donem donem);
        void CreateYasama(Yasama donem);
        void CreateBirlesim(Birlesim birlesim);
        void DeleteBirlesim(Guid id);
        Result CreateBirlesimGorevAtama(Birlesim birlesim);
        Guid CreateOturum(Oturum oturum);
        void CreateKomisyon(Komisyon komisyon);
        void CreateGrup(Grup grup);
        //void CreateGidenGrup(GidenGrup grup);
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
    }
    public class GlobalService : BaseService, IGlobalService
    {
        private IRepository<Donem> _donemRepo;
        private IRepository<Yasama> _yasamaRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<Komisyon> _komisyonRepo;
        private IRepository<AltKomisyon> _altkomisyonRepo;
        private IRepository<Grup> _grupRepo;        
        private IRepository<StenografBeklemeSure> _stenoBeklemeSure;
        private IRepository<OzelGorevTur> _ozelGorevTurRepo;
        private IRepository<Oturum> _oturumRepo;
        private IUnitOfWork _unitWork;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IRepository<Stenograf> _stenografRepo;

        public GlobalService(IRepository<Donem> donemRepo,
                             IRepository<Yasama> yasamaRepo,
                             IRepository<Birlesim> birlesimRepo,
                             IRepository<Komisyon> komisyonRepo,
                             IRepository<Grup> grupRepo,
                             IRepository<AltKomisyon> altkomisyonRepo,
                             IRepository<StenografBeklemeSure> stenoBeklemeSure,
                             IRepository<OzelGorevTur> ozelGorevTurRepo,
                             IRepository<Oturum> oturumRepo,
                             IUnitOfWork unitWork,
                             IRepository<GorevAtama> stenoGorevRepo,
                             IRepository<Stenograf> stenografRepo,
                          
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
            _stenoGorevRepo = stenoGorevRepo;
            _stenografRepo = stenografRepo;

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

        public Result CreateBirlesimGorevAtama(Birlesim birlesim)
        {
            var result = new Result();  
            try
            {
                if(!GetBirlesimByDate())
                {
                    birlesim.ToplanmaDurumu = ToplanmaStatu.Planlandı;
                    CreateBirlesim(birlesim);

                    var oturumId = CreateOturum(new Oturum
                    {
                        BirlesimId = birlesim.Id,
                        BaslangicTarihi = birlesim.BaslangicTarihi
                    });

                    CreateStenoGorevAtamaGenelKurul(birlesim, oturumId);
                   
                    result.HasError = false;
                }
                else
                {
                    result.HasError = true;
                    result.Message = "Devam eden birlesim olduğundan yeni birlesim oluşturulamaz!";
                }

            }
            catch(Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public void CreateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Create(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();
        }

        public void CreateStenoGorevAtamaGenelKurul(Birlesim birlesim,Guid oturumId)
        {
            CreateSteno(birlesim, oturumId);
            CreateUzmanSteno(birlesim, oturumId);
        }

        private void CreateUzmanSteno(Birlesim birlesim, Guid oturumId)
        {
            var stenoList = _stenografRepo.Get(x => x.StenoGorevTuru == StenoGorevTuru.Uzman).OrderBy(x => x.SiraNo);
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                var atamaList = new List<GorevAtama>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtama();
                    newEntity.BirlesimId = birlesim.Id;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item.Id;
                    newEntity.GorevStatu = GorevStatu.Planlandı;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.UzmanStenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.UzmanStenoSure) : null;
                    newEntity.StenoSure = birlesim.UzmanStenoSure;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                _stenoGorevRepo.Save();
            }
        }

        private void CreateSteno(Birlesim birlesim, Guid oturumId)
        {
            var stenoList = _stenografRepo.Get(x=>x.StenoGorevTuru == StenoGorevTuru.Stenograf).OrderBy(x => x.SiraNo);
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                var atamaList = new List<GorevAtama>();
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtama();
                    newEntity.BirlesimId = birlesim.Id;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item.Id;
                    newEntity.GorevStatu = GorevStatu.Planlandı;
                    newEntity.GorevBasTarihi = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value.AddMinutes(firstRec * birlesim.StenoSure) : null;
                    newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.HasValue ? newEntity.GorevBasTarihi.Value.AddMinutes(birlesim.StenoSure) : null;
                    newEntity.StenoSure = birlesim.StenoSure;
                    atamaList.Add(newEntity);
                    firstRec++;
                }
                _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
                _stenoGorevRepo.Save();
            }
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
            return _komisyonRepo.Get(includeProperties: "AltKomisyons");
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

        //public void CreateGidenGrup(GidenGrup grup)
        //{
        //    _gidenGrupRepo.Create(grup, CurrentUser.Id);
        //    _gidenGrupRepo.Save();
        //}

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

        public Guid CreateOturum(Oturum oturum)
        {
            var otr = _oturumRepo.Get(x => x.BirlesimId == oturum.BirlesimId);
            if (otr != null && otr.Count()>0)
                oturum.OturumNo = otr.Max(x => x.OturumNo) + 1;
            _oturumRepo.Create(oturum, CurrentUser.Id);
            _oturumRepo.Save();
            return oturum.Id;
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
            var result = _birlesimRepo.Get(x => x.Id == id && x.ToplanmaDurumu == ToplanmaStatu.Oluşturuldu || x.ToplanmaDurumu == ToplanmaStatu.Planlandı);
            if(result !=null)
            {
                _birlesimRepo.Delete(result);
                _birlesimRepo.Save();
            }
        }
    }
}
