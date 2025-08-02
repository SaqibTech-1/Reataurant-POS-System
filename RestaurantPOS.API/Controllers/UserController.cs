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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(new ApiResponse<IEnumerable<UserDto>>(await _service.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(new ApiResponse<UserDto>(await _service.GetByIdAsync(id)));

        [HttpGet("guid/{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid) => Ok(new ApiResponse<UserDto>(await _service.GetByGlobalIdAsync(guid)));

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateUserDto dto)
        {
            var result = await _service.CreateOrUpdateAsync(dto);
            if (result == null)
                return NotFound(new ApiResponse<string>("User not found", false));

            return Ok(new ApiResponse<UserDto>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Deleted successfully", true));
        }
    }
}
