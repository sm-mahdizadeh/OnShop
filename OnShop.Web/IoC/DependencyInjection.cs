using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnShop.ApplicationServices.Arrangements.Command;
using OnShop.ApplicationServices.Arrangements.Queries;
using OnShop.ApplicationServices.Basket.Command;
using OnShop.ApplicationServices.Basket.Queries;
using OnShop.ApplicationServices.Blogs.Commands;
using OnShop.ApplicationServices.Blogs.Queries.Posts;
using OnShop.ApplicationServices.Convertors;
using OnShop.ApplicationServices.Notifications.Commands;
using OnShop.ApplicationServices.Notifications.Queries;
using OnShop.ApplicationServices.Orders.Command;
using OnShop.ApplicationServices.Orders.Queries;
using OnShop.ApplicationServices.Payments.Command;
using OnShop.ApplicationServices.Products.Command.Brands;
using OnShop.ApplicationServices.Products.Command.Categories;
using OnShop.ApplicationServices.Products.Command.Products;
using OnShop.ApplicationServices.Products.Queries.Brands;
using OnShop.ApplicationServices.Products.Queries.Categories;
using OnShop.ApplicationServices.Products.Queries.Products;
using OnShop.ApplicationServices.Services;
using OnShop.ApplicationServices.Services.Interface;
using OnShop.ApplicationServices.Slider.Command;
using OnShop.ApplicationServices.Slider.Queries;
using OnShop.ApplicationServices.Stores.Command;
using OnShop.ApplicationServices.Stores.Queries;
using OnShop.ApplicationServices.User.Command;
using OnShop.ApplicationServices.User.Queries;
using OnShop.ApplicationServices.User.Queries.UserAdress;
using OnShop.Core.Convertors.Interfaces;
using OnShop.Core.Services;
using OnShop.DAL.Arrangements.Repositories;
using OnShop.DAL.Basket.Repositories;
using OnShop.DAL.Blogs.Repositories;
using OnShop.DAL.Context;
using OnShop.DAL.Context.UOW;
using OnShop.DAL.Orders.Repositories;
using OnShop.DAL.Payments.Repositories;
using OnShop.DAL.Product.Repositories.Brands;
using OnShop.DAL.Product.Repositories.Categories;
using OnShop.DAL.Product.Repositories.Products;
using OnShop.DAL.Slider.Repositories;
using OnShop.DAL.Stores.Repositories;
using OnShop.DAL.User.Repositories;
using OnShop.Domain.Arrangements.Commands;
using OnShop.Domain.Arrangements.Dtos;
using OnShop.Domain.Arrangements.Queries;
using OnShop.Domain.Arrangements.Repositories;
using OnShop.Domain.Basket.Commands;
using OnShop.Domain.Basket.Dtos;
using OnShop.Domain.Basket.Queries;
using OnShop.Domain.Basket.Repositories;
using OnShop.Domain.Blogs.Commands.PostCategories;
using OnShop.Domain.Blogs.Commands.Posts;
using OnShop.Domain.Blogs.Dtos;
using OnShop.Domain.Blogs.Queries.PostCategories;
using OnShop.Domain.Blogs.Queries.Posts;
using OnShop.Domain.Blogs.Repositories;
using OnShop.Domain.DTOs;
using OnShop.Domain.DTOs.Site.Products;
using OnShop.Domain.Notifications.Commands;
using OnShop.Domain.Notifications.Dtos;
using OnShop.Domain.Notifications.Queries;
using OnShop.Domain.Notifications.Repositories;
using OnShop.Domain.Orders.Commands;
using OnShop.Domain.Orders.Dtos;
using OnShop.Domain.Orders.Queries;
using OnShop.Domain.Orders.Repositories;
using OnShop.Domain.Payments.Commands;
using OnShop.Domain.Payments.Repositories;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Domain.Product.Commands.Categories;
using OnShop.Domain.Product.Commands.Products;
using OnShop.Domain.Product.Dtos.Brands;
using OnShop.Domain.Product.Dtos.Categories;
using OnShop.Domain.Product.Queries.Brands;
using OnShop.Domain.Product.Queries.Categories;
using OnShop.Domain.Product.Queries.Products;
using OnShop.Domain.Product.Repositories.Brands;
using OnShop.Domain.Product.Repositories.Categories;
using OnShop.Domain.Product.Repositories.Products;
using OnShop.Domain.SeedWork;
using OnShop.Domain.Slider.Commands;
using OnShop.Domain.Slider.Dtos;
using OnShop.Domain.Slider.Queries;
using OnShop.Domain.Slider.Repositories;
using OnShop.Domain.Stores.Commands;
using OnShop.Domain.Stores.Dtos;
using OnShop.Domain.Stores.Queries;
using OnShop.Domain.Stores.Repositories;
using OnShop.Domain.User.Commands;
using OnShop.Domain.User.Dtos;
using OnShop.Domain.User.Dtos.UserAddresses;
using OnShop.Domain.User.Entities;
using OnShop.Domain.User.Queries;
using OnShop.Domain.User.Repositories;
using OnShop.Framework.Commands;
using OnShop.Framework.Common.File;
using OnShop.Framework.Common.Interfaces;
using OnShop.Framework.Dtos;
using OnShop.Framework.Queries;
using OnShop.Framework.Resources;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;
using System;
using System.Collections.Generic;
using OnShop.Domain.Product.Dtos.Product;

