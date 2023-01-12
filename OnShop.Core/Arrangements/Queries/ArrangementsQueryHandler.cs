using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Arrangements;
using OnShop.Domain.Arrangements.Dtos;
using OnShop.Domain.Arrangements.Entities;
using OnShop.Domain.Arrangements.Queries;
using OnShop.Domain.Arrangements.Repositories;
using OnShop.Domain.DTOs;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Arrangements.Queries
{
    public class ArrangementsQueryHandler :
        IRequestHandler<ArrangementsQueries, IReadOnlyList<ArrangementGetDto>>,
        IRequestHandler<ArrangementsPaginationQueries, QueryList<ArrangementGetDto>>,
        IRequestHandler<ArrangementByIdQueries, ResultDto<ArrangementGetDto>>
    {
        private readonly IArrangementRepository _repository;
        private readonly ResultDto<ArrangementGetDto> _result;
        private readonly IMapper _mapper;

        public ArrangementsQueryHandler(IArrangementRepository repository, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _mapper = mapper;
            _result = new ResultDto<ArrangementGetDto>(resourceManager);
        }

        public async Task<IReadOnlyList<ArrangementGetDto>> Handle(ArrangementsQueries request, CancellationToken cancellationToken)
        {
            var spec = new ArrangementsSpecification(request.StoreId);
            var lst = await _repository.ListAsync(spec);
            return lst?.Select(Mapper).ToList().AsReadOnly();
        }

        public async Task<QueryList<ArrangementGetDto>> Handle(ArrangementsPaginationQueries request, CancellationToken cancellationToken)
        {
            var spec = new ArrangementsSpecification(request.SearchKey);
            var count = await _repository.CountAsync(spec);
            var res = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            var lst = res?.Select(Mapper).ToList().AsReadOnly();
            return new QueryList<ArrangementGetDto> { Data = lst, TotalCount = count };
        }

        public async Task<ResultDto<ArrangementGetDto>> Handle(ArrangementByIdQueries request, CancellationToken cancellationToken)
        {
            var result = await _repository.FirstOrDefaultAsync(new ArrangementsSpecification(request.Id));
            if (result != null)
            {
                _result.Data = Mapper(result);
            }
            else
            {
                _result.IsSuccess = false;
                _result.AddError(SharedResource.NotFound);
            }
            return _result;
        }

        private ArrangementGetDto Mapper(Arrangement model)
        {
            return new ArrangementGetDto()
            {
                Id = model.Id,
                CreatorUserId = model.CreatorUserId,
                CreateDate = model.CreateDate,
                Description = model.Description,
                ModifiedId = model.ModifiedId,
                IsRemoved = model.IsRemoved,
                ModifiedDate = model.ModifiedDate,
                RemoveTime = model.RemoveTime,

                Priority = model.Priority,
                StoreId = model.StoreId,
                StoreIdName = model.Store?.Title,
                TargetId = model.TargetId,
                Type = model.Type
            };
        }
    }
}