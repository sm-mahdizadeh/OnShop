using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.User;
using OnShop.Domain.User.Dtos.UserAddresses;
using OnShop.Domain.User.Queries;
using OnShop.Domain.User.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;

namespace OnShop.ApplicationServices.User.Queries.UserAdress
{
    public class UserAddressQueryHandler :
        IRequestHandler<GetUserAddressQueries, List<UserAddressDto>>,
        IRequestHandler<GetZoneQueries, IReadOnlyList<DropDownDto>>,
        IRequestHandler<GetProvinceQueries, IReadOnlyList<DropDownDto>>,
        IRequestHandler<GetDistrictQueries, IReadOnlyList<DropDownDto>>
        
        

    {
        private readonly IUserAddressRepository _repository;
        private readonly IMapper _mapper;
        private readonly ResultDto<UserAddressDto> _result;

        public UserAddressQueryHandler(IUserAddressRepository repository, IMapper mapper, IResourceManager resourceManager)
        {
            _repository = repository;
            _mapper = mapper;
            _result = new ResultDto<UserAddressDto>(resourceManager);
        }

        public async Task<List<UserAddressDto>> Handle(GetUserAddressQueries request, CancellationToken cancellationToken)
        {
            var res = await _repository.ListAsync(new UserAddressSpecification(request.ApplicationUserId), true);

            return res.Select(x => new UserAddressDto()
            {
                Id = x.Id,
                DistrictName = x.District.Title,
                Plaque = x.Plaque,
                PostCode = x.PostCode,
                PostalAddress = x.PostalAddress,
                ProvinceName = x.District.Province.Title,
                ZoneName = x.District.Province.Zone.Title

            }).ToList();


        }

        public async Task<IReadOnlyList<DropDownDto>> Handle(GetZoneQueries request, CancellationToken cancellationToken)
        {
            var res = await _repository.ListZoneAllAsync();
            var dr = res.Select(x => new DropDownDto()
            {
                Value = x.Id.ToString(),
                Name = x.Title
            }).ToList().AsReadOnly();
            return dr;
        }

        public async Task<IReadOnlyList<DropDownDto>> Handle(GetProvinceQueries request, CancellationToken cancellationToken)
        {
            var res = await _repository.ListProvinceAllAsync(request.ZoneId);
            var dr = res.Select(x => new DropDownDto()
            {
                Value = x.Id.ToString(),
                Name = x.Title
            }).ToList().AsReadOnly();
            return dr;
        }
        public async Task<IReadOnlyList<DropDownDto>> Handle(GetDistrictQueries request, CancellationToken cancellationToken)
        {
            var res = await _repository.ListDistrictAllAsync(request.ProvinceId);
            var dr = res.Select(x => new DropDownDto()
            {
                Value = x.Id.ToString(),
                Name = x.Title
            }).ToList().AsReadOnly();
            return dr;
        }
    }
}