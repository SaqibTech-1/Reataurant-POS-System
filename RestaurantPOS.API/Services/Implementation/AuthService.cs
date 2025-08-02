using BCrypt.Net;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Helpers;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto dto)
        {
            var user = (await _uow.Users.GetAllAsync()).FirstOrDefault(u => u.UserName == dto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var role = (await _uow.Roles.GetByIdAsync(user.RoleId))?.Name ?? "User";

            var token = JwtHelper.GenerateToken(user, _config["Jwt:Key"], _config["Jwt:Issuer"]);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Role = role,
                ExpiresAt = DateTime.UtcNow.AddHours(2)
            };
        }

        public async Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            return await new UserService(_uow).CreateOrUpdateAsync(dto);
        }



    }
}
