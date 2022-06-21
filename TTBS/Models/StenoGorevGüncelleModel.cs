using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevGüncelleModel
    {
        public Guid Id { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        public Guid StenografId { get; set; }
        public Guid BirlesimId { get; set; }
    }
}
