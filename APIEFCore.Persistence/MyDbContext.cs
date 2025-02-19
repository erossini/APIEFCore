using APIEFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIEFCore.Persistence
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> option) : base(option)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Channel> Channels { get; set; }
    }
}