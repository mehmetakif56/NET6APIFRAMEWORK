using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class RoleClaimEntity : BaseEntity
    {
        public Guid ClaimId { get; set; }
        public Guid RoleId { get; set; }

        //public virtual RoleEntity Role { get; set; }
        public virtual ClaimEntity Claim { get; set; }
    }
}
