using System;
using System.Collections.Generic;
using System.Text;
using TTBS.Core.Entities;
using TTBS.Core.Utilities.Security.Jwt;

namespace TTBS.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
