using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IUserService
    {
        UserEntity GetUserByUserName(string userName);

        UserEntity GetUserById(Guid? id);
        IEnumerable<UserEntity> GetUserByMailAndPassword(string mail, string passWord);

    }

    public class UserService : BaseService, IUserService
    {
        private IRepository<UserEntity> _userRepository;
        private IUnitOfWork _unitWork;
        public UserService(IRepository<UserEntity> userRepository, IUnitOfWork unitWork,
                             IServiceProvider provider) : base(provider)
        {
            _userRepository = userRepository;
            _unitWork = unitWork;
        }

        public UserEntity GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetUserByMailAndPassword(string mail, string passWord)
        {
            throw new NotImplementedException();
        }
    }
}
