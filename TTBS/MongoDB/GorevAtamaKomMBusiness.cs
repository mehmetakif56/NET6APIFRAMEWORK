using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TTBS.MongoDB
{
    public class GorevAtamaKomMBusiness : MongoDbRepositoryBase<GorevAtamaKomM>, IGorevAtamaKomMBusiness
    {
        public GorevAtamaKomMBusiness(IOptions<MongoDbSettings> options) : base(options)
        {

        }
    }
}
