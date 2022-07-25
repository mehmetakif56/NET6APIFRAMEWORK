using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class GidenGrup:BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public DateTime? GidenGrupTarihi { get; set; }
        public GidenGrupDurumu GidenGrupDurumu { get; set; } = GidenGrupDurumu.Hayır;
        public Guid GrupId  { get; set; }
        public Grup Grup { get; set; }
    }
}
