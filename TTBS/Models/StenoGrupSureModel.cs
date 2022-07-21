using TTBS.Core.Entities;

namespace TTBS.Models
{
    public class StenoGrupSureModel
    {
        public IEnumerable<StenoModel> stenos { get; set; }
        public string komisyonAd { get; set; }
        public DateTime date { get; set; }

        public IEnumerable<StenoGorevModel> stenoGorevs { get; set; }

        public int[,] tablo { get; set; }

        public int[] toplamTablo { get; set; }
    }
}
