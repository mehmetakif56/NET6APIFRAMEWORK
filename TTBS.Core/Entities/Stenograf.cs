using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class Stenograf : BaseEntity
    {
        public Guid Id { get; set; }   
        public string AdSoyad { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int SiraNo { get; set; }
        public virtual ICollection<StenoGrup> StenoGrups { get; set; }
        public virtual ICollection<StenoGorev> StenoGorevs { get; set; }
    }
}
