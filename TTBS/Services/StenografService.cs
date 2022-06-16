using TTBS.Core.Entities;
using TTBS.Core.Interfaces;
using System.Linq;
using TTBS.Core.Enums;

namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoIzin> GetAllStenoIzin();
        IEnumerable<StenoIzin> GetStenoIzinByStenografId(Guid id);
        IEnumerable<StenoIzin> GetStenoIzinByName(string adSoyad);
        IEnumerable<StenoIzin> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi,  string? field, string? sortOrder, int? izinTur, Guid? stenografId, int pageIndex, int pagesize);
        IEnumerable<GorevAtama> GetStenoGorevById(Guid id);
        IEnumerable<GorevAtama> GetStenoGorevByName(string adSoyad);
        IEnumerable<GorevAtama> GetStenoGorevByDateAndStatus(DateTime gorevBasTarihi,DateTime gorevBitTarihi, int status);
        //IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi);
        void CreateStenoGorevAtama(GorevAtama stenoGorev);
        void UpdateStenoGorev(List<GorevAtama> stenoGorev);
        void CreateStenoIzin(StenoIzin stenoGorev);
        IEnumerable<GorevAtama> GetStenoGorevByBirlesimId(Guid id);
        //List<StenoPlan> GetStenoPlanByStatus(int status);
        //IEnumerable<StenoPlan> GetStenoPlanByDateAndStatus(DateTime gorevTarihi, DateTime gorevBitTarihi, int gorevTuru);
        List<GorevAtama> GetStenoGorevBySatatus(int status);
        IEnumerable<Stenograf> GetAllStenografByGroupId(Guid? groupId);
        void CreateStenograf(Stenograf stenograf);
        IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru);
        void DeleteStenoGorev(Guid stenoGorevId);
        IEnumerable<GorevAtama> GetStenoGorevByGrupId(Guid id);
        void CreateStenoGroup(StenoGrup stenograf);
        void DeleteStenoGroup(StenoGrup stenograf);
        IEnumerable<Stenograf> GetAllStenoGrupNotInclueded();
        //List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int stenografId, int toplantiTur);
        //void UpdateStenoPlan(StenoPlan plan);
        //IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur);
        //IEnumerable<GorevAtama> GetAssignedStenoByPlanIdAndGrorevTur(Guid planId, int gorevturu);
        //IEnumerable<GorevAtama> GetIntersectStenoPlan(Guid stenoPlanId, Guid stenoId);
        void UpdateStenoSiraNo(List<Stenograf> steno);
        IEnumerable<Grup> GetAllStenografGroup();
    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoIzin> _stenoIzinRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IUnitOfWork _unitWork;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<StenoGrup> _stenoGrupRepo;
        private IRepository<StenografBeklemeSure> _stenoBeklemeSure;
        private IRepository<Grup> _grupRepo;
        public StenografService(IRepository<StenoIzin> stenoIzinRepo, IRepository<GorevAtama> stenoGorevRepo,
                                IUnitOfWork unitWork,
                                IRepository<Stenograf> stenografRepo,
                                IRepository<StenoGrup> stenoGrupRepo,
                                IRepository<StenografBeklemeSure> stenoBeklemeSure,
                                IRepository<Grup> grupRepo,
                                IServiceProvider provider) : base(provider)
        {
            _stenoIzinRepo = stenoIzinRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _unitWork = unitWork;
            _stenografRepo = stenografRepo;
            _stenoGrupRepo = stenoGrupRepo;
            _stenoBeklemeSure = stenoBeklemeSure;
            _grupRepo = grupRepo;
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

        public IEnumerable<StenoIzin> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi, DateTime bitTarihi, string? field, string? sortOrder, int? izinTur, Guid? stenografId, int pageIndex,int pagesize)
        {

            var stenoIzinList = _stenoIzinRepo.Get(x =>  x.BaslangicTarihi >= basTarihi && x.BitisTarihi <= bitTarihi,               
                                                        includeProperties: "Stenograf");
            if (izinTur != null)
                stenoIzinList = stenoIzinList.Where(x => (int)x.IzinTuru == izinTur);
            if (stenografId != null && stenografId !=Guid.Empty)
                stenoIzinList = stenoIzinList.Where(x => x.StenografId == stenografId);

            if (stenoIzinList !=null && stenoIzinList.Count()>0)
            {
                if(!string.IsNullOrEmpty(field))
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
                }              

                stenoIzinList.ToList().ForEach(x => x.StenografCount = stenoIzinList.Count());
            }
            return  stenoIzinList != null && stenoIzinList.Count() > 0 ? stenoIzinList.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList() : new List<StenoIzin> { };
        }

        public IEnumerable<GorevAtama> GetStenoGorevById(Guid id)
        {
            return _stenoGorevRepo.Get(x=>x.Id == id,includeProperties: "Stenograf");
        }       

        public IEnumerable<GorevAtama> GetStenoGorevByDateAndStatus(DateTime gorevBasTarihi, DateTime gorevBitTarihi, int statu)
        {
            return _stenoGorevRepo.Get(x => x.GorevBasTarihi>= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi && (int)x.GorevStatu == statu, includeProperties: "Stenograf");
        }

        public void CreateStenoGorevAtama(GorevAtama entity)
        {
            foreach (var item in entity.StenografIds)
            {
                var newEntity = new GorevAtama();
                newEntity.BirlesimId = entity.BirlesimId;
                newEntity.OturumId = entity.OturumId;
                newEntity.StenografId = item;
                newEntity.GorevStatu = GorevStatu.Planlandı;
                _stenoGorevRepo.Create(newEntity, CurrentUser.Id);
                _stenoGorevRepo.Save();
            }
        }

        public void UpdateStenoGorev(List<GorevAtama> entityList)
        {
            _stenoGorevRepo.Update(entityList, CurrentUser.Id);
            _stenoGorevRepo.Save();
        }

        public void CreateStenoIzin(StenoIzin entity)
        {
            _stenoIzinRepo.Create(entity, CurrentUser.Id);
            _stenoIzinRepo.Save();
        }

        public IEnumerable<GorevAtama> GetStenoGorevByBirlesimId(Guid id)
        {
            return _stenoGorevRepo.Get(x => x.BirlesimId == id, includeProperties: "Stenograf,Oturum,Birlesim");
        }

        //public List<StenoPlan> GetStenoPlanByStatus(int status)
        //{
        //    return _stenoPlanRepo.Get(x => (int)x.GorevStatu == status).ToList();
        //}

        public List<GorevAtama> GetStenoGorevBySatatus(int status)
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

        public IEnumerable<GorevAtama> GetStenoGorevByName(string adSoyad)
        {
            return _stenoGorevRepo.Get(x=>x.Stenograf.AdSoyad == adSoyad,includeProperties: "Stenograf");
        }

        //public IEnumerable<StenoPlan> GetStenoPlanByDateAndStatus(DateTime gorevBasTarihi, DateTime gorevBitTarihi,int gorevTuru)
        //{
        //    return _stenoPlanRepo.Get(x => x.PlanlananBaslangicTarihi >= gorevBasTarihi && x.PlanlananBitisTarihi <= gorevBitTarihi && (int)x.GorevTuru == gorevTuru);
        //}

        public void CreateStenograf(Stenograf entity)
        {
            _stenografRepo.Create(entity, CurrentUser.Id);
            _stenografRepo.Save();            
        }
        public IEnumerable<Stenograf> GetAllStenografByGorevTuru(int gorevTuru)
        {
            return _stenografRepo.Get(x => (int)x.StenoGorevTuru == gorevTuru,includeProperties:"GorevAtamas");
        }
      
        public void DeleteStenoGorev(Guid stenoGorevId)
        {
            _stenoGorevRepo.Delete(stenoGorevId);
            _stenoGorevRepo.Save();
        }

        public IEnumerable<GorevAtama> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            return _stenoGorevRepo.Get(x=>x.StenografId == stenografId && x.GorevBasTarihi >= gorevBasTarihi && x.GorevBitisTarihi <= gorevBitTarihi, includeProperties: "Stenograf");
        }

        public IEnumerable<GorevAtama> GetStenoGorevByGrupId(Guid id)
        {
            return _stenoGrupRepo.Get(x=>x.GrupId == id,  includeProperties: "Stenograf.GorevAtamas").SelectMany(x=>x.Stenograf.GorevAtamas);
        }
        
        public void CreateStenoGroup(StenoGrup entity)
        {
            _stenoGrupRepo.Create(entity, CurrentUser.Id);
            _stenoGrupRepo.Save();
        }
        public IEnumerable<Stenograf> GetAllStenoGrupNotInclueded()
        {
            return _stenografRepo.Get(x =>x.StenoGorevTuru == StenoGorevTuru.Stenograf && !x.StenoGrups.Select(x=>x.StenoId).Contains(x.Id));
        }
        public void DeleteStenoGroup(StenoGrup entity)
        {
            _stenoGrupRepo.Delete(entity);
            _stenoGrupRepo.Save();
        }

        //public List<Stenograf> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi, int stenoGorevTuru,int toplantiTur)
        //{
        //    var lst = _stenoBeklemeSure.Get(x => (int)x.PlanTuru == toplantiTur);
        //    //if(lst !=null && lst.Count()>0)
        //    //{
        //    //    basTarihi = basTarihi.AddMinutes(-lst.FirstOrDefault().GorevOnceBeklemeSuresi);
        //    //    bitTarihi = bitTarihi.AddMinutes(lst.FirstOrDefault().GorevSonraBeklemeSuresi);
        //    //}

        //    var allList=  new List<Stenograf>();
        //    var stList = _stenografRepo.Get(x => (int)x.StenoGorevTuru == stenoGorevTuru);
        //    foreach (var st in stList)
        //    {
        //        var cnt = _stenoGorevRepo.Get(x => (basTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && basTarihi <= x.StenoPlan.PlanlananBitisTarihi) || 
        //                                           (bitTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && bitTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                           (x.StenoPlan.PlanlananBaslangicTarihi>= basTarihi && x.StenoPlan.PlanlananBaslangicTarihi <= bitTarihi) ||
        //                                           (x.StenoPlan.PlanlananBitisTarihi >= basTarihi && x.StenoPlan.PlanlananBitisTarihi <= bitTarihi),
        //                                           includeProperties: "StenoPlan,Stenograf").Where(x=>x.StenografId == st.Id);
        //        st.StenoGorevDurum = cnt!=null && cnt.Count() > 0 ? true : false;
        //      allList.Add(st);
        //    }
        //    return allList;

        //}

        //public IEnumerable<GorevAtama> GetIntersectStenoPlan(Guid stenoPlanId,Guid stenoId)
        //{
        //    var plan = _stenoPlanRepo.GetById(stenoPlanId);
        //    var basTarihi = plan.PlanlananBaslangicTarihi;
        //    var bitTarihi =plan.PlanlananBitisTarihi;

        //    return _stenoGorevRepo.Get(x => (basTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && basTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                           (bitTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && bitTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                           (x.StenoPlan.PlanlananBaslangicTarihi >= basTarihi && x.StenoPlan.PlanlananBaslangicTarihi <= bitTarihi) ||
        //                                           (x.StenoPlan.PlanlananBitisTarihi >= basTarihi && x.StenoPlan.PlanlananBitisTarihi <= bitTarihi),
        //                                           includeProperties: "StenoPlan,Stenograf").Where(x => x.StenografId == stenoId);

        //}

        //public IEnumerable<Stenograf> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur)
        //{
        //    var lst = _stenoBeklemeSure.Get(x => (int)x.PlanTuru == toplantiTur);
        //    //if (lst != null && lst.Count() > 0)
        //    //{
        //    //    basTarihi = basTarihi.AddMinutes(-lst.FirstOrDefault().GorevOnceBeklemeSuresi);
        //    //    bitTarihi = bitTarihi.AddMinutes(lst.FirstOrDefault().GorevSonraBeklemeSuresi);
        //    //}

        //    var allList = new List<Stenograf>();
        //    var stList = _stenografRepo.Get(x => x.StenoGrups.Select(x => x.GrupId).Contains(groupId), includeProperties: "StenoGrups");
        //    foreach (var st in stList)
        //    {
        //        var cnt = _stenoGorevRepo.Get(x => (basTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && basTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                          (bitTarihi >= x.StenoPlan.PlanlananBaslangicTarihi && bitTarihi <= x.StenoPlan.PlanlananBitisTarihi) ||
        //                                          (x.StenoPlan.PlanlananBaslangicTarihi >= basTarihi && x.StenoPlan.PlanlananBaslangicTarihi <= bitTarihi) ||
        //                                          (x.StenoPlan.PlanlananBitisTarihi >= basTarihi && x.StenoPlan.PlanlananBitisTarihi <= bitTarihi),
        //                                          includeProperties: "StenoPlan,Stenograf").Where(x => x.StenografId == st.Id);
        //        st.StenoGorevDurum = cnt != null && cnt.Count() > 0 ? true : false;
        //        allList.Add(st);
        //    }
        //    return allList;
        //}

        //public IEnumerable<GorevAtama> GetAssignedStenoByPlanIdAndGrorevTur(Guid planId, int gorevturu)
        //{
        //    return _stenoGorevRepo.Get(x => x.StenoPlanId == planId && (int)x.Stenograf.StenoGorevTuru == gorevturu, includeProperties: "Stenograf");
        //}

        public void UpdateStenoSiraNo(List<Stenograf> stenoList)
        {
            foreach (var steno in stenoList)
            {
                _stenografRepo.Update(steno, CurrentUser.Id);
                _stenografRepo.Save();
            }
           
        }

        public IEnumerable<Grup> GetAllStenografGroup()
        {
            return _grupRepo.Get(includeProperties: "StenoGrups.Stenograf.GorevAtamas");
        }

       
    }
}
