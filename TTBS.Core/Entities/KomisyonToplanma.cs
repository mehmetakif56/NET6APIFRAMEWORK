using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class KomisyonToplanma : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid KomisYonId { get; set; }
        public Guid AltKomisYonId { get; set; } = Guid.Empty;
        public DateTime? PlanlananBaslangicTarihi { get; set; }
        public DateTime? PlanlananBitisTarihi { get; set; }
        public DateTime? GerceklesenBaslangicTarihi { get; set; }
        public DateTime? GerceklesenBitisTarihi { get; set; }
        public int StenoSure { get; set; }
        public int UzmanStenoSure { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public string Yeri { get; set; }
    }
}
