using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Basket;
using OnShop.Domain.Basket.Dtos;
using OnShop.Domain.Basket.Queries;
using OnShop.Domain.Basket.Repositories;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Basket.Queries
{
    public class BasketQueryHandler :
        IRequestHandler<GetCardQueries, ResultDto<CartDto>>,
        IRequestHandler<GetCardShippingQueries, ResultDto<CartPayDto>>,
        IRequestHandler<GetCountItemQueries, ResultDto<int>>
    {
        private readonly IBasketRepository _repository;
        private readonly ResultDto<CartDto> _result;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;

        public BasketQueryHandler(IBasketRepository repository, IResourceManager resourceManager)
        {
            _repository = repository;
            _resourceManager = resourceManager;
            _result = new ResultDto<CartDto>(_resourceManager);
        }

        public async Task<ResultDto<CartDto>> Handle(GetCardQueries request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _repository.FirstOrDefaultAsync(new CartSpecification(request.BrowserId, request.UserId));
                if (cart != null)
                {
                    if (!cart.CreatorUserId.HasValue && request.UserId.HasValue)
                    {
                        cart.CreatorUserId = request.UserId;
                        foreach (var item in cart.CartItems)
                        {
                            item.CreatorUserId = request.UserId;
                        }
                        await _repository.SaveAsync(cancellationToken);
                    }
                    _result.Data = new CartDto
                    {
                        CartId = cart.Id,

                        CartItemDtos = cart.CartItems.Select(p => new CartItemDto
                        {
                            CartItemId = p.Id,
                            ProductId = p.ProductId,
                            Price = p.Price,
                            PriceDiscount = p.PriceDiscount.GetValueOrDefault(0),
                            FinalPrice = p.FinalPrice,
                            Count = p.Count,
                            ProductName = p.Product.Title,
                            ProductCode = p.Product.Code,
                            ImageSrc = p.Product?.ProductImages?.FirstOrDefault()?.Src,
                            //TotalCount = p.FinalPrice
                        }).ToList()
                    };
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.AddError(SharedResource.NotFound);
                }
            }
            catch (Exception e)
            {
                _result.Message = e.Message;
                _result.AddError(SharedResource.SaveError);
                _result.IsSuccess = false;
            }
            return _result;
        }

        public async Task<ResultDto<int>> Handle(GetCountItemQueries request, CancellationToken cancellationToken)
        {
            var res = new ResultDto<int>(_resourceManager);
            try
            {
                var cartItemCount = await _repository.CountCartItemAsync(new CartItemSpecification(request.BrowserId, request.UserId));
                res.Data = cartItemCount;

            }
            catch (Exception)
            {
                res.Data = 0;
            }
            return res;
        }

        public async Task<ResultDto<CartPayDto>> Handle(GetCardShippingQueries request, CancellationToken cancellationToken)
        {
            var res = new ResultDto<CartPayDto>(_resourceManager);
            try
            {
                var cart = await _repository.FirstOrDefaultAsync(new CartSpecification(request.BrowserId, request.UserId, false));
                if (cart != null)
                {
                    var cartItem = cart.CartItems.ToList();
                    res.Data = new CartPayDto
                    {
                        Count = cartItem.Sum(x => x.Count),
                        CartId = cart.Id,
                        FinalAmount = cartItem.Sum(x => x.FinalPrice)
                    };
                }
                else
                {
                    res.IsSuccess = false;
                    res.AddError(SharedResource.NotFound);
                }
            }
            catch (Exception e)
            {
                res.Message = e.Message;
                res.AddError(SharedResource.SaveError);
                res.IsSuccess = false;
            }

            return res;
        }
    }
}