using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
