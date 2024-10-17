using WebApi.Data;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repositories
{
    public class DeviceRepository : IRepository<Device>
    {
        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.Store)
                .Include(d => d.Client)
                .ToListAsync();
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.Store)
                .Include(d => d.Client)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}
