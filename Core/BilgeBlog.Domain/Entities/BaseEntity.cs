using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeBlog.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
