using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class KomisyonToplanmaModel
    {
        public Guid KomisyonId { get; set; }
        public Guid? AltKomisyonId { get; set; } = Guid.Empty;
        public string Yeri { get; set; }
        public decimal StenoSure { get; set; }
        public decimal UzmanStenoSure { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
    }
}