namespace OnShop.Web.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIoc(this IServiceCollection services,
            IConfiguration configuration)
        {
            var mailAddress = configuration.GetValue<string>("EmailInfo:MailAddress");
            var mailPassword = configuration.GetValue<string>("EmailInfo:MailPassword");

            services.AddTransient<CommandDispatcher>();
            services.AddTransient<QueryDispatcher>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                    builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));


            services.AddTransient<IEmailService>(provider => new EmailService(mailAddress, mailPassword));

            services.AddTransient<IResourceManager, ResourceManager<SharedResource>>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IFileHandler, FileHandler>();
            services.AddTransient<IMenuService, MenuService>();

            #region Repository

            services.AddTransient<IApplicationUserInfoCommandRepository, ApplicationUserInfoCommandRepository>();
            services.AddTransient<IBrandsCommandRepository, BrandsCommandRepository>();
            services.AddTransient<ICategoryCommandRepository, CategoryCommandRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ISliderRepository, SliderRepository>();
            services.AddTransient<IArrangementRepository, ArrangementsRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IUserAddressRepository, UserAddressRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostCategoryRepository, PostCategoryRepository>();
            #endregion

            #region MediatR

            #region User
            services.AddTransient<IRequestHandler<AddApplicationUserInfoCommand, ResultDto>, UserInfoCommandHandler>();
            services.AddTransient<IRequestHandler<GetApplicationUserInfoQuery, ApplicationUserInfoDto>, UserInfoQueryHandler>();
            services.AddTransient<IRequestHandler<GetUserAddressQueries, List<UserAddressDto>>, UserAddressQueryHandler>();

            services.AddTransient<IRequestHandler<GetZoneQueries, IReadOnlyList<DropDownDto>>, UserAddressQueryHandler>();
            services.AddTransient<IRequestHandler<GetProvinceQueries, IReadOnlyList<DropDownDto>>, UserAddressQueryHandler>();
            services.AddTransient<IRequestHandler<GetDistrictQueries, IReadOnlyList<DropDownDto>>, UserAddressQueryHandler>();

            #endregion

            #region UserAddress
            services.AddScoped<IRequestHandler<AddUserAddressCommand, ResultDto>, UserAddressCommandHandler>();

            services.AddScoped<IRequestHandler<DeleteUserAddressCommand, ResultDto>, UserAddressCommandHandler>();



            #endregion

            #region Slider

            //Command
            services.AddScoped<IRequestHandler<AddSliderCommand, ResultDto>, SliderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateSliderCommand, ResultDto>, SliderCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteSliderCommand, ResultDto>, SliderCommandHandler>();

            //Queries
            services.AddScoped<IRequestHandler<SliderQueries, IReadOnlyList<SliderDto>>, SliderQueryHandler>();
            services.AddScoped<IRequestHandler<SliderByIdQueries, ResultDto<SliderDto>>, SliderQueryHandler>();
            services.AddScoped<IRequestHandler<SliderPaginationQueries, QueryList<SliderDto>>, SliderQueryHandler>();

            #endregion

            #region Arrangement

            //Command
            services.AddScoped<IRequestHandler<AddArrangementCommand, ResultDto>, ArrangementCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteArrangementCommand, ResultDto>, ArrangementCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateArrangementCommand, ResultDto>, ArrangementCommandHandler>();

            //Queries
            services.AddScoped<IRequestHandler<ArrangementsQueries, IReadOnlyList<ArrangementGetDto>>, ArrangementsQueryHandler>();
            services.AddScoped<IRequestHandler<ArrangementByIdQueries, ResultDto<ArrangementGetDto>>, ArrangementsQueryHandler>();
            services.AddScoped<IRequestHandler<ArrangementsPaginationQueries, QueryList<ArrangementGetDto>>, ArrangementsQueryHandler>();


            #endregion

            #region Store

            //Command
            services.AddScoped<IRequestHandler<StoreCreateCommand, ResultDto>, StoreCommandHandler>();
            services.AddScoped<IRequestHandler<StoreUpdateCommand, ResultDto>, StoreCommandHandler>();
            services.AddScoped<IRequestHandler<StoreUpdateCommand, ResultDto>, StoreCommandHandler>();

            //Queries
            services.AddScoped<IRequestHandler<StoreQuery, IReadOnlyList<StoreDto>>, StoreQueryHandler>();
            services.AddScoped<IRequestHandler<StorePaginationQuery, QueryList<StoreDto>>, StoreQueryHandler>();
            services.AddScoped<IRequestHandler<StoreGetByIdQuery, ResultDto<StoreDto>>, StoreQueryHandler>();
            services.AddScoped<IRequestHandler<StoreGetByCodeQuery, ResultDto<StoreDto>>, StoreQueryHandler>();

            #endregion

            #region Products

            #region Brand
            //Command
            services.AddTransient<IRequestHandler<BrandCreateCommand, ResultDto>, AddBrandHandler>();
            services.AddTransient<IRequestHandler<BrandUpdateCommand, ResultDto>, AddBrandHandler>();
            services.AddTransient<IRequestHandler<DeleteBrandByIdCommand, ResultDto>, AddBrandHandler>();

            //Queries
            services.AddTransient<IRequestHandler<GetBrandQueries, IReadOnlyList<GetBrandDto>>, GetBrandQueryHandler>();
            services.AddTransient<IRequestHandler<GetBrandByIdQueries, GetBrandDto>, GetBrandQueryHandler>();

            #endregion

            #region Category
            //Queries
            services.AddTransient<IRequestHandler<GetCategoryQueries, IReadOnlyList<GetCategoryDto>>, CategoriesQueryHandler>();
            services.AddTransient<IRequestHandler<GetCategoryPaginationQueries, QueryList<GetCategoryDto>>, CategoriesQueryHandler>();
            services.AddTransient<IRequestHandler<GetCategoryByIdQueries, ResultDto<GetCategoryDto>>, CategoriesQueryHandler>();

            //Command
            services.AddTransient<IRequestHandler<CategoryCreateCommand, ResultDto>, AddCategoryHandler>();
            services.AddTransient<IRequestHandler<CategoryUpdateCommand, ResultDto>, AddCategoryHandler>();
            services.AddTransient<IRequestHandler<DeleteCategoryByIdCommand, ResultDto>, AddCategoryHandler>();
            #endregion

            #region Product

            //Command
            services.AddTransient<IRequestHandler<ProductsCreateCommand, ResultDto>, AddProductsHandler>();
            services.AddTransient<IRequestHandler<DeleteProductCommand, ResultDto>, AddProductsHandler>();
            services.AddTransient<IRequestHandler<ProductsUpdateCommand, ResultDto>, AddProductsHandler>();

            //Queries
            services.AddTransient<IRequestHandler<GetProductByIdQueries, ResultProductDetailsSiteDto>, ProductsQueryHandler>();
            services.AddTransient<IRequestHandler<GetAdminProductByIdQueries, ProductDto>, ProductsQueryHandler>();
            services.AddTransient<IRequestHandler<GetProductQueries, ResultProductSiteDto>, ProductsQueryHandler>();

            #endregion


            #endregion

            #region Basket

            //Command
            services.AddScoped<IRequestHandler<AddCartCommand, ResultDto>, BasketCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveFromCardCommand, ResultDto>, BasketCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCountCartCommand, ResultDto>, BasketCommandHandler>();

            //Queries
            services.AddScoped<IRequestHandler<GetCardQueries, ResultDto<CartDto>>, BasketQueryHandler>();
            services.AddScoped<IRequestHandler<GetCardShippingQueries, ResultDto<CartPayDto>>, BasketQueryHandler>();
            services.AddScoped<IRequestHandler<GetCountItemQueries, ResultDto<int>>, BasketQueryHandler>();



            #endregion

            #region Notification
            //Command
            services.AddScoped<IRequestHandler<NotificationCreateCommand, ResultDto>, NotificationCommandHandler>();
            services.AddScoped<IRequestHandler<NotificationDeleteCommand, ResultDto>, NotificationCommandHandler>();
            services.AddScoped<IRequestHandler<NotificationClearCommand, ResultDto>, NotificationCommandHandler>();
            services.AddScoped<IRequestHandler<NotificationMarkAsReadCommand, ResultDto>, NotificationCommandHandler>();
            //Queries
            services.AddScoped<IRequestHandler<NotificationListQuery, QueryList<GetNotificationDto>>, NotificationQueryHandler>();
            services.AddScoped<IRequestHandler<NotificationQueries, IReadOnlyList<GetNotificationDto>>, NotificationQueryHandler>();
            services.AddScoped<IRequestHandler<NotificationByIdQueries, ResultDto<GetNotificationDto>>, NotificationQueryHandler>();
            services.AddScoped<IRequestHandler<NotificationUnReadCountQueries, ResultDto<int>>, NotificationQueryHandler>();


            #endregion

            #region Blog
            #region Post
            //Queries
            services.AddTransient<IRequestHandler<PostGetByIdQuery, ResultDto<PostGetQueryDto>>, PostQueryHandler>();
            services.AddTransient<IRequestHandler<PostPaginationQueries, QueryList<PostListQueryDto>>, PostQueryHandler>();

            //Command
            services.AddTransient<IRequestHandler<PostCreateCommand, ResultDto>, PostCommandHandler>();
            services.AddTransient<IRequestHandler<PostUpdateCommand, ResultDto>, PostCommandHandler>();
            services.AddTransient<IRequestHandler<PostDeleteCommand, ResultDto>, PostCommandHandler>();
            #endregion
            #region PostCategory
            //Queries
            services.AddTransient<IRequestHandler<PostCategoryListQuery, QueryList<PostCategoryListQueryDto>>, PostCategoryQueryHandler>();
            services.AddTransient<IRequestHandler<PostCategoryGetByIdQuery, ResultDto<PostCategoryGetQueryDto>>, PostCategoryQueryHandler>();

            //Command
            services.AddTransient<IRequestHandler<PostCategoryCreateCommand, ResultDto>, PostCategoryCommandHandler>();
            services.AddTransient<IRequestHandler<PostCategoryUpdateCommand, ResultDto>, PostCategoryCommandHandler>();
            services.AddTransient<IRequestHandler<PostCategoryDeleteCommand, ResultDto>, PostCategoryCommandHandler>();
            #endregion
            #endregion

            #region Payment

            services.AddScoped<IRequestHandler<AddPaymentCommand, ResultDto>, PaymentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCartFinished, ResultDto>, OrderCommandHandler>();

            services.AddScoped<IRequestHandler<AddOrderCommand, ResultDto<OrderDto>>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<GetOrderByIdQueries, OrderDto>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrderByUserIdQueries, QueryList<OrderDto>>, OrderQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrderDetailsQueries, QueryList<OrderDetailsDto>>, OrderQueryHandler>();


            #endregion
            //services.AddTransient<CreateUserInfoCommandValidator>();
            services.AddMediatR(typeof(Startup));
            #endregion

            #region Identity

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityError>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(7200);

                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            return services;
        }
    }
}
