using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class ClaimEntity : BaseEntity
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual ICollection<RoleClaimEntity> RoleClaims { get; set; }
    }
}
