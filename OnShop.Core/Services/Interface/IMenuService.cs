using System.Collections.Generic;
using System.Threading.Tasks;
using OnShop.Domain.DTOs.Site.Common;
using OnShop.Framework.Dtos;

namespace OnShop.ApplicationServices.Services.Interface
{
    public interface IMenuService
    {
        ResultDto<List<MenuDto>> GetMenu();
        Task<ResultDto<IReadOnlyList<MenuDto>>> GetMenuAsync();
    }
}