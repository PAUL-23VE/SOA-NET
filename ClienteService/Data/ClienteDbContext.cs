using ClienteService.models;
using Microsoft.EntityFrameworkCore;

namespace ClienteService.Data
{
    public class ClienteDbContext : DbContext
    {
        public ClienteDbContext(DbContextOptions<ClienteDbContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
