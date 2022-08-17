using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class GrupDetay:BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? GidenGrupTarihi { get; set; }
        public Guid GrupId  { get; set; }
        public Grup Grup { get; set; }
    }
}
