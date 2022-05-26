using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoModel
    {
        public Guid? Id { get; set; }
        public string AdSoyad { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
        public string SiraNo { get; set; }
        public DateTime? SonGorevTarihi { get; set; }
        public int SonGorevSuresi { get; set; }

    }
}
