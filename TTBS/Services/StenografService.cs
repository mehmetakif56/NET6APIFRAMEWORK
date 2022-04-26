using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoPlan> GetStenoPlan();
        void CreateStenoPlan(StenoPlan stenoPlan);
        IEnumerable<StenoIzin> GetAllStenoIzin();

        StenoIzin GetStenoIzinById(Guid id);

        IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad);

        IEnumerable<StenoIzin> GetStenoIzinBetweenDate(DateTime basTarihi,DateTime bitTarihi);

        StenoGorev GetStenoGorevById(Guid id);

        IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad);

        IEnumerable<StenoGorev> GetStenoGorevByDateAndTime(DateTime? gorevTarihi, int gorevSaati);

        void CreateStenoGorev(StenoGorev stenoGorev);

        void CreateStenoIzin(StenoIzin stenoGorev);

    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<StenoGorev> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        public StenografService(IRepository<StenoPlan> stenoPlanRepo,
                                IRepository<StenoIzin> stenoIzinRepo, IRepository<StenoGorev> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
        }
        public IEnumerable<StenoPlan> GetStenoPlan()
        {
            return _stenoPlanRepo.GetAll();
        }

        public void CreateStenoPlan(StenoPlan entity)
        {
            _stenoPlanRepo.Create(entity, CurrentUser.Id);
            _stenoPlanRepo.Save();
        }

        public IEnumerable<StenoIzin> GetAllStenoIzin()
        {
            return _stenoIzinRepo.GetAll();
        }

        public StenoIzin GetStenoIzinById(Guid id)
        {
            return _stenoIzinRepo.GetById(id);
        }

        public IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad)
        {
            return _stenoIzinRepo.Get(x=>x.AdSoyad == adSoyad);
        }

        public IEnumerable<StenoIzin> GetStenoIzinBetweenDate(DateTime basTarihi, DateTime bitTarihi)
        {
            return _stenoIzinRepo.Get(x => x.BaslangicTarihi >= basTarihi && x.BitisTarihi <= bitTarihi);
        }

        public StenoGorev GetStenoGorevById(Guid id)
        {
            return _stenoGorevRepo.GetById(id);
        }

        public IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad)
        {
            return _stenoGorevRepo.Get(x => x.AdSoyad == adSoyad);
        }

        public IEnumerable<StenoGorev> GetStenoGorevByDateAndTime(DateTime? gorevTarihi, int gorevSaati)
        {
            return _stenoGorevRepo.Get(x => x.GörevTarihi == gorevTarihi.Value && x.GorevSaati == gorevSaati);
        }

        public void CreateStenoGorev(StenoGorev entity)
        {
            _stenoGorevRepo.Create(entity, CurrentUser.Id);
            _stenoGorevRepo.Save();
        }

        public void CreateStenoIzin(StenoIzin entity)
        {
            _stenoIzinRepo.Create(entity, CurrentUser.Id);
            _stenoIzinRepo.Save();
        }
    }
}
