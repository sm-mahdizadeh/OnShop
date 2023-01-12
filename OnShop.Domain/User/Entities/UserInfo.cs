using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OnShop.Domain.Interfaces;
using OnShop.Framework.Domain;

namespace OnShop.Domain.User.Entities
{
    public class UserInfo : BaseEntity<long>, IAggregateRoot
    {
        public int ApplicationUserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? Birthdate { get; set; }

        public bool? Gender { get; set; }

        #region Relations
        public virtual ApplicationUser ApplicationUser { get; set; }
        #endregion
    }
}