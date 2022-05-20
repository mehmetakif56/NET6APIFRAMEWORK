using System;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class StenoGrup:BaseEntity
    {
        public Guid StenoId { get; set; }
        public Guid GrupId { get; set; }
        public Stenograf Stenograf { get; set; }
        public Grup Grup { get; set; }
    }
}
