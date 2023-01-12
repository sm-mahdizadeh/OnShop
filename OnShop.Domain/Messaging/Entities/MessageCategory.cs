using OnShop.Domain.Common;
using OnShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnShop.Domain.Chats.Entities
{
    public class MessageCategory : BaseUserEntity<int>, IAggregateRoot
    {
        public string Description { get; set; }
        public string Key { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
