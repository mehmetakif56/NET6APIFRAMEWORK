using System;
using System.Collections.Generic;
using System.Text;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class UserOperationClaim : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

    }
}
