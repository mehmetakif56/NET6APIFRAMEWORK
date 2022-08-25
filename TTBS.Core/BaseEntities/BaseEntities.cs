using System;
using System.ComponentModel.DataAnnotations;
using TTBS.Core.Interfaces;

namespace TTBS.Core.BaseEntities
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        //public string Description { get; set; }
        object IEntity.Id { get { return Id; } set { } }

        public T Id { get; set; }

        //private DateTime? createdDate;
        //[DataType(DataType.DateTime)]
        //public DateTime? CreatedDate { get => createdDate ?? DateTime.Now; set => createdDate = value; }

        //[DataType(DataType.DateTime)]
        //public DateTime? ModifiedDate { get; set; }

        //public Guid? CreatedBy { get; set; }
        //public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid> { }
}
