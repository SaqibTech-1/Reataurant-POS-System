using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<OrderDto> GetByGlobalIdAsync(Guid globalId);
        Task<OrderDto> CreateOrUpdateAsync(CreateOrderDto dto);
        Task DeleteAsync(int id);
    }
}
