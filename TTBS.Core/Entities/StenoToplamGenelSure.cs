using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class StenoToplamGenelSure : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid BirlesimId { get; set; }
        public Birlesim Birlesim { get; set; } 
        public Guid YasamaId { get; set; }
        public Yasama Yasama { get; set; }
        public Guid GroupId { get; set; }
        public Guid StenoId { get; set; }
        public DateTime Tarih { get; set; }
        public double Sure { get; set; }
    }
}
