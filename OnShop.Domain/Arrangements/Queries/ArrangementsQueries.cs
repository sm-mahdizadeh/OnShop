using System.Collections.Generic;
using MediatR;
using OnShop.Domain.Arrangements.Dtos;
using OnShop.Domain.DTOs;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Arrangements.Queries
{
    public class ArrangementsQueries : IRequest<IReadOnlyList<ArrangementGetDto>>
    {
        public long? StoreId { get; set; }
    }
    public class ArrangementsPaginationQueries : BaseQueries, IRequest<QueryList<ArrangementGetDto>>
    {
        
    }

    public class ArrangementByIdQueries : IRequest<ResultDto<ArrangementGetDto>>
    {
        public long Id { get; set; }
    }
}