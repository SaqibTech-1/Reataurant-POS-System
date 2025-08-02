using RestaurantPOS.API.DTOs;

namespace RestaurantPOS.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(AuthRequestDto dto);
        Task<UserDto> RegisterAsync(CreateUserDto dto);
    }
}
