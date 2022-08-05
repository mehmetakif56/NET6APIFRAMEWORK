using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class BirlesimOzelToplanma : BaseEntity
    {
        public Guid OzelToplanmaId { get; set; }
        public Guid BirlesimId { get; set; }
        public OzelToplanma OzelToplanma { get; set; }
        public Birlesim Birlesim { get; set; }
    }
}
