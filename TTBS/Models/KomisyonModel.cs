using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class KomisyonModel
    {
        public string Ad { get; set; }
        public string Kodu { get; set; }
        public DateTime? Tarih { get; set; }
        public string Yeri { get; set; }
        public KomisyonTipi KomisyonTipi { get; set; }

    }
}
