using AutoMapper;
using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public abstract class BaseService
    {
        protected UserEntity CurrentUser { get; }
        protected IMapper Mapper { get; }
        protected ILogger<BaseService> Logger { get; }
        protected IConfiguration Configuration { get; }
        protected string BasePath => Configuration.GetValue<string>("BasePath");

        public BaseService(IServiceProvider provider)
        {
            //CurrentUser = provider.GetService<ISessionHelper>().User;
            Mapper = provider.GetService<IMapper>();
            Logger = provider.GetService<ILogger<BaseService>>();
            Configuration = provider.GetService<IConfiguration>();

        }
    }

    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base("AUTHORIZATION_ERROR | " + message)
        {

        }
    }
}
