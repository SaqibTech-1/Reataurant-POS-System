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
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var list = await _uow.Categories.GetAllAsync();
            return list.Select(c => new CategoryDto
            {
                Id = c.Id,
                GlobalId = c.GlobalId,
                Name = c.Name
            });
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);
            return c == null ? null : new CategoryDto
            {
                Id = c.Id,
                GlobalId = c.GlobalId,
                Name = c.Name
            };
        }

        public async Task<CategoryDto> GetByGlobalIdAsync(Guid globalId)
        {
            var c = await _uow.Categories.GetByGlobalIdAsync(globalId);
            return c == null ? null : new CategoryDto
            {
                Id = c.Id,
                GlobalId = c.GlobalId,
                Name = c.Name
            };
        }

        public async Task<CategoryDto> CreateOrUpdateAsync(CreateCategoryDto dto)
        {
            if (!dto.GlobalId.HasValue)
            {
                var newCategory = new Category
                {
                    Name = dto.Name
                };
                await _uow.Categories.AddAsync(newCategory);
                await _uow.SaveChangesAsync();

                return new CategoryDto
                {
                    Id = newCategory.Id,
                    GlobalId = newCategory.GlobalId,
                    Name = newCategory.Name
                };
            }
            else
            {
                var existingCategory = await _uow.Categories.GetByGlobalIdAsync(dto.GlobalId.Value);
                if (existingCategory == null)
                    throw new Exception("Category not found");

                existingCategory.Name = dto.Name;
                _uow.Categories.Update(existingCategory);
                await _uow.SaveChangesAsync();

                return new CategoryDto
                {
                    Id = existingCategory.Id,
                    GlobalId = existingCategory.GlobalId,
                    Name = existingCategory.Name
                };
            }
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category != null)
            {
                _uow.Categories.Delete(category);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
