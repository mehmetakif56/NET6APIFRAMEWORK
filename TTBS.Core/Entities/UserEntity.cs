using System;
using TTBS.Core.BaseEntities;

namespace TTBS.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
