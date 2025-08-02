using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> GetByGlobalIdAsync(Guid globalId);
        Task<CategoryDto> CreateOrUpdateAsync(CreateCategoryDto dto);
        Task DeleteAsync(int id);

    }
}
