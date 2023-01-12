using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnShop.Domain.Basket.Entities;
using OnShop.Domain.Common.Entity;
using OnShop.Domain.Orders.Entities;
using OnShop.Domain.Payments.Entities;
using OnShop.Domain.Product.Entities;
using OnShop.Domain.User.Entities;
using OnShop.Domain.Zone.Entities;
using OnShop.Domain.Wallet.Entities;
using OnShop.Domain.Blogs.Entities;
using OnShop.Domain.Chats.Entities;
using OnShop.Domain.Stores.Entities;

namespace OnShop.DAL.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        #region Users
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Domain.Zone.Entities.Zone> Zones { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Domain.Wallet.Entities.Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }


        #endregion

        #region Product
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Product.Entities.Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTechnical> ProductTechnicals { get; set; }
        public DbSet<Store> Stores { get; set; }

        #endregion

        #region Site
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Domain.Slider.Entities.Slider> Sliders { get; set; }
        public DbSet<Domain.Notifications.Entities.Notification> Notifications { get; set; }
        #endregion

        #region Blog
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        #endregion

        #region Messaging
        public DbSet<MessageCategory> MessageCategories { get; set; }
        public DbSet<Message> Messages { get; set; }
        #endregion

        #region Basket
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            ApplyQueryFilter(builder);

            #region DBO 

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");

            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });
            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");

            });


            #endregion

            #region Products

            builder.Entity<Brand>().ToTable(nameof(Brands), "pr");
            builder.Entity<Category>().ToTable(nameof(Categories), "pr");
            builder.Entity<Domain.Product.Entities.Product>().ToTable(nameof(Products), "pr");
            builder.Entity<ProductFeature>().ToTable(nameof(ProductFeatures), "pr");
            builder.Entity<ProductImage>().ToTable(nameof(ProductImages), "pr");
            builder.Entity<ProductTechnical>().ToTable(nameof(ProductTechnicals), "pr");

            #endregion

            #region Basket

            builder.Entity<Cart>().ToTable(nameof(Carts), "cd");
            builder.Entity<CartItem>().ToTable(nameof(CartItems), "cd");
            builder.Entity<Order>().ToTable(nameof(Orders), "cd");
            builder.Entity<OrderDetail>().ToTable(nameof(OrderDetails), "cd");
            builder.Entity<Payment>().ToTable(nameof(Payments), "cd");


            #endregion

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = 2,
                Name = "Bloger",
                NormalizedName = "BLOGER"
            });
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 1,
                UserName = "ادمین",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = string.Empty,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                PhoneNumberConfirmed = true,
                PhoneNumber = "09120001234",
                RegisteredDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                StoreId = 1
            });
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 2,
                UserName = "تست",
                NormalizedUserName = "TEST",
                Email = "test@test.com",
                NormalizedEmail = "TEST@TEST.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = string.Empty,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                PhoneNumberConfirmed = true,
                PhoneNumber = "09190001234",
                RegisteredDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                StoreId = 2
            });
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 3,
                UserName = "خریدار",
                NormalizedUserName = "USER",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123"),
                SecurityStamp = string.Empty,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                PhoneNumberConfirmed = true,
                PhoneNumber = "09110001234",
                RegisteredDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            });
            builder.Entity<Store>().HasData(new Store()
            {
                Id = 1,
                CreatorUserId = 1,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsRemoved = false,
                StoreType = 1,
                MembershipType = 1,
                PhoneNumber = "091212345678",
                Title = "شوش بازار"
            });
            builder.Entity<Store>().HasData(new Store()
            {
                Id = 2,
                CreatorUserId =2,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsRemoved = false,
                StoreType = 1,
                MembershipType = 1,
                PhoneNumber = "09121234567",
                Address= "ایران - تهران - شوش - پاساژ میلاد",
                Title = "فروشگاه تستی"
            });
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 1
            });

        }

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Product.Entities.Product>().HasQueryFilter(x => !x.IsRemoved)
                .HasQueryFilter(x => x.Displayed);

            modelBuilder.Entity<CartItem>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<UserAddress>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<PostCategory>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<Post>().HasQueryFilter(x => !x.IsRemoved);
            modelBuilder.Entity<Domain.Slider.Entities.Slider>().HasQueryFilter(x => !x.IsRemoved);
        }

    }
}