using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevModel
    {
        public DateTime? GörevTarihi { get; set; }
        public DateTime? GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public string AdSoyad { get; set; }
        public Guid  PlanId { get; set; }
        public StenoPlanModel StenoPlan { get; set; }
    }
}
