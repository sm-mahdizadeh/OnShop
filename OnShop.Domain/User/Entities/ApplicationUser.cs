using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using OnShop.Domain.Basket.Entities;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.Stores.Entities;

namespace OnShop.Domain.User.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        //[Column(TypeName = "DateTime")]
        public DateTime? RegisteredDate { get; set; }
        //[Column(TypeName = "DateTime")]
        public DateTime? ModifiedDate { get; set; }

        public long? StoreId { get; set; }

        #region Relations
        public virtual Store Store { get; set; }

        public virtual List<Wallet.Entities.Wallet> Wallets { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public ICollection<UserAddress> UserAddress { get; set; }
        public ICollection<ProductFeature> ProductFeatures { get; set; }
        public ICollection<Product.Entities.Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<ProductTechnical> ProductTechnicals { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Slider.Entities.Slider> Sliders { get; set; }

        #endregion

    }
}