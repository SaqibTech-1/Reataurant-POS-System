using AutoMapper;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IUserContextService userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var list = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(list);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var c = await _unitOfWork.Categories.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(c);
        }

        public async Task<CategoryDto> GetByGlobalIdAsync(Guid globalId)
        {
            var c = await _unitOfWork.Categories.GetByGlobalIdAsync(globalId);
            return _mapper.Map<CategoryDto>(c);
        }

        public async Task<CategoryDto> CreateOrUpdateAsync(CreateCategoryDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var entity = _mapper.Map<Category>(dto);
                entity.CreatedBy = _userContext.GetUserId ?? 0;
                entity.CreatedOn = DateTime.UtcNow;

                await _unitOfWork.Categories.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<CategoryDto>(entity);
            }

            var existing = await _unitOfWork.Categories.GetByGlobalIdAsync(dto.GlobalId.Value);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.ModifiedBy = _userContext.GetUserId ?? 0;
            existing.ModifiedOn = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _unitOfWork.Categories.GetByIdAsync(id);
            if (c != null)
            {
                _unitOfWork.Categories.Delete(c);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

}
