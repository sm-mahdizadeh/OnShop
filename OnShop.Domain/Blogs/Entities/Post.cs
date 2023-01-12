using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;
using OnShop.Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnShop.Domain.Blogs.Entities
{
    public class Post : BaseUserEntity<long>, IAggregateRoot
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Tages { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual PostCategory Category { get; set; }
    }
}
