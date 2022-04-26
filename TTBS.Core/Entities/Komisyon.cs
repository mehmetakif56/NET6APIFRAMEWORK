using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class Komisyon:BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Kodu { get; set; }
        public DateTime? Tarih { get; set; }
        public string Yeri { get; set; }
        public KomisyonTipi KomisyonTipi { get; set; }
        public virtual ICollection<StenoPlan> StenoPlans { get; set; }
    }
}
