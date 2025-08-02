using AutoMapper;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IUserContextService userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetByIdAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> GetByGlobalIdAsync(Guid globalId)
        {
            var role = await _unitOfWork.Roles.GetByGlobalIdAsync(globalId);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateOrUpdateAsync(CreateRoleDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var role = _mapper.Map<Role>(dto);
                role.CreatedOn = DateTime.UtcNow;
                role.CreatedBy = _userContext.GetUserId ?? 0;

                await _unitOfWork.Roles.AddAsync(role);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<RoleDto>(role);
            }

            var existing = await _unitOfWork.Roles.GetByGlobalIdAsync(dto.GlobalId.Value);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.ModifiedOn = DateTime.UtcNow;
            existing.ModifiedBy = _userContext.GetUserId ?? 0;

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<RoleDto>(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(id);
            if (role != null)
            {
                _unitOfWork.Roles.Delete(role);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

}
