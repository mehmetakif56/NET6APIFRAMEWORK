using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IGlobalService
    {
        IEnumerable<Donem> GetAllDonem();
        IEnumerable<Birlesim> GetAllBirlesim();
        IEnumerable<Komisyon> GetAllKomisyon();
        IEnumerable<GorevTuru> GetAllGorev();
        Birlesim GetBirlesimById(Guid id);
        Komisyon GetKomisyonById(Guid id);
        Donem GetDonemById(Guid id);
        GorevTuru GetGorevTuruById(Guid id);
        void CreateDonem(Donem donem);
        void CreateBirlesim(Birlesim birlesim);
        void CreateKomisyon(Komisyon komisyon);
        void CreateGorevTuru(GorevTuru donem);
    }
    public class GlobalService : BaseService, IGlobalService
    {
        private IRepository<Donem> _donemRepo;
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<Komisyon> _komisyonRepo;
        private IRepository<GorevTuru> _gorevTuruRepo;
        private IUnitOfWork _unitWork;
        public GlobalService(IRepository<Donem> donemRepo, IRepository<Birlesim> birlesimRepo, 
                             IRepository<Komisyon> komisyonRepo, IRepository<GorevTuru> gorevTuruRepo,IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _donemRepo = donemRepo;
            _birlesimRepo = birlesimRepo;
            _komisyonRepo = komisyonRepo;
            _gorevTuruRepo = gorevTuruRepo;
            _unitWork = unitWork;
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
            return _donemRepo.GetById(id);
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

        public IEnumerable<GorevTuru> GetAllGorev()
        {
            return _gorevTuruRepo.GetAll();
        }

        public GorevTuru GetGorevTuruById(Guid id)
        {
            return _gorevTuruRepo.GetById(id);
        }

        public void CreateGorevTuru(GorevTuru gorevTuru)
        {
            _gorevTuruRepo.Create(gorevTuru, CurrentUser.Id);
            _gorevTuruRepo.Save();
        }
    }
}
