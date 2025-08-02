using AutoMapper;
using BCrypt.Net;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Implementation;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _uow.Users.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                GlobalId = u.GlobalId,
                Name = u.Name,
                UserName = u.UserName,
                RoleId = u.RoleId
            });
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            return user == null ? null : new UserDto
            {
                Id = user.Id,
                GlobalId = user.GlobalId,
                Name = user.Name,
                UserName = user.UserName,
                RoleId = user.RoleId
            };
        }

        public async Task<UserDto> GetByGlobalIdAsync(Guid globalId)
        {
            var user = await _uow.Users.GetByGlobalIdAsync(globalId);
            return user == null ? null : new UserDto
            {
                Id = user.Id,
                GlobalId = user.GlobalId,
                Name = user.Name,
                UserName = user.UserName,
                RoleId = user.RoleId
            };
        }

        public async Task<UserDto> CreateOrUpdateAsync(CreateUserDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var newUser = new User
                {
                    Name = dto.Name,
                    UserName = dto.UserName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    RoleId = dto.RoleId,
                    IsActive = true,
                };

                await _uow.Users.AddAsync(newUser);
                try
                {
                    await _uow.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("EF SaveChanges failed: " + ex.InnerException?.Message, ex);
                }

                return new UserDto
                {
                    Id = newUser.Id,
                    GlobalId = newUser.GlobalId,
                    Name = newUser.Name,
                    UserName = newUser.UserName,
                    RoleId = newUser.RoleId
                };
            }
            else
            {
                var user = await _uow.Users.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (user == null)
                    throw new Exception("User not found");

                user.Name = dto.Name;
                user.UserName = dto.UserName;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                user.RoleId = dto.RoleId;

                _uow.Users.Update(user);
                await _uow.SaveChangesAsync();

                return new UserDto
                {
                    Id = user.Id,
                    GlobalId = user.GlobalId,
                    Name = user.Name,
                    UserName = user.UserName,
                    RoleId = user.RoleId
                };
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _uow.Users.GetByIdAsync(id);
            if (user != null)
            {
                _uow.Users.Delete(user);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
