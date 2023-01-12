using MediatR;
using OnShop.Framework.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnShop.Domain.Notifications.Queries
{
    public class GetNotificationQuery : IRequest<ResultDto<GetNotificationQuery>>
    {
    }
}
