using TTBS.Core.Entities;
using TTBS.Core.Interfaces;
using TTBS.Extensions;

namespace TTBS.Helper
{
    public class SessionHelper : ISessionHelper
    {
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public SessionHelper(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }
        public UserEntity User
        {
            get { return Get<UserEntity>("User"); }
            set { Set("User", value); }
        }

        public string BUILD_VERSION => throw new NotImplementedException();

        public string BUILD_DATE => throw new NotImplementedException();

        public string BasePath => throw new NotImplementedException();

        public bool IsPrivilegedUser()
        {
            throw new NotImplementedException();
        }

        private T Get<T>(string key)
        {
            object o = _contextAccessor.HttpContext.Session.GetObject<T>(key);
            if (o is T)
            {
                return (T)o;
            }

            return default(T);
        }

        /// <summary> Sets. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="key">  The key. </param>
        /// <param name="item"> The item. </param>
        private void Set<T>(string key, T item)
        {
            _contextAccessor.HttpContext.Session.SetObject(key, item);
        }

    }
}
