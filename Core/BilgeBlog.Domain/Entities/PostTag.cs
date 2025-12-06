using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeBlog.Domain.Entities
{
    public class PostTag : BaseEntity
    {
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }

}
