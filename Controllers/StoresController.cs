using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly IRepository<Store> _storeRepository;

        public StoresController(IRepository<Store> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetAllStores()
        {
            var stores = await _storeRepository.GetAllAsync();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStoreById(int id)
        {
            var store = await _storeRepository.GetByIdAsync(id);
            if (store == null)
                return NotFound();

            return Ok(store);
        }

        [HttpPost]
        public async Task<ActionResult> AddStore([FromBody] Store store)
        {
            await _storeRepository.AddAsync(store);
            return CreatedAtAction(nameof(GetStoreById), new { id = store.Id }, store);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStore(int id, [FromBody] Store store)
        {
            if (id != store.Id)
                return BadRequest();

            await _storeRepository.UpdateAsync(store);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStore(int id)
        {
            await _storeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
