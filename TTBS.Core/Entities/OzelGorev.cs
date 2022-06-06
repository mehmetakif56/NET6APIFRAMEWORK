using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class OzelGorev : BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Kodu { get; set; }
        public DateTime? Tarih { get; set; }
        public string Yeri { get; set; }
        public OzelGorevTur OzelGorevTur { get; set; }
        public Guid OzelGorevTurId { get; set; }
    }
}
