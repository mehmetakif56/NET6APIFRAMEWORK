using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoModel
    {
        public Guid? Id { get; set; }
        public string AdSoyad { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public int SiraNo { get; set; }
        public DateTime? SonGorevTarihi { get; set; }
        public int SonGorevSuresi { get; set; }
        public bool StenoGorevDurum { get; set; }
        public int GorevStatu { get; set; }
    }
}
