using TTBS.Core.Entities;
using TTBS.Core.Interfaces;

namespace TTBS.Services
{
    public interface IUserService
    {
        UserEntity GetUserByUserName(string userName);

        UserEntity GetUserById(Guid? id);
        IEnumerable<UserEntity> GetUserByMailAndPassword(string mail, string passWord);

        IEnumerable<ClaimEntity> GetUserRoleClaims(int[] roleIds);

    }

    public class UserService : BaseService, IUserService
    {
        private IRepository<UserEntity> _userRepository;
        private IUnitOfWork _unitWork;
        private readonly IRepository<ClaimEntity> _claimRepository;
        public UserService(IRepository<UserEntity> userRepository, IUnitOfWork unitWork, IRepository<ClaimEntity> claimRepository,
                             IServiceProvider provider) : base(provider)
        {
            _userRepository = userRepository;
            _unitWork = unitWork;
            _claimRepository = claimRepository;
        }

        public UserEntity GetUserByUserName(string userName)
        {
            return _userRepository.GetOne(x => x.UserName == userName, includeProperties: "UserRoles.Role");
        }

        public UserEntity GetUserById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> GetUserByMailAndPassword(string mail, string passWord)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClaimEntity> GetUserRoleClaims(int[] roleIds)
        {
            try
            {
                var list = new List<ClaimEntity>();
                for (int i = 0; i < roleIds.Length; i++)
                {
                    list.AddRange(_claimRepository.Get(c => c.RoleClaims.Any(rc => rc.Role.RoleStatusId == roleIds[i])).ToList());
                }
                return list.GroupBy(i => i.ClaimValue).Select(grp => grp.First());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
