using System.Collections.Generic;
using MediatR;
using OnShop.Domain.DTOs;
using OnShop.Domain.Slider.Dtos;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Slider.Queries
{
    public class SliderQueries : IRequest<IReadOnlyList<SliderDto>>
    {

    }
    public class SliderPaginationQueries : BaseQueries, IRequest<QueryList<SliderDto>>
    {

    }

    public class SliderByIdQueries : IRequest<ResultDto<SliderDto>>
    {
        public int Id { get; set; }
    }
}