using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TTBS.MongoDB
{
    public class GorevAtamaGKMBusiness : MongoDbRepositoryBase<GorevAtamaGKM>, IGorevAtamaGKMBusiness
    {
        public GorevAtamaGKMBusiness(IOptions<MongoDbSettings> options) : base(options)
        {

        }
    }
}
