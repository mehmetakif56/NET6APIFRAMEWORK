using TTBS.Core.Entities;

namespace TTBS.Models
{
    public class StenoToplamGenelSureModel
    {
        public Guid? Id { get; set; }
        public Guid BirlesimId { get; set; }
        public Guid YasamaId { get; set; }
        public Guid GroupId { get; set; }
        public Guid StenoId { get; set; }
        public DateTime? Tarih { get; set; }
        public double Sure { get; set; }
    }
}
