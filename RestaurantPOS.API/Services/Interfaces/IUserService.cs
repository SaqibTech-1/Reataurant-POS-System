using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByGlobalIdAsync(Guid globalId);
        Task<UserDto> CreateOrUpdateAsync(CreateUserDto dto);
        Task DeleteAsync(int id);
    }
}
