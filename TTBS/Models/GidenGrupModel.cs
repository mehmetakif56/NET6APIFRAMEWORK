using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class GidenGrupModel
    {
        public Guid? Id { get; set; }
        public string Ad { get; set; }
        public DateTime? GidenGrupTarihi { get; set; }
        public GidenGrupDurumu GidenGrupDurumu { get; set; } = GidenGrupDurumu.Hayır;
        public Guid GrupId { get; set; }
    }
}
