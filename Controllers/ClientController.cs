using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IRepository<Client> _clientRepository;

        public ClientsController(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            var clients = await _clientRepository.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> AddClient([FromBody] Client client)
        {
            await _clientRepository.AddAsync(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _clientRepository.UpdateAsync(client);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            await _clientRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
