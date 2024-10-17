using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Store)
                .WithMany(s => s.Devices)
                .HasForeignKey(d => d.StoreId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Client)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.ClientId);
        }
    }
}
