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
            System.Console.WriteLine("USER ID : " + CurrentUser.Id);
            System.Console.WriteLine(System.DateTime.Today);
            var gorevList = _stenoGorevRepo.Get(x => x.Stenograf.UserId == CurrentUser.Id && x.IsDeleted == false);
            List<GorevAtama> list = new List<GorevAtama>();
            foreach(var gorev in gorevList)
            {
                string[] date = gorev.GorevBasTarihi.ToString().Split(" ");
                if (date[0].Equals(DateTime.Now.ToString("dd/MM/yyyy")))
                {
                    list.Add(gorev);
                }
            }
            return list;
        }
    }
}
