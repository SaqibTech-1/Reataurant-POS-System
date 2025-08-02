using BCrypt.Net;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Helpers;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<AuthResponseDto> LoginAsync(AuthRequestDto dto)
        {
            var user = (await _unitOfWork.Users.GetAllAsync()).FirstOrDefault(u => u.UserName == dto.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid Credentials.");
            }

            var role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
            var token = JwtHelper.GenerateToken(user, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"]);

            return new AuthResponseDto
            {
                Token = token,
                UserName = user.UserName,
                Role = role?.Name ?? "User",
                ExpiresAt = DateTime.UtcNow.AddHours(2),
            };
        }

        public async Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            return await _userService.CreateOrUpdateAsync(dto);
        }



    }
}
