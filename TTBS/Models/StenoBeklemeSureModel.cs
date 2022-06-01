using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoBeklemeSureModel
    {
        public Guid Id { get; set; }
        public PlanTuru PlanTuru { get; set; }
        public int GorevOnceBeklemeSuresi { get; set; }
        public int GorevSonraBeklemeSuresi { get; set; }

    }
}
