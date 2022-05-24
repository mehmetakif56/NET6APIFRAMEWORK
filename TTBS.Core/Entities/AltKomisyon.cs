using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class AltKomisyon:BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Kodu { get; set; }
        public Komisyon Komisyon { get; set; }
        public Guid KomisyonId { get; set; }
    }
}
