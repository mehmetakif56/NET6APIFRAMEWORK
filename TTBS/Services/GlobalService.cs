using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IGlobalService
    {
        IEnumerable<Donem> GetAllDonem();
        IEnumerable<Yasama> GetAllYasama();
        IEnumerable<Birlesim> GetAllBirlesim();
        IEnumerable<Komisyon> GetAllKomisyon();
        Birlesim GetBirlesimById(Guid id);
        Komisyon GetKomisyonById(Guid id);
        Donem GetDonemById(Guid id);
        Yasama GetYasamaById(Guid id);
        void CreateDonem(Donem donem);
        void CreateYasama(Yasama donem);
        void CreateBirlesim(Birlesim birlesim);
        void CreateKomisyon(Komisyon komisyon);
        void CreateGrup(Grup grup);
        IEnumerable<Grup> GetAllGrup();
        Grup GetGrupById(Guid id);
        void CreateAltKomisyon(AltKomisyon komisyon);
        void DeleteAltKomisyon(Guid id);
        void UpdateAltKomisyon(AltKomisyon komisyon);
        IEnumerable<Komisyon> GetAllAltKomisyon();
        IEnumerable<AltKomisyon> GetAltKomisyon();
        AltKomisyon GetAltKomisyonById(Guid id);
        void DeleteGroup(Grup grup);
    }
    public class GlobalService : BaseService, IGlobalService
    {
        private IRepository<Donem> _donemRepo;
        private IRepository<Yasama> _yasamaRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<Komisyon> _komisyonRepo;
        private IRepository<AltKomisyon> _altkomisyonRepo;
        private IRepository<Grup> _grupRepo;
        private IUnitOfWork _unitWork;
        public GlobalService(IRepository<Donem> donemRepo,
                             IRepository<Yasama> yasamaRepo,
                             IRepository<Birlesim> birlesimRepo,
                             IRepository<Komisyon> komisyonRepo,
                             IRepository<Grup> grupRepo,
                             IRepository<AltKomisyon> altkomisyonRepo,
                             IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _donemRepo = donemRepo;
            _birlesimRepo = birlesimRepo;
            _komisyonRepo = komisyonRepo;
            _grupRepo = grupRepo;
            _unitWork = unitWork;
            _yasamaRepo = yasamaRepo;
            _altkomisyonRepo = altkomisyonRepo;
        }
        public IEnumerable<Donem> GetAllDonem()
        {
            return _donemRepo.GetAll();
        }

        public IEnumerable<Birlesim> GetAllBirlesim()
        {
            return _birlesimRepo.GetAll();
        }

        public IEnumerable<Komisyon> GetAllKomisyon()
        {
            return _komisyonRepo.GetAll();
        }

        public Birlesim GetBirlesimById(Guid id)
        {
            return _birlesimRepo.GetById(id);
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

        public void CreateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Create(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();
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

        public IEnumerable<Grup> GetAllGrup()
        {
            return _grupRepo.GetAll();
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
    }
}
