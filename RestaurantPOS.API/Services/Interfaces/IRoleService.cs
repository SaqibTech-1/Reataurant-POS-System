using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto> GetByIdAsync(int id);
        Task<RoleDto> GetByGlobalIdAsync(Guid globalId);
        Task<RoleDto> CreateOrUpdateAsync(CreateRoleDto dto);
        Task DeleteAsync(int id);
    }
}
