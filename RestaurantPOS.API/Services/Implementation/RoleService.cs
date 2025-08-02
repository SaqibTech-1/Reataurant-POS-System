using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;

        public RoleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _uow.Roles.GetAllAsync();
            return roles.Select(r => new RoleDto
            {
                Id = r.Id,
                GlobalId = r.GlobalId,
                Name = r.Name
            });
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _uow.Roles.GetByIdAsync(id);
            return role == null ? null : new RoleDto
            {
                Id = role.Id,
                GlobalId = role.GlobalId,
                Name = role.Name
            };
        }

        public async Task<RoleDto> GetByGlobalIdAsync(Guid globalId)
        {
            var role = await _uow.Roles.GetByGlobalIdAsync(globalId);
            return role == null ? null : new RoleDto
            {
                Id = role.Id,
                GlobalId = role.GlobalId,
                Name = role.Name
            };
        }

        public async Task<RoleDto> CreateOrUpdateAsync(CreateRoleDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var newRole = new Role { Name = dto.Name };
                await _uow.Roles.AddAsync(newRole);
                await _uow.SaveChangesAsync();

                return new RoleDto
                {
                    Id = newRole.Id,
                    GlobalId = newRole.GlobalId,
                    Name = newRole.Name
                };
            }
            else
            {
                var role = await _uow.Roles.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (role == null) throw new Exception("Role not found");

                role.Name = dto.Name;
                _uow.Roles.Update(role);
                await _uow.SaveChangesAsync();

                return new RoleDto
                {
                    Id = role.Id,
                    GlobalId = role.GlobalId,
                    Name = role.Name
                };
            }
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _uow.Roles.GetByIdAsync(id);
            if (role != null)
            {
                _uow.Roles.Delete(role);
                await _uow.SaveChangesAsync();
            }
        }



    }
}
