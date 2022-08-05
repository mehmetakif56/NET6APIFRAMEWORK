using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class BirlesimKomisyon : BaseEntity
    {
        public Guid KomisyonId { get; set; }
        public Guid? AltKomisyonId { get; set; }
        public Guid BirlesimId { get; set; }
        public Komisyon Komisyon { get; set; }
        public Birlesim Birlesim { get; set; }
    }
}
