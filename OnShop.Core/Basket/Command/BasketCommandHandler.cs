using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnShop.ApplicationServices.Specifications.Basket;
using OnShop.Domain.Basket.Commands;
using OnShop.Domain.Basket.Entities;
using OnShop.Domain.Basket.Repositories;
using OnShop.Domain.Product.Repositories.Products;
using OnShop.Domain.SeedWork;
using OnShop.Framework.Dtos;
using OnShop.Framework.Resources.Interface;
using OnShop.Resources.Resources;

namespace OnShop.ApplicationServices.Basket.Command
{
    public class BasketCommandHandler :
         IRequestHandler<AddCartCommand, ResultDto>,
         IRequestHandler<RemoveFromCardCommand, ResultDto>,
         IRequestHandler<UpdateCountCartCommand, ResultDto>

    {
        private readonly IBasketRepository _repository;
        private readonly IProductsRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResourceManager _resourceManager;
        private readonly ResultDto _result;

        public BasketCommandHandler(IBasketRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IResourceManager resourceManager, IProductsRepository productRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _resourceManager = resourceManager;
            _productRepository = productRepository;
            _result = new ResultDto(_resourceManager);
        }

        public async Task<ResultDto> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product != null && !IsValidCount(product.Quantity, request.Count))
                {
                    _result.IsSuccess = false;
                    _result.Message = _resourceManager[SharedResource.UpdateBasket];
                    return _result;

                }

                var cart = await _repository.FirstOrDefaultAsync(new CartSpecification(request.BrowserId, false, request.UserId));
                long cartId = 0;
                if (cart == null)
                {
                    var newCart = new Cart
                    {
                        CreateDate = DateTime.Now,
                        BrowserId = request.BrowserId,
                        BrowserName = request.BrowserName,
                        IsFinished = false,
                        UserIp = request.UserIp,
                        CreatorUserId = request.UserId
                    };
                    await _repository.AddAsync(newCart);
                    cartId = await _unitOfWork.CommitAsync(cancellationToken);
                    if (cartId > 0)
                        cart = newCart;
                }
                else
                {
                    cartId = cart.Id;
                }

                if (cartId > 0)
                {
                    if (cart != null && product != null)
                    {
                        var priceDiscount =
                            CurrentPrice(product.Price, product.Discount, product.PriceDiscount);
                        var cartItem =
                            await _repository.GetCartItemAsync(new CartItemSpecification(request.ProductId, cart.Id));
                        if (cartItem != null)
                        {
                            var cnt = cartItem.Count + request.Count;
                            cartItem.Count = cnt;
                            cartItem.Price = product.Price * cnt;
                            cartItem.PriceDiscount = priceDiscount * cnt;
                            cartItem.FinalPrice = (product.Price - priceDiscount) * cnt;
                        }
                        else
                        {

                            
                            var crItem = new CartItem
                            {
                                CreatorUserId = request.UserId,
                                IsRemoved = false,
                                CreateDate = DateTime.Now,
                                Cart = cart,
                                Product = product,
                                Count = request.Count,
                                Price = product.Price * request.Count,
                                PriceDiscount = priceDiscount * request.Count,
                                FinalPrice = (product.Price - priceDiscount) * request.Count,

                            };
                            await _repository.AddCartItemAsync(crItem);

                        }
                        _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    }
                }
                else
                {
                    _result.IsSuccess = false;
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

        private decimal CurrentPrice(decimal price, int? discount = 0, decimal? priceDiscount = 0)
        {
            var newPrice = 0M;
            if (discount > 0)
            {
                newPrice = (price * discount.GetValueOrDefault() / 100);
            }
            else if (priceDiscount > 0)
            {
                newPrice = priceDiscount.GetValueOrDefault();
            }
            return newPrice;
        }

        public async Task<ResultDto> Handle(RemoveFromCardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartItem = await _repository.GetCartItemAsync(new CartItemSpecification(request.CartItemId, request.BrowserId, request.UserId));

                if (cartItem != null)
                {
                    _repository.SoftDeleteCartItem(cartItem);
                    _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                    if (_result.IsSuccess)
                        _result.Message = _resourceManager[SharedResource.CartItemDelete];
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
                _result.IsSuccess = false;
                _result.AddError(SharedResource.SaveError);
            }

            return _result;
        }

        public async Task<ResultDto> Handle(UpdateCountCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _repository.GetCartItemAsync(new CartItemSpecification(request.CartItemId, request.BrowserId, request.UserId));
                if (item != null)
                {
                    item.Count = request.Count;
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    if (product != null && IsValidCount(product.Quantity, item.Count))
                    {
                        var priceDiscount = CurrentPrice(product.Price, product.Discount, product.PriceDiscount);
                        item.Price = product.Price * request.Count;
                        item.PriceDiscount = priceDiscount * request.Count;
                        item.FinalPrice = (product.Price - priceDiscount) * request.Count;
                        _result.IsSuccess = await _unitOfWork.CommitAsync(cancellationToken) > 0;
                        _result.Message = _resourceManager[SharedResource.UpdateBasket];
                    }
                    else
                    {
                        _result.IsSuccess = false;
                        _result.AddError(SharedResource.ValidCount);
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

        /// <summary>
        /// تعداد کالای انتخابی با موجود در انبار
        /// </summary>
        /// <param name="productCount"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private bool IsValidCount(int productCount, int count)
        {
            return count <= productCount;
        }


    }
}