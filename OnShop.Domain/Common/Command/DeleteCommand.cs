using System;
using MediatR;
using OnShop.Framework.Dtos;

namespace OnShop.Domain.Common.Command
{
    public class DeleteCommand : IRequest<ResultDto>
    {
        public long Id { get; set; }
        public bool IsSoftDelete { get; set; } = false;
        public int ModifiedId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}