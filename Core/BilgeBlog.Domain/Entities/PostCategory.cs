using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeBlog.Domain.Entities
{
    public class PostCategory : BaseEntity
    {
        public Guid PostId { get; set; }
        public virtual Post? Post { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
