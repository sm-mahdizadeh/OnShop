using System.ComponentModel.DataAnnotations.Schema;
using OnShop.Domain.User.Entities;
using OnShop.Framework.Domain;

namespace OnShop.Domain.Common
{
    public class BaseUserEntity<T> : BaseEntity<T>
    {
        [ForeignKey(nameof(CreatorUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
