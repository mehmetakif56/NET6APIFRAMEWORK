using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevAtamaModel
    {
        public List<Guid> StenografIds { get; set; }
        public Guid BirlesimId { get; set; }
        public Guid OturumId { get; set; } = Guid.Empty;
        public int TurAdedi { get; set; }

        public int StenoGorevTuru { get; set; }
    }
}
