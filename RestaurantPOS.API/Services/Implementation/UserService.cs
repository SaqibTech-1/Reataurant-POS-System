using AutoMapper;
using BCrypt.Net;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IUserContextService userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByGlobalIdAsync(Guid globalId)
        {
            var user = await _unitOfWork.Users.GetByGlobalIdAsync(globalId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateOrUpdateAsync(CreateUserDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var user = _mapper.Map<User>(dto);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }
            else
            {
                var user = await _unitOfWork.Users.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (user == null)
                    return null;

                _mapper.Map(dto, user);

                if (!string.IsNullOrEmpty(dto.Password))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                }

                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
