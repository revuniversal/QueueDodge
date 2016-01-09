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
        public DbSet<LeaderboardComparison> LeaderboardComparisons { get; set; }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Realm> Realms { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Region> Regions { get; set; }

        // INSERTS FROM API REQUESTS ONLY.
        public DbSet<Leaderboard> Leaderboards { get; set; }

        public QueueDodgeDB() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Visual Studio 2015 | Use the LocalDb 12 instance created by Visual Studio
            optionsBuilder.UseSqlServer(@"data source=(localdb)\v11.0;integrated security=true;initial catalog=queuedodge;multipleactiveresultsets=true;");

            // Visual Studio 2013 | Use the LocalDb 11 instance created by Visual Studio
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<LeaderboardComparison>()
                .HasOne<BattleNetRequest>(p => p.CurrentRequest)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaderboardComparison>()
          .HasOne<BattleNetRequest>(p => p.PreviousRequest)
          .WithOne()
          .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
