using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class Grup :BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public virtual ICollection<StenoGrup> StenoGrups { get; set; }
        public StenoGorevTuru StenoGrupTuru { get; set; }
    }
}
