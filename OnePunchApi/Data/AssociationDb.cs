using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data;

public class AssociationDb : DbContext
{
    private const string ConnectionString = "Data Source=heros.db";

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }

    public DbSet<Fight> Fights { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Monster>()
            .Property(m => m.DisasterLevel)
            .HasConversion<EnumToStringConverter<DisasterLevel>>();

        modelBuilder.Entity<Hero>()
            .Property(h => h.Rank)
            .HasConversion<EnumToStringConverter<Rank>>();

        modelBuilder.Entity<Fight>()
            .HasKey(f => new {f.Id, f.HeroId, f.MonsterId});

        // modelBuilder.Entity<HeroSponsorship>()
        //     .HasKey(hsp => new {hsp.SponsorId, hsp.HeroId});
        //
        // modelBuilder.Entity<MonsterSponsorship>()
        //     .HasKey(msp => new {msp.SponsorId, msp.MonsterId});
    }
}