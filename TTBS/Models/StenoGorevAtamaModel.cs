using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevAtamaModel
    {
        public Guid StenografId { get; set; }
        public Guid  PlanId { get; set; }
        public int SiraNo { get; set; }
    }
}
