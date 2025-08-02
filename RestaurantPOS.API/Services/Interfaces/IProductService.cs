using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> GetByGlobalIdAsync(Guid globalId);
        Task<ProductDto> CreateOrUpdateAsync(CreateProductDto dto);
        Task DeleteAsync(int id);
    }
}
