using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace QueueDodge
{
    public class QueueDodgeDB : DbContext
    {
        public IConfigurationRoot Configuration { get; set; }

        public DbSet<LadderChange> LadderChanges { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Realm> Realms { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Region> Regions { get; set; }

        public QueueDodgeDB() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO:  Use the new configuration options to store this connection string.
            optionsBuilder.UseNpgsql("User ID=QueueDodge;Password=queuedodge;Host=localhost;Port=5432;Database=QueueDodge;Pooling=true;Connection Lifetime=0;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
