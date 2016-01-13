using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using QueueDodge.Models;
using System.Linq;

namespace QueueDodge
{
    public class QueueDodgeDB : DbContext
    {
        public DbSet<LadderChange> LadderChanges { get; set; }
        public DbSet<BattleNetRequest> BattleNetRequests { get; set; }
        //public DbSet<LadderEntryComparison> LeaderboardComparisons { get; set; }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Realm> Realms { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Region> Regions { get; set; }

        public QueueDodgeDB() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Visual Studio 2015 | Use the LocalDb 12 instance created by Visual Studio
            optionsBuilder.UseSqlServer(@"data source=(localdb)\v11.0;integrated security=true;initial catalog=queuedodge;multipleactiveresultsets=true;");
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
