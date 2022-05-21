using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevPlanModel
    {
        public Guid Id { get; set; }
        public DateTime? GörevTarihi { get; set; }
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public string AdSoyad { get; set; }
        public Guid StenoPlanId { get; set; }
        public string GorevYeri { get; set; }
        public string GorevAd { get; set; }
    }
}
