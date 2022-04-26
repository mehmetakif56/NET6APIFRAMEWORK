using System;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class Donem: BaseEntity
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public DateTime? DonemTarihi { get; set; }
    }
}
