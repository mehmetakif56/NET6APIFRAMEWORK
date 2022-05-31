using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IReportService
    {
        
    }
    public class ReportService : BaseService, IReportService
    {
        private IRepository<StenoPlan> _stenoPlanRepo;

        private IUnitOfWork _unitWork;
        public ReportService(IRepository<StenoPlan> stenoPlanRepo,
                             IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _stenoPlanRepo = stenoPlanRepo;
        }
       
       
    }
}
