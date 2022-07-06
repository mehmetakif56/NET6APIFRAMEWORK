using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IDashboardService
    {
        IEnumerable<GorevAtama> GetActiveGorevler();
    }
    public class DashboardService : BaseService, IDashboardService
    {
        private IRepository<GorevAtama> _stenoGorevRepo;
        public DashboardService(IServiceProvider provider, IRepository<GorevAtama> stenoGorevRepo) : base(provider)
        {
            _stenoGorevRepo = stenoGorevRepo;
        }

        public IEnumerable<GorevAtama> GetActiveGorevler()
        {
            return _stenoGorevRepo.Get(x => x.Stenograf.UserId == CurrentUser.Id && x.GorevBasTarihi >= DateTime.Now.Date && x.GorevBasTarihi < DateTime.Now.AddDays(1).Date);
        }
    }
}
