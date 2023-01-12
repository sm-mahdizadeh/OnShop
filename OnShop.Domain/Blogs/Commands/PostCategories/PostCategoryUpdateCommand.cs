using System;
using System.Collections.Generic;
using MediatR;
using OnShop.Domain.Blogs.Commands.Posts;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Blogs.Commands.PostCategories
{
    public class PostCategoryUpdateCommand :IRequest<ResultDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ModifierUserId { get; set; }
        
    }
}