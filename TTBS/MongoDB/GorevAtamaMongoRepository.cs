using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TTBS.MongoDB
{
    public class GorevAtamaMongoRepository : MongoDbRepositoryBase<GorevAtamaGKMongo>, IGorevAtamaMongoRepository
    {
        public GorevAtamaMongoRepository(IOptions<MongoDbSettings> options) : base(options)
        {

        }
    }
}
