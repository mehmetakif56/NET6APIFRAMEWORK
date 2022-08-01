using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class BirlesimViewModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public string Yeri { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public string ToplanmaDurumu { get; set; }
        public string KomisyonAdı { get; set; }
        public string AltKomisyonAdı { get; set; }
        public Guid Id { get; set; }
        public Guid OturumId { get; set; }
        public Guid YasamaId { get; set; }
    }
}
