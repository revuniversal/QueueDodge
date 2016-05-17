﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
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
            optionsBuilder.UseNpgsql(options.connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
