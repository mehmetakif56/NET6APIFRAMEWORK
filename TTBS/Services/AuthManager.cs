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
    public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public User Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return  new SuccessDataResult<User>(user,Messages.UserRegistered);
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
            return new User();
        }

        public bool UserExists(string email)
        {

            return true;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return accessToken;
        }
    }
}
