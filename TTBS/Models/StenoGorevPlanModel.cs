using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevPlanModel
    {
        public Guid Id { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
        public Guid StenografId { get; set; }
        public string AdSoyad { get; set; }
        public Guid StenoPlanId { get; set; }
        public string ToplanmaYeri { get; set; }
        public string ToplanmaAd { get; set; }
        public ToplanmaTuru ToplanmaTuru { get; set; }
    }
}
