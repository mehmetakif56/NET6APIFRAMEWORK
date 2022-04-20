using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Helper;

namespace TTBS.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private IMapper _mapper;
        private ILogger<T> _logger;
        private IConfiguration _conf;
        private GenericSharedResourceService _localizer;
        private UserEntity _currentUser;

        protected IConfiguration Configuration => _conf ?? (_conf = HttpContext.RequestServices.GetService<IConfiguration>());
        protected IMapper Mapper => _mapper ?? (_mapper = HttpContext.RequestServices.GetService<IMapper>());
        protected ILogger<T> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<T>>());

        //protected UserEntity CurrentUser => _currentUser ?? (_currentUser = HttpContext.RequestServices.GetService<ISessionHelper>().User);
        protected GenericSharedResourceService L => _localizer ??
               (_localizer = HttpContext.RequestServices.GetService<GenericSharedResourceService>());
        protected string BasePath => Configuration.GetValue<string>("BasePath");

    }
}
