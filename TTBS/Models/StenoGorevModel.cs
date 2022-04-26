using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevModel
    {
        public DateTime? GörevTarihi { get; set; }
        public DateTime? GorevSaati { get; set; }
        public int GorevSuresi { get; set; }
        public string AdSoyad { get; set; }

    }
}
