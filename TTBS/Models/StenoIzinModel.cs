using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoIzinModel
    {
        public Guid? Id { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public IzınTuru IzinTuru { get; set; }
        public Guid StenografId { get; set; }
        public string StenografAdSoyad { get; set; }
    }
}
