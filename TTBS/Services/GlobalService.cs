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
    }
    public class GlobalService : BaseService, IGlobalService
    {
        private IRepository<Donem> _donemRepo;
        private IRepository<Yasama> _yasamaRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<Komisyon> _komisyonRepo;
        private IUnitOfWork _unitWork;
        public GlobalService(IRepository<Donem> donemRepo,
                             IRepository<Yasama> yasamaRepo,
                             IRepository<Birlesim> birlesimRepo, 
                             IRepository<Komisyon> komisyonRepo, 
                             IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _donemRepo = donemRepo;
            _birlesimRepo = birlesimRepo;
            _komisyonRepo = komisyonRepo;
            _unitWork = unitWork;
            _yasamaRepo = yasamaRepo;
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
    }
}
