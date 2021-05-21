using FantasyNameAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyNameAPI.Database
{
    public class FantasyNameApiContext : DbContext
    {
        public FantasyNameApiContext(DbContextOptions<FantasyNameApiContext> options) : base(options)
        { }
        
        public DbSet<FantasyModel> FantasyItems { get; set; }
    }
}