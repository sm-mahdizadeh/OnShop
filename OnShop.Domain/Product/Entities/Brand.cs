using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnShop.Domain.Interfaces;
using OnShop.Framework.Domain;

namespace OnShop.Domain.Product.Entities
{
    public class Brand : BaseHcEntity<int>, IAggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; }

        public ICollection<Product> Products { get; set; }
        public string Src { get; set; }
    }
}