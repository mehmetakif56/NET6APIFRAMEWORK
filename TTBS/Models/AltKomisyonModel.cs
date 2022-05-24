using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class AltKomisyonModel
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Kodu { get; set; }
        public Guid KomisyonId { get; set; }
    }
}
