using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnShop.ApplicationServices.Services.Interface;
using OnShop.ApplicationServices.Specifications.Products;
using OnShop.Domain.DTOs.Site.Common;
using OnShop.Domain.Product.Repositories.Categories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;

namespace OnShop.ApplicationServices.Services
{
    public class MenuService : IMenuService
    {
        private readonly ICategoryCommandRepository _repository;
        private readonly IResourceManager _resourceManager;
        public MenuService(ICategoryCommandRepository repository, IResourceManager resourceManager)
        {
            _repository = repository;
            _resourceManager = resourceManager;
        }


        public ResultDto<List<MenuDto>> GetMenu()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResultDto<IReadOnlyList<MenuDto>>> GetMenuAsync()
        {
            var list = await _repository.ListAsync(new CategorySpecification());
            var res = new ResultDto<IReadOnlyList<MenuDto>>(_resourceManager);
            var query = list.Select(x => new MenuDto
            {
                Icon = x.Icon,
                Id = x.Id,
                Title = x.Title,
                Child = x.Children.ToList().Select(p => new MenuDto
                {
                    Icon = p.Icon,
                    Id = p.Id,
                    Title = p.Title,
                    Child = x.Children.ToList().Select(s => new MenuDto
                    {
                        Icon = s.Icon,
                        Id = s.Id,
                        Title = p.Title,
                    }).ToList()
                }).ToList()

            }).ToList().AsReadOnly();
            res.Data = query;
            return res;
        }
    }
}