using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevGüncelleModel
    {
        public Guid StenoPlanId { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public DateTime? GörevTarihi { get; set; }
        public int GorevDakika { get; set; }
        public int GorevSaniye { get; set; }
    }
}
