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
    public class TableController : ControllerBase
    {
        private readonly ITableService _service;

        public TableController(ITableService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(new ApiResponse<IEnumerable<TableDto>>(await _service.GetAllAsync()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(new ApiResponse<TableDto>(await _service.GetByIdAsync(id)));

        [HttpGet("guid/{guid}")]
        public async Task<IActionResult> GetByGuid(Guid guid) => Ok(new ApiResponse<TableDto>(await _service.GetByGlobalIdAsync(guid)));

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(CreateTableDto dto) => Ok(new ApiResponse<TableDto>(await _service.CreateOrUpdateAsync(dto)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<string>("Deleted successfully", true));
        }
    }
}
