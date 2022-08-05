using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.Enums;
using TTBS.MongoDB;

namespace TTBS.MongoDB
{
    public class GorevAtamaMongoDB : MongoDbEntity
    {
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public GorevStatu GorevStatu { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StenoSure { get; set; }
        public Guid OturumId { get; set; }
        public Guid BirlesimId { get; set; }
    }
}
