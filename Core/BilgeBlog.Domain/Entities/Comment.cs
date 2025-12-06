using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeBlog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Message { get; set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Replies { get; set; }
    }
}
