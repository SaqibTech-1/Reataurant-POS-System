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
        private readonly IWebHostEnvironment _env;

        public ProductService(IUnitOfWork uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
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
            string? imagePath = null;

            if (dto.Image != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
                var savePath = Path.Combine(_env.WebRootPath, "images", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!); // Ensure directory exists

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = $"/images/{fileName}";
            }

            if (!dto.GlobalId.HasValue || dto.GlobalId == Guid.Empty)
            {
                var item = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CategoryId = dto.CategoryId,
                    IsAvailable = dto.IsAvailable,
                    FoodType = dto.FoodType,
                    ImageUrl = imagePath
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
                    IsAvailable = item.IsAvailable,
                    FoodType = item.FoodType,
                    ImageUrl = item.ImageUrl
                };
            }
            else
            {
                var item = await _uow.Products.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (item == null)
                    throw new Exception("Product not found");

                item.Name = dto.Name;
                item.Price = dto.Price;
                item.CategoryId = dto.CategoryId;
                item.IsAvailable = dto.IsAvailable;
                item.FoodType = dto.FoodType;
                if (imagePath != null)
                    item.ImageUrl = imagePath;

                _uow.Products.Update(item);
                await _uow.SaveChangesAsync();

                return new ProductDto
                {
                    Id = item.Id,
                    GlobalId = item.GlobalId,
                    Name = item.Name,
                    Price = item.Price,
                    CategoryId = item.CategoryId,
                    IsAvailable = item.IsAvailable,
                    FoodType = item.FoodType,
                    ImageUrl = item.ImageUrl
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
