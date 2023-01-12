using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnShop.Domain.Blogs.Entities
{
    public class PostCategory : BaseUserEntity<int>, IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
