using System.Collections.Generic;
using MediatR;
using OnShop.Domain.Blogs.Commands.Posts;
using OnShop.Domain.Product.Commands.Brands;
using OnShop.Framework.Commands;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Product.Commands.Products
{
    public class PostUpdateCommand : PostCreateCommand, IRequest<ResultDto>
    {
        public long Id { get; set; }
        public int ModifierUserId { get; set; }
    }
}