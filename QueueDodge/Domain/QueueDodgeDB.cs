using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using QueueDodge.Data;
using System.Linq;

namespace QueueDodge
{
    public class QueueDodgeDB : DbContext
    {
        private QueueDodgeOptions options;

        public DbSet<LadderChangeModel> LadderChanges { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Realm> Realms { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Region> Regions { get; set; }

        public QueueDodgeDB(IOptions<QueueDodgeOptions> options)
        {
            this.options = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO:  Use the new configuration options to store this connection string.
            optionsBuilder.UseNpgsql(options.connection);
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
