using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGrupViewModel
    {
        public Guid GrupId { get; set; }
        public string GrupName { get; set; }
        public StenoGorevTuru StenoGrupTuru { get; set; }
        public List<StenoViewModel> StenoViews { get; set; }
    }
}
