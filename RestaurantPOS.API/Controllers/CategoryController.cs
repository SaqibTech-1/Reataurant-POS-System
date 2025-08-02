using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Modals;
using RestaurantPOS.API.Services.Interfaces;

namespace RestaurantPOS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(new ApiResponse<IEnumerable<CategoryDto>>(await _service.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(new ApiResponse<CategoryDto>(await _service.GetByIdAsync(id)));

        [HttpGet("guid/{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid) =>
            Ok(new ApiResponse<CategoryDto>(await _service.GetByGlobalIdAsync(guid)));

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateCategoryDto dto) =>
            Ok(new ApiResponse<CategoryDto>(await _service.CreateOrUpdateAsync(dto)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Deleted Successfully", true));
        }
    }
}
