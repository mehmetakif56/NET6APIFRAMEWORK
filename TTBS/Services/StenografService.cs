using TTBS.Core.Entities;
using TTBS.Core.Interfaces;


namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoPlan> GetStenoPlan();
        void CreateStenoPlan(StenoPlan stenoPlan);
        IEnumerable<StenoIzin> GetAllStenoIzin();
        IEnumerable<StenoIzin> GetStenoIzinById(Guid id);
        IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad);
        IEnumerable<StenoIzin> GetStenoIzinBetweenDate(DateTime basTarihi,DateTime bitTarihi);
        StenoGorev GetStenoGorevById(Guid id);
        IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad);
        IEnumerable<StenoGorev> GetStenoGorevByDateAndStatus(DateTime gorevTarihi, int status);
        void CreateStenoGorev(StenoGorev stenoGorev);
        void CreateStenoIzin(StenoIzin stenoGorev);
        List<StenoGorev> GetStenoGorevByPlanId(Guid id);
        List<StenoPlan> GetStenoPlanByStatus(int status);
        List<StenoGorev> GetStenoGorevBySatatus(int status);
        IEnumerable<Stenograf> GetAllStenograf();

    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<StenoGorev> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenograf;
        public StenografService(IRepository<StenoPlan> stenoPlanRepo,
                                IRepository<StenoIzin> stenoIzinRepo, IRepository<StenoGorev> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenograf,
                                IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
            _stenograf = stenograf;
        }
        public IEnumerable<StenoPlan> GetStenoPlan()
        {
            return _stenoPlanRepo.GetAll(includeProperties: "Birlesim,Komisyon,StenoGorevs");
        }

        public void CreateStenoPlan(StenoPlan entity)
        {
            entity.KomisyonId = entity.KomisyonId ==Guid.Empty ? null : entity.KomisyonId;
            _stenoPlanRepo.Create(entity, CurrentUser.Id);
            _stenoPlanRepo.Save();
        }

        public IEnumerable<StenoIzin> GetAllStenoIzin()
        {
            return _stenoIzinRepo.GetAll(includeProperties:"Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinById(Guid id)
        {
            return _stenoIzinRepo.Get(x=>x.Id ==id, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad)
        {
            return _stenoIzinRepo.Get(x=>x.Stenograf.AdSoyad == adSoyad, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinBetweenDate(DateTime basTarihi, DateTime bitTarihi)
        {
            return _stenoIzinRepo.Get(x => x.BaslangicTarihi >= basTarihi && x.BitisTarihi <= bitTarihi, includeProperties: "Stenograf");
        }

        public StenoGorev GetStenoGorevById(Guid id)
        {
            return _stenoGorevRepo.GetById(id);
        }       

        public IEnumerable<StenoGorev> GetStenoGorevByDateAndStatus(DateTime gorevTarihi, int statu)
        {
            return _stenoGorevRepo.Get(x => x.GörevTarihi == gorevTarihi && (int)x.GorevStatu == statu);
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

        public List<StenoGorev> GetStenoGorevByPlanId(Guid id)
        {
            return _stenoGorevRepo.Get(x=>x.StenoPlanId == id,includeProperties: "StenoPlan").ToList();
        }

        public List<StenoPlan> GetStenoPlanByStatus(int status)
        {
            return _stenoPlanRepo.Get(x => (int)x.GorevStatu == status).ToList();
        }

        public List<StenoGorev> GetStenoGorevBySatatus(int status)
        {
            return _stenoGorevRepo.Get(x => (int)x.GorevStatu == status).ToList();
        }

        public IEnumerable<Stenograf> GetAllStenograf()
        {
            return _stenograf.GetAll();
        }

        public IEnumerable<Stenograf> GetStenoGorevByTur(int gorevTuru)
        {
            return  _stenograf.Get(x => (int)x.StenoGorevTuru == gorevTuru);
        }

        public IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad)
        {
            return _stenoGorevRepo.Get(x=>x.Stenograf.AdSoyad == adSoyad);
        }
    }
}
