using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Slider;
using OnShop.Domain.DTOs;
using OnShop.Domain.Slider.Dtos;
using OnShop.Domain.Slider.Queries;
using OnShop.Domain.Slider.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Slider.Queries
{
    public class SliderQueryHandler :
        IRequestHandler<SliderQueries, IReadOnlyList<SliderDto>>,
        IRequestHandler<SliderPaginationQueries, QueryList<SliderDto>>,
        IRequestHandler<SliderByIdQueries, ResultDto<SliderDto>>
    {
        private readonly ISliderRepository _repository;
        private readonly ResultDto<SliderDto> _result;
        private readonly IMapper _mapper;

        public SliderQueryHandler(ISliderRepository repository, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _mapper = mapper;
            _result = new ResultDto<SliderDto>(resourceManager);
        }

        public async Task<IReadOnlyList<SliderDto>> Handle(SliderQueries request, CancellationToken cancellationToken)
        {
            var lst = await _repository.ListAllAsync();
            return lst?.Select(Mapper).ToList().AsReadOnly();
        }

        private SliderDto Mapper(Domain.Slider.Entities.Slider slider)
        {
            //var res = _mapper.Map<Domain.Slider.Entities.Slider, SliderDto>(slider);
            var res = new SliderDto()
            {
                Id = slider.Id,
                CreatorUserId = slider.CreatorUserId,
                IsRemoved = slider.IsRemoved,
                CreateDate = slider.CreateDate,
                Description = slider.Description,
                Src = slider.Src,
                Link = slider.Link,
                LinkTitle = slider.LinkTitle,
                ModifiedDate = slider.ModifiedDate,
                Title = slider.Title,
                ModifiedId = slider.ModifiedId,
                RemoveTime = slider.RemoveTime
            };
            return res;
        }

        public async Task<QueryList<SliderDto>> Handle(SliderPaginationQueries request, CancellationToken cancellationToken)
        {
            var spec = new SliderSpecification(request.SearchKey);
            var count = await _repository.CountAsync(spec);
            var res = await _repository.GetPagedRespondAsync(spec, request.PageSize, request.Skip);
            var lst = res?.Select(Mapper).ToList().AsReadOnly();
            return new QueryList<SliderDto> { Data = lst, TotalCount = count };
        }

        public async Task<ResultDto<SliderDto>> Handle(SliderByIdQueries request, CancellationToken cancellationToken)
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
    }
}