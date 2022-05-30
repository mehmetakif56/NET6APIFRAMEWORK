using TTBS.Core.Entities;
using TTBS.Core.Interfaces;
using System.Linq;


namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoPlan> GetStenoPlan();
        void CreateStenoPlan(StenoPlan stenoPlan);
        void DeleteStenoPlan(Guid id);
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
        IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId);
        void CreateStenograf(Stenograf stenograf);
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru);
        void DeleteStenoGorev(Guid stenoGorevId);
        IEnumerable<StenoGorev> GetStenoGorevByGrupId(Guid id);
        IEnumerable<StenoGorev> GetStenoGorevByPlanDateAndStatus(DateTime gorevTarihi, int gorevturu);
        void CreateStenoGroup(StenoGrup stenograf);
        void DeleteStenoGroup(StenoGrup stenograf);
        IEnumerable<Stenograf> GetAllStenoGrupNotInclueded();
        List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int stenografId);
        void UpdateStenoPlan(StenoPlan plan);

        IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId);

        IEnumerable<Stenograf> GetAssignedStenoByPlanIdAndGrorevTur(Guid planId, int gorevturu);

        void UpdateStenoSiraNo(List<Stenograf> steno);
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
        public void DeleteStenoPlan(Guid id)
        {
            _stenoPlanRepo.Delete(id);
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
                 return _stenoIzinRepo.Get(x => x.StenografId == stenografId && x.BaslangicTarihi >= basTarihi && x.BitisTarihi <= bitTarihi, includeProperties: "Stenograf");
            else
                return _stenoIzinRepo.Get(x => x.BaslangicTarihi >= basTarihi && x.BitisTarihi <= bitTarihi, includeProperties: "Stenograf");
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

        public IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId)
        {
            if (groupId != null)
                return _stenoGrupRepo.Get(x => x.GrupId == groupId, includeProperties: "Stenograf").Select(x=>x.Stenograf);
            else
                return _stenoGrupRepo.Get(includeProperties: "Stenograf").Select(x => x.Stenograf);
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
      
        public void DeleteStenoGorev(Guid stenoGorevId)
        {
            _stenoGorevRepo.Delete(stenoGorevId);
            _stenoGorevRepo.Save();
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
            return _stenoGrupRepo.Get(x=>x.GrupId == id,  includeProperties: "Stenograf.StenoGorevs").SelectMany(x=>x.Stenograf.StenoGorevs);
        }
        public IEnumerable<StenoGorev> GetStenoGorevByPlanDateAndStatus(DateTime gorevTarihi,int gorevturu)
        {
            return _stenoGorevRepo.Get(x => (int)x.StenoPlan.GorevTuru == gorevturu &&  x.StenoPlan.BaslangicTarihi <= gorevTarihi && x.StenoPlan.BitisTarihi >= gorevTarihi, includeProperties: "StenoPlan,Stenograf");
        }
        
        public void CreateStenoGroup(StenoGrup entity)
        {
            _stenoGrupRepo.Create(entity, CurrentUser.Id);
            _stenoGrupRepo.Save();
        }
        public IEnumerable<Stenograf> GetAllStenoGrupNotInclueded()
        {
            return _stenografRepo.Get(x => !x.StenoGrups.Select(x=>x.StenoId).Contains(x.Id));
        }
        public void DeleteStenoGroup(StenoGrup entity)
        {
            _stenoGrupRepo.Delete(entity);
            _stenoGrupRepo.Save();
        }

        public List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int gorevTuru)
        {
            var list = _stenoGorevRepo.Get(x =>x.StenoPlan.BaslangicTarihi <=basTarihi && x.StenoPlan.BitisTarihi >= bitTarihi,includeProperties: "StenoPlan").Select(x=>x.StenografId);
            var allList=  new List<Stenograf>();
            var stList = _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru);
            foreach (var st in stList)
            {
                if(!list.Contains(st.Id))
                    allList.Add(st);
            }
            // _stenografRepo.Get(includeProperties: "StenoGorevs").Where(x => x.StenoGorevs.Select(x => x.StenografId).Contains(x.Id)).Select(x => x.StenoGorevs).DefaultIfEmpty().Select(s => new Stenograf { AdSoyad = s.First().Stenograf.AdSoyad }).ToList();

            return allList;

        }

        public IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId)
        {
            var grpList = _stenoGorevRepo.Get(x => x.StenoPlan.BaslangicTarihi <= basTarihi && x.StenoPlan.BitisTarihi >= bitTarihi , includeProperties: "StenoPlan,Stenograf.StenoGrups")
                                         .Where(x=>x.Stenograf.StenoGrups.Select(x=>x.GrupId).Contains(groupId));
            var allList = new List<Stenograf>();
            var stList = _stenografRepo.Get(includeProperties: "StenoGrups");
            foreach (var st in stList)
            {
                var stId = grpList.Select(x => x.StenografId).ToList();
                if (!stId.Contains(st.Id))
                    allList.Add(st);
            }
            return allList;
        }

        public void UpdateStenoPlan(StenoPlan plan)
        {
            plan.KomisyonId = plan.KomisyonId == Guid.Empty ? null : plan.KomisyonId;
            _stenoPlanRepo.Update(plan,CurrentUser.Id);
            _stenoPlanRepo.Save();
        }

        public IEnumerable<Stenograf> GetAssignedStenoByPlanIdAndGrorevTur(Guid planId, int gorevturu)
        {
            return _stenoGorevRepo.Get(x => x.StenoPlanId == planId && (int)x.Stenograf.StenoGorevTuru == gorevturu, includeProperties: "Stenograf").Select(x => x.Stenograf);
        }

        public void UpdateStenoSiraNo(List<Stenograf> stenoList)
        {
            foreach (var steno in stenoList)
            {
                _stenografRepo.Update(steno, CurrentUser.Id);
                _stenografRepo.Save();
            }
           
        }

    }
}
