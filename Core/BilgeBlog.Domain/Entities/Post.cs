using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeBlog.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; } = true;
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostLike> Likes { get; set; }
    }
}
