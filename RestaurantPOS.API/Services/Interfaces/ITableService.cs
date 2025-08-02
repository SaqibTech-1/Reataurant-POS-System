using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllAsync();
        Task<TableDto> GetByIdAsync(int id);
        Task<TableDto> GetByGlobalIdAsync(Guid globalId);
        Task<TableDto> CreateOrUpdateAsync(CreateTableDto dto);
        Task DeleteAsync(int id);
    }
}
