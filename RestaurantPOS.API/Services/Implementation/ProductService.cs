using AutoMapper;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IUserContextService userContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var items = await _unitOfWork.MenuItems.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(items);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.MenuItems.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(item);
        }

        public async Task<ProductDto> GetByGlobalIdAsync(Guid globalId)
        {
            var item = await _unitOfWork.MenuItems.GetByGlobalIdAsync(globalId);
            return _mapper.Map<ProductDto>(item);
        }

        public async Task<ProductDto> CreateOrUpdateAsync(CreateProductDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var item = _mapper.Map<Product>(dto);
                item.CreatedBy = _userContext.GetUserId ?? 0;
                item.CreatedOn = DateTime.UtcNow;

                await _unitOfWork.MenuItems.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();

                return _mapper.Map<ProductDto>(item);
            }

            var existing = await _unitOfWork.MenuItems.GetByGlobalIdAsync(dto.GlobalId.Value);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            existing.ModifiedBy = _userContext.GetUserId ?? 0;
            existing.ModifiedOn = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _unitOfWork.MenuItems.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.MenuItems.Delete(item);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }

}
