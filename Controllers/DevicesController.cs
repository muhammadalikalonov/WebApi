using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IRepository<Device> _deviceRepository;

        public DevicesController(IRepository<Device> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetAllDevices()
        {
            var devices = await _deviceRepository.GetAllAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDeviceById(int id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device == null)
                return NotFound();

            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult> AddDevice([FromBody] Device device)
        {
            await _deviceRepository.AddAsync(device);
            return CreatedAtAction(nameof(GetDeviceById), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDevice(int id, [FromBody] Device device)
        {
            if (id != device.Id)
                return BadRequest();

            await _deviceRepository.UpdateAsync(device);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            await _deviceRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
