using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class StenografBeklemeSure : BaseEntity
    {
        public Guid Id { get; set; }   
        public PlanTuru PlanTuru { get; set; }
        public int GorevOnceBeklemeSuresi { get; set; }
        public int GorevSonraBeklemeSuresi { get; set; }
    }
}
