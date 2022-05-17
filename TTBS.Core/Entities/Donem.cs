using System;
using System.Collections.Generic;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Donem: BaseEntity
    {
        public Guid Id { get; set; }
        public string KısaAd { get; set; }
        public string EskiAd { get; set; }
        public string MeclisKod { get; set; }
        public string DonemKod { get; set; }
        public string DonemAd { get; set; }
        public int UyeTamsayi { get; set; }
        public int MevcutUye { get; set; }
        public DateTime? DonemSecTarihi { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public virtual ICollection<Yasama> Yasamas { get; set; }
    }
}
