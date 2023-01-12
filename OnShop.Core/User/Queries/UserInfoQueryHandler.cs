using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications;
using OnShop.Domain.User.Dtos;
using OnShop.Domain.User.Queries;
using OnShop.Domain.User.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace OnShop.ApplicationServices.User.Queries
{
    public class UserInfoQueryHandler :
        IRequestHandler<GetApplicationUserInfoQuery, ApplicationUserInfoDto>
    {

        private readonly IApplicationUserInfoCommandRepository _repository;
        private readonly IMapper _mapper;

        public UserInfoQueryHandler(IApplicationUserInfoCommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApplicationUserInfoDto> Handle(GetApplicationUserInfoQuery request, CancellationToken cancellationToken)
        {
            var rec = await _repository.FirstOrDefaultAsync(
                new ApplicationUserInfoSpecification(request.ApplicationUserId));
            if (rec != null)
                return new ApplicationUserInfoDto()
                {
                    ApplicationUserId = rec.ApplicationUserId,
                    FirstName = rec.FirstName,
                    LastName = rec.LastName,
                    NationalCode = rec.NationalCode,
                    Birthdate = rec.Birthdate,
                    Gender = rec.Gender,
                };
            return null;
        }
    }
}