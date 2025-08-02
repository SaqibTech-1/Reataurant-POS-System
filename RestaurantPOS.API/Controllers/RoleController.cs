using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Modals;
using RestaurantPOS.API.Services.Interfaces;

namespace RestaurantPOS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(new ApiResponse<IEnumerable<RoleDto>>(await _service.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(new ApiResponse<RoleDto>(await _service.GetByIdAsync(id)));

        [HttpGet("guid/{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid) => Ok(new ApiResponse<RoleDto>(await _service.GetByGlobalIdAsync(guid)));

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateRoleDto dto) => Ok(new ApiResponse<RoleDto>(await _service.CreateOrUpdateAsync(dto)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Deleted successfully", true));
        }
    }
}
