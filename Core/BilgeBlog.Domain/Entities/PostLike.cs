using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeBlog.Domain.Entities
{
    public class PostLike : BaseEntity
    {
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }

}
