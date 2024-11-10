using C2K2DP_HSZF_2024251.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2K2DP_HSZF_2024251.Persistence.MsSql
{
    public interface IHeroesVsMonstersDbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public int SaveChanges();
    }
    public class HeroesVsMonstersDbContext : DbContext , IHeroesVsMonstersDbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public HeroesVsMonstersDbContext(DbContextOptions<HeroesVsMonstersDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>()
            .HasKey(h => h.Id);

            modelBuilder.Entity<Hero>()
                .Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Hero>()
                .Property(h => h.Category)
                .IsRequired();

            modelBuilder.Entity<Hero>()
                .Property(h => h.Abilities)
                .HasMaxLength(500);


            modelBuilder.Entity<Monster>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Monster>()
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Monster>()
                .Property(m => m.Level)
                .IsRequired();


            modelBuilder.Entity<Battle>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Battle>()
                .HasOne(b => b.Hero)
                .WithMany(h => h.Battles)
                .HasForeignKey(b => b.HeroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Battle>()
                .HasOne(b => b.Monster)
                .WithMany(m => m.Battles)
                .HasForeignKey(b => b.MonsterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
