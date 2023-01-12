using System;

namespace OnShop.Framework.Commands
{
    public class BaseCommandEntity
    {

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public int? CreatorUserId { get; set; }
        public int? ModifiedId { get; set; }

        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
    }
}