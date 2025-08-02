using AutoMapper;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IUserContextService userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetByGlobalIdAsync(Guid globalId)
        {
            var order = await _unitOfWork.Orders.GetByGlobalIdAsync(globalId);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrUpdateAsync(CreateOrderDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var order = new Order
                {
                    TableId = dto.TableId,
                    UserId = dto.UserId,
                    OrderTime = DateTime.UtcNow,
                    Status = "Pending",
                    TotalAmount = dto.Items.Sum(i => i.UnitPrice * i.Quantity),
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = _userContext.GetUserId ?? 0
                };

                await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                foreach (var item in dto.Items)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };
                    await _unitOfWork.OrderItems.AddAsync(orderItem);
                }

                await _unitOfWork.SaveChangesAsync();
                return await GetByIdAsync(order.Id);
            }

            throw new NotImplementedException("Order update is not supported.");
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order != null)
            {
                _unitOfWork.Orders.Delete(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

}
