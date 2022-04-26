using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IStenografService
    {
        IEnumerable<StenoPlan> GetStenoPlan();
        void CreateStenoPlan(StenoPlan stenoPlan);

    }
    public class StenografService : BaseService, IStenografService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;
        private IUnitOfWork _unitWork;
        public StenografService(IRepository<StenoPlan> stenoPlanRepo, IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
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

    }
}
