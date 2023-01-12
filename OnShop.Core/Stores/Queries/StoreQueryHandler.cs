using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Stores;
using OnShop.Domain.DTOs;
using OnShop.Domain.Stores.Dtos;
using OnShop.Domain.Stores.Entities;
using OnShop.Domain.Stores.Queries;
using OnShop.Domain.Stores.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Stores.Queries
{
    public class StoreQueryHandler :
        IRequestHandler<StoreQuery, IReadOnlyList<StoreDto>>,
        IRequestHandler<StorePaginationQuery, QueryList<StoreDto>>,
        IRequestHandler<StoreGetByIdQuery, ResultDto<StoreDto>>,
        IRequestHandler<StoreGetByCodeQuery, ResultDto<StoreDto>>
    {
        private readonly IStoreRepository _repository;
        private readonly ResultDto<StoreDto> _result;
        private readonly IMapper _mapper;

        public StoreQueryHandler(IStoreRepository repository, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _mapper = mapper;
            _result = new ResultDto<StoreDto>(resourceManager);
        }

        public async Task<IReadOnlyList<StoreDto>> Handle(StoreQuery request, CancellationToken cancellationToken)
        {
            var lst = await _repository.ListAllAsync();
            return lst?.Select(Mapper).ToList().AsReadOnly();
        }

        public async Task<QueryList<StoreDto>> Handle(StorePaginationQuery request, CancellationToken cancellationToken)
        {
            var spec = new StoreSpecification(request.SearchKey);
            var count = await _repository.CountAsync(spec);
            var res = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            var lst = res?.Select(Mapper).ToList().AsReadOnly();
            return new QueryList<StoreDto> { Data = lst, TotalCount = count };
        }

        public async Task<ResultDto<StoreDto>> Handle(StoreGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

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
        public async Task<ResultDto<StoreDto>> Handle(StoreGetByCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByCodeAsync(request.Code);

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
        private StoreDto Mapper(Store model)
        {
            return new StoreDto()
            {
                Id = model.Id,
                CreatorUserId = model.CreatorUserId,
                CreateDate = model.CreateDate,
                ModifiedId = model.ModifiedId,
                IsRemoved = model.IsRemoved,
                ModifiedDate = model.ModifiedDate,
                RemoveTime = model.RemoveTime,

                Description = model.Description,
                Title = model.Title,
                StoreType = model.StoreType,
                MembershipType = model.MembershipType,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                BigLogo = model.BigLogo,
                SmallLogo = model.SmallLogo,
                Code = model.Code,
            };
        }
    }
}