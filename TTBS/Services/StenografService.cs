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
        IEnumerable<StenoIzin> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi,DateTime bitTarihi ,Guid? stenografId);
        IEnumerable<StenoGorev> GetStenoGorevById(Guid id);
        IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad);
        IEnumerable<StenoGorev> GetStenoGorevByDateAndStatus(DateTime gorevAtamaTarihi, int status);
        IEnumerable<StenoGorev> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevAtamaTarihi);
        void CreateStenoGorev(StenoGorev stenoGorev);
        void CreateStenoIzin(StenoIzin stenoGorev);
        IEnumerable<StenoGorev> GetStenoGorevByPlanId(Guid id);
        List<StenoPlan> GetStenoPlanByStatus(int status);
        IEnumerable<StenoPlan> GetStenoPlanByDateAndStatus(DateTime gorevTarihi, int gorevTuru);
        List<StenoGorev> GetStenoGorevBySatatus(int status);
        IEnumerable<Stenograf> GetAllStenograf(Guid? groupId);
        void CreateStenograf(Stenograf stenograf);
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru);
        void UpdateStenoGorev(StenoGorev stenoGorev);
        IEnumerable<StenoGorev> GetStenoGorevByGrupId(Guid id);
        IEnumerable<StenoGorev> GetStenoGorevByPlanDateAndStatus(DateTime gorevTarihi, int gorevturu);

    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<StenoGorev> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenografRepo;
        public StenografService(IRepository<StenoPlan> stenoPlanRepo,
                                IRepository<StenoIzin> stenoIzinRepo, IRepository<StenoGorev> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenografRepo,
                                IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
            _stenografRepo = stenografRepo;
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

        public IEnumerable<StenoIzin> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi,Guid? stenografId)
        {
            if(stenografId !=null)
                 return _stenoIzinRepo.Get(x => x.StenografId == stenografId && x.BaslangicTarihi <= basTarihi && x.BitisTarihi >= bitTarihi, includeProperties: "Stenograf");
            else
                return _stenoIzinRepo.Get(x => x.BaslangicTarihi <= basTarihi && x.BitisTarihi >= bitTarihi, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoGorev> GetStenoGorevById(Guid id)
        {
            return _stenoGorevRepo.Get(x=>x.Id == id,includeProperties: "Stenograf");
        }       

        public IEnumerable<StenoGorev> GetStenoGorevByDateAndStatus(DateTime gorevAtamaTarihi, int statu)
        {
            return _stenoGorevRepo.Get(x => x.GörevTarihi == gorevAtamaTarihi && (int)x.GorevStatu == statu, includeProperties: "Stenograf");
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
            return _stenoGorevRepo.Get(x=>x.StenoPlanId == id,includeProperties: "StenoPlan,Stenograf");
        }

        public List<StenoPlan> GetStenoPlanByStatus(int status)
        {
            return _stenoPlanRepo.Get(x => (int)x.GorevStatu == status).ToList();
        }

        public List<StenoGorev> GetStenoGorevBySatatus(int status)
        {
            return _stenoGorevRepo.Get(x => (int)x.GorevStatu == status, includeProperties: "Stenograf").ToList();
        }

        public IEnumerable<Stenograf> GetAllStenograf(Guid? groupId)
        {
            if (groupId != null)
                return _stenografRepo.Get(x => x.GrupId == groupId);
            else
                return _stenografRepo.GetAll();
        }

        public IEnumerable<Stenograf> GetStenoGorevByTur(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
        }

        public IEnumerable<StenoGorev> GetStenoGorevByName(string adSoyad)
        {
            return _stenoGorevRepo.Get(x=>x.Stenograf.AdSoyad == adSoyad,includeProperties: "Stenograf");
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
      
        public void UpdateStenoGorev(StenoGorev stenoGorev)
        {
            //_stenoGrupRepo.Create(entity, CurrentUser.Id);
            //_stenoGrupRepo.Save();
        }

        public IEnumerable<StenoGorev> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevAtamaTarihi)
        {
            return _stenoGorevRepo.Get(x=>x.StenografId == stenografId && x.GörevTarihi == gorevAtamaTarihi, includeProperties: "Stenograf");
        }

        public IEnumerable<StenoGorev> GetStenoGorevByGorevTuruAndDate(Guid stenografId, DateTime gorevAtamaTarihi)
        {
            return _stenoGorevRepo.Get(x => x.StenografId == stenografId && x.GörevTarihi == gorevAtamaTarihi);
        }

        public IEnumerable<StenoGorev> GetStenoGorevByGrupId(Guid id)
        {
            return _stenoGorevRepo.Get(x=>x.Stenograf.Grup.Id == id,  includeProperties: "Stenograf.Grup");
        }
        public IEnumerable<StenoGorev> GetStenoGorevByPlanDateAndStatus(DateTime gorevTarihi,int gorevturu)
        {
            return _stenoGorevRepo.Get(x => (int)x.StenoPlan.GorevTuru == gorevturu &&  x.StenoPlan.BaslangicTarihi <= gorevTarihi && x.StenoPlan.BitisTarihi >= gorevTarihi, includeProperties: "StenoPlan,Stenograf");
        }
    }
}
