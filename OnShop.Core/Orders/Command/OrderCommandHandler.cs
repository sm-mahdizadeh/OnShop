using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Basket;
using OnShop.ApplicationServices.Specifications.Orders;
using OnShop.Domain.Basket.Commands;
using OnShop.Domain.Basket.Repositories;
using OnShop.Domain.Orders.Commands;
using OnShop.Domain.Orders.Dtos;
using OnShop.Domain.Orders.Entities;
using OnShop.Domain.Orders.Repositories;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Orders.Command
{
    public class OrderCommandHandler :
                 IRequestHandler<AddOrderCommand, ResultDto<OrderDto>>,
                 IRequestHandler<UpdateCartFinished, ResultDto>

    {

        private readonly IOrderRepository _repository;
        private readonly IOrderDetailRepository _detailRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto<OrderDto> _result;

        public OrderCommandHandler(IOrderRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager, IOrderDetailRepository detailRepository, IBasketRepository basketRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _detailRepository = detailRepository;
            _basketRepository = basketRepository;
            _result = new ResultDto<OrderDto>(_resourceManager);

        }

        public async Task<ResultDto<OrderDto>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await IsValid())
                {
                    var orderDetail = new List<OrderDetail>();
                    var cart = await _basketRepository.FirstOrDefaultAsync(new CartSpecification(null, request.CreatorUserId.GetValueOrDefault(), false));
                    if (cart != null)
                    {


                        var model = await _repository.FirstOrDefaultAsync(new OrderSpecification(cartId: cart.Id));

                        if (cart.CartItems.Any())
                        {
                            orderDetail = cart.CartItems.Select(x => new OrderDetail()
                            {
                                Price = x.Price / x.Count,
                                Count = x.Count,
                                DiscountPrice = x.PriceDiscount.GetValueOrDefault(),
                                ProductId = x.ProductId,
                                FinalPrice = x.FinalPrice,

                                CreateDate = DateTime.Now,
                                CreatorUserId = request.CreatorUserId,
                                IsRemoved = false,

                            }).ToList();

                        }
                        if (model != null)
                        {

                            _detailRepository.Delete(model.OrderDetails);
                            //await _unitOfWork.CommitAsync(cancellationToken);

                            model.UserAddressId = request.UserAddressId;
                            model.OrderPostType = request.OrderPostType;
                            model.OrderDetails = orderDetail;


                        }
                        else
                        {
                            model = new Order
                            {
                                CreateDate = DateTime.Now,
                                CreatorUserId = request.CreatorUserId,
                                IsRemoved = false,

                                UserAddressId = request.UserAddressId,
                                OrderPostType = request.OrderPostType,
                                OrderState = request.OrderState,
                                Cart = cart,
                                OrderDetails = orderDetail
                            };

                            await _repository.AddAsync(model);

                        }


                        cart.IsFinished = true;
                        cart.ModifiedDate = DateTime.Now;

                        _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;

                        if (_result.IsSuccess)
                        {
                            if (cart.CartItems.Any())
                                _result.Data = new OrderDto
                                {
                                    Id = model.Id,
                                    Amount = cart.CartItems.Sum(x => x.FinalPrice)
                                };
                        }
                    }
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

        private async Task<bool> IsValid()
        {
            return true;
        }
        public async Task<ResultDto> Handle(UpdateCartFinished request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _repository.GetByIdAsync(request.CartId);
                if (item != null)
                {
                    var cart = await _basketRepository.FirstOrDefaultAsync(new CartSpecification(item.CartId));
                    if (cart != null)
                    {
                        cart.IsFinished = false;
                        _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    }
                }
                else
                {
                    _result.IsSuccess = false;
                    _result.AddError(SharedResource.NotFound);
                    _result.Message = _resourceManager[SharedResource.NotFound];
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
    }
}