using System.Collections.Generic;
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
