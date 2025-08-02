using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;
using RestaurantPOS.API.Services.GetCurrentUser;
using RestaurantPOS.API.Services.Interfaces;
using RestaurantPOS.API.UnitOfWork.Interfaces;

namespace RestaurantPOS.API.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var items = await _uow.Products.GetAllAsync();
            return items.Select(m => new ProductDto
            {
                Id = m.Id,
                GlobalId = m.GlobalId,
                Name = m.Name,
                Price = m.Price,
                CategoryId = m.CategoryId,
                IsAvailable = m.IsAvailable
            });
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var m = await _uow.Products.GetByIdAsync(id);
            return m == null ? null : new ProductDto
            {
                Id = m.Id,
                GlobalId = m.GlobalId,
                Name = m.Name,
                Price = m.Price,
                CategoryId = m.CategoryId,
                IsAvailable = m.IsAvailable
            };
        }

        public async Task<ProductDto> GetByGlobalIdAsync(Guid globalId)
        {
            var m = await _uow.Products.GetByGlobalIdAsync(globalId);
            return m == null ? null : new ProductDto
            {
                Id = m.Id,
                GlobalId = m.GlobalId,
                Name = m.Name,
                Price = m.Price,
                CategoryId = m.CategoryId,
                IsAvailable = m.IsAvailable
            };
        }

        public async Task<ProductDto> CreateOrUpdateAsync(CreateProductDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var item = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId,
                    IsAvailable = dto.IsAvailable
                };
                await _uow.Products.AddAsync(item);
                await _uow.SaveChangesAsync();

                return new ProductDto
                {
                    Id = item.Id,
                    GlobalId = item.GlobalId,
                    Name = item.Name,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    IsAvailable = item.IsAvailable
                };
            }
            else
            {
                var item = await _uow.Products.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (item == null)
                    throw new Exception("MenuItem not found");

                item.Name = dto.Name;
                item.Price = dto.Price;
                item.CategoryId = dto.CategoryId;
                item.IsAvailable = dto.IsAvailable;

                _uow.Products.Update(item);
                await _uow.SaveChangesAsync();

                return new ProductDto
                {
                    Id = item.Id,
                    GlobalId = item.GlobalId,
                    Name = item.Name,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    IsAvailable = item.IsAvailable
                };
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _uow.Products.GetByIdAsync(id);
            if (item != null)
            {
                _uow.Products.Delete(item);
                await _uow.SaveChangesAsync();
            }
        }



    }
}
