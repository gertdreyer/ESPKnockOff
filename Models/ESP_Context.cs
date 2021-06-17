using Microsoft.EntityFrameworkCore;

namespace ESPKnockOff.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TimeCode> TimeCode { get; set; }
        public DbSet<Schedule> LoadSheddingSlot { get; set; }
        public DbSet<SuburbCluster> SuburbCluster { get; set; }
        public DbSet<Suburb> Suburb { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<Province> Province { get; set; }
    }
}
