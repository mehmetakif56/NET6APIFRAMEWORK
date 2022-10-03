using System;
using System.Collections.Generic;
using System.Text;
using TTBS.Core.Entities;
using TTBS.Core.Utilities.Security.Jwt;
using TTBS.Models;

namespace TTBS.Services
{
    public interface IAuthService
    {
        User Register(UserForRegisterDto userForRegisterDto,string password);
        User Login(UserForLoginDto userForLoginDto);
        bool UserExists(string email);
        AccessToken CreateAccessToken(User user);
    }
}
