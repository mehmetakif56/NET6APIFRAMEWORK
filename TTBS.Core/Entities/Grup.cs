using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Grup :BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public virtual ICollection<StenoGrup> StenoGrups { get; set; }
    }
}
