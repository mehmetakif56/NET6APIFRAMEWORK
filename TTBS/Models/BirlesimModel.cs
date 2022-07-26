using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimModel
    {
        public Guid? Id { get; set; }
        public string BirlesimNo { get; set; } =String.Empty;
        public DateTime? BaslangicTarihi { get; set; }
        public Guid YasamaId { get; set; }
        public decimal StenoSure { get; set; }
        public decimal UzmanStenoSure { get; set; }
        public Guid KomisyonId { get; set; } = Guid.Empty;
        public Guid? AltKomisyonId { get; set; } = Guid.Empty;
        public Guid? OzelToplanmaId { get; set; } = Guid.Empty;
        public string Yeri { get; set; } = String.Empty;
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public ToplanmaStatu ToplanmaDurumu { get; set; }

    }
}
