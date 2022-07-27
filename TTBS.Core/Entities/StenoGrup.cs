using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class StenoGrup : BaseEntity
    {
        public Guid StenoId { get; set; }
        public Guid GrupId { get; set; }
        public Stenograf Stenograf { get; set; }
        public Grup Grup { get; set; }
        public DurumStatu GidenGrupMu { get; set; } = DurumStatu.Hayır;
    }
}
