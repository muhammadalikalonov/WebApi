using WebApi.Data;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client entity)
        {
            _context.Clients.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
