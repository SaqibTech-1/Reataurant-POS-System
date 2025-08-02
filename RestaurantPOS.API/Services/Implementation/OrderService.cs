using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _uow.Orders.GetAllAsync();
            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                GlobalId = o.GlobalId,
                TableId = o.TableId,
                UserId = o.UserId,
                OrderTime = o.OrderTime,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                OrderItems = o.Items?.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            });
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var o = await _uow.Orders.GetByIdAsync(id);
            return o == null ? null : new OrderDto
            {
                Id = o.Id,
                GlobalId = o.GlobalId,
                TableId = o.TableId,
                UserId = o.UserId,
                OrderTime = o.OrderTime,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                OrderItems = o.Items?.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task<OrderDto> GetByGlobalIdAsync(Guid globalId)
        {
            var o = await _uow.Orders.GetByGlobalIdAsync(globalId);
            return o == null ? null : await GetByIdAsync(o.Id);
        }

        public async Task<OrderDto> CreateOrUpdateAsync(CreateOrderDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var order = new Order
                {
                    TableId = dto.TableId,
                    UserId = dto.UserId,
                    Status = "Pending",
                    OrderTime = DateTime.UtcNow,
                    TotalAmount = dto.Items.Sum(i => i.Quantity * i.UnitPrice),
                    Items = dto.Items.Select(i => new OrderItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                };

                await _uow.Orders.AddAsync(order);
                await _uow.SaveChangesAsync();

                return await GetByIdAsync(order.Id);
            }
            else
            {
                var existing = await _uow.Orders.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (existing == null) throw new Exception("Order not found");

                existing.TableId = dto.TableId;
                existing.UserId = dto.UserId;
                existing.TotalAmount = dto.Items.Sum(i => i.Quantity * i.UnitPrice);
                existing.Items = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList();

                _uow.Orders.Update(existing);
                await _uow.SaveChangesAsync();

                return await GetByIdAsync(existing.Id);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var o = await _uow.Orders.GetByIdAsync(id);
            if (o != null)
            {
                _uow.Orders.Delete(o);
                await _uow.SaveChangesAsync();
            }
        }


    }
}
