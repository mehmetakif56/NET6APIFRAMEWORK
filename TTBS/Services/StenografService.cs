using TTBS.Core.Entities;
using TTBS.Core.Interfaces;


namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoPlan> GetStenoPlan();
        void CreateStenoPlan(StenoPlan stenoPlan);
        IEnumerable<StenoIzin> GetAllStenoIzin();
        IEnumerable<StenoIzin> GetStenoIzinByStenografId(Guid id);
        IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad);
        IEnumerable<StenoIzin> GetStenoIzinBetweenDate(DateTime basTarihi,DateTime bitTarihi);
        IEnumerable<StenoGorev> GetStenoGorevById(Guid id);
        IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad);
        IEnumerable<StenoGorev> GetStenoGorevByDateAndStatus(DateTime gorevAtamaTarihi, int status,int gorevSaati);
        void CreateStenoGorev(StenoGorev stenoGorev);
        void CreateStenoIzin(StenoIzin stenoGorev);
        IEnumerable<StenoGorev> GetStenoGorevByPlanId(Guid id);
        List<StenoPlan> GetStenoPlanByStatus(int status);
        IEnumerable<StenoPlan> GetStenoPlanByDateAndStatus(DateTime gorevTarihi, int gorevTuru);
        List<StenoGorev> GetStenoGorevBySatatus(int status);
        IEnumerable<Stenograf> GetAllStenograf();
        void CreateStenograf(Stenograf stenograf);
        void CreateStenoGrup(StenoGrup stenoGrup);
        IEnumerable<StenoGrup> GetAllStenoGrup();
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru);

    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<StenoGorev> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenoGrup> _stenoGrupRepo;
        public StenografService(IRepository<StenoPlan> stenoPlanRepo,
                                IRepository<StenoIzin> stenoIzinRepo, IRepository<StenoGorev> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenografRepo,
                                IRepository<StenoGrup> stenoGrupRepo, 
                                IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
            _stenografRepo = stenografRepo;
            _stenoGrupRepo = stenoGrupRepo;
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

        public IEnumerable<StenoIzin> GetStenoIzinByStenografId(Guid id)
        {
            return _stenoIzinRepo.Get(x=>x.StenografId ==id, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad)
        {
            return _stenoIzinRepo.Get(x=>x.Stenograf.AdSoyad == adSoyad, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoIzin> GetStenoIzinBetweenDate(DateTime basTarihi, DateTime bitTarihi)
        {
            return _stenoIzinRepo.Get(x => x.BaslangicTarihi <= basTarihi && x.BitisTarihi >= bitTarihi, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoGorev> GetStenoGorevById(Guid id)
        {
            return _stenoGorevRepo.Get(x=>x.Id == id);
        }       

        public IEnumerable<StenoGorev> GetStenoGorevByDateAndStatus(DateTime gorevAtamaTarihi, int statu,int gorevSaati)
        {
            return _stenoGorevRepo.Get(x => x.GörevTarihi == gorevAtamaTarihi && (int)x.GorevStatu == statu && x.GorevDakika == gorevSaati);
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

        public IEnumerable<StenoGorev> GetStenoGorevByPlanId(Guid id)
        {
            return _stenoGorevRepo.Get(x=>x.StenoPlanId == id,includeProperties: "StenoPlan");
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
            return _stenografRepo.GetAll();
        }

        public IEnumerable<Stenograf> GetStenoGorevByTur(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
        }

        public IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad)
        {
            return _stenoGorevRepo.Get(x=>x.Stenograf.AdSoyad == adSoyad);
        }

        public IEnumerable<StenoPlan> GetStenoPlanByDateAndStatus(DateTime gorevBasTarihi,int gorevTuru)
        {
            return _stenoPlanRepo.Get(x => x.BaslangicTarihi <= gorevBasTarihi && x.BitisTarihi >= gorevBasTarihi && (int)x.GorevTuru == gorevTuru);
        }

        public void CreateStenograf(Stenograf entity)
        {
            _stenografRepo.Create(entity, CurrentUser.Id);
            _stenografRepo.Save();            
        }
        public IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
        }

        public void CreateStenoGrup(StenoGrup entity)
        {
            _stenoGrupRepo.Create(entity, CurrentUser.Id);
            _stenoGrupRepo.Save(); 
        }

        public IEnumerable<StenoGrup> GetAllStenoGrup()
        {
            return _stenoGrupRepo.GetAll(includeProperties:"Grup,Stenograf");
        }
    }
}
