using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IDonemService
    {
        DonemEntity GetDonem();
    }
    public class DonemService : BaseService, IDonemService
    {
        private IRepository<DonemEntity> _donemRepository;
        private IUnitOfWork _unitWork;
        public DonemService(IRepository<DonemEntity> donemRepository, IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _donemRepository = donemRepository;
            _unitWork = unitWork;
        }
        public DonemEntity GetDonem()
        {
            return _donemRepository.GetOne();
        }
    }
}
