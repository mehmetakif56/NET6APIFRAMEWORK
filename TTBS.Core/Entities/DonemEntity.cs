using System;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class DonemEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? DonemTarihi { get; set; }
    }
}
