using System;
using System.Collections.Generic;
using System.Text;
using TTBS.Core.Entities;
using TTBS.Core.Utilities.Security.Encyption;
using TTBS.Core.Utilities.Security.Jwt;
using TTBS.Models;
using TTBS.Services;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private static List<User> _users = new List<User>()
        {
            new User(){FirstName="tutanak1", LastName="123456",Email="dummymail",},
            new User(){FirstName="tutanak2", LastName="987654",Email="dummymail",}
        };
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public User Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            //_userService.Add(user);
            return user;
        }

        public User Login(UserForLoginDto userForLoginDto)
        {/*
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck,Messages.SuccessfulLogin);
            */

            User accessedUser;
            if (userForLoginDto != null)
            {
                accessedUser = _users.FirstOrDefault(x => (!String.IsNullOrWhiteSpace(userForLoginDto.UserName) && x.FirstName.ToLower().Trim().Equals(userForLoginDto.UserName.ToLower().Trim()))
                && (!String.IsNullOrWhiteSpace(userForLoginDto.Password) && x.LastName.ToLower().Trim().Equals(userForLoginDto.Password.ToLower().Trim()))
                );

                if (accessedUser == null)
                {
                    throw new Exception("Kullanıcı tanımlaması bulunmamaktadır.");
                }

            }
            else
            {
                throw new Exception("Kullanıcı tanımlaması bulunmamaktadır.");
            }


            return accessedUser;
        }

        public bool UserExists(string email)
        {

            return true;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = new List<OperationClaim>();//_userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return accessToken;
        }
    }
}
