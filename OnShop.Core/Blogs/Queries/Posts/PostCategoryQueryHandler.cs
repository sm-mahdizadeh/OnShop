using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Blogs;
using OnShop.ApplicationServices.Specifications.Slider;
using OnShop.Domain.Blogs.Entities;
using OnShop.Domain.Blogs.Queries.PostCategories;
using OnShop.Domain.Blogs.Queries.Posts;
using OnShop.Domain.Blogs.Repositories;
using OnShop.Domain.DTOs;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OnShop.Domain.Blogs.Dtos;

namespace OnShop.ApplicationServices.Blogs.Queries.Posts
{
    public class PostCategoryQueryHandler :
          IRequestHandler<PostCategoryListQuery, QueryList<PostCategoryListQueryDto>>,
            IRequestHandler<PostCategoryGetByIdQuery, ResultDto<PostCategoryGetQueryDto>>
    {

        private readonly IPostCategoryRepository _repository;

        private readonly ResultDto<PostCategoryGetQueryDto> _result;
        public PostCategoryQueryHandler(IPostCategoryRepository repository,  IResourceManager resourceManager)
        {
            _repository = repository;
            _result = new ResultDto<PostCategoryGetQueryDto>(resourceManager);
        }

        public async Task<QueryList<PostCategoryListQueryDto>> Handle(PostCategoryListQuery request, CancellationToken cancellationToken)
        {
            var spec = new PostCategorySpecification(request.SearchKey);
            var count = await _repository.CountAsync(spec);
            var res = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            var lst = res.Select(s => new PostCategoryListQueryDto
            {
                Id = s.Id,
                Title = s.Title,
                PostCount = s.Posts?.Count ?? 0
            }) .ToList();
            return new QueryList<PostCategoryListQueryDto> { Data = lst, TotalCount = count };
        }

        public async Task<ResultDto<PostCategoryGetQueryDto>> Handle(PostCategoryGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.FirstOrDefaultAsync(new PostCategorySpecification(request.Id));
            if (result != null)
            {
                _result.Data = new PostCategoryGetQueryDto
                {
                    Id = result.Id,
                    Title = result.Title,
                };
                _result.IsSuccess = true;
            }
            else
            {
                _result.IsSuccess = false;
                _result.AddError(SharedResource.NotFound);
            }
            return _result;
        }
    }
}
