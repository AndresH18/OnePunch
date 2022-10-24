using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data;

public class HeroAssociationDb : DbContext
{
    private const string ConnectionString = "Data Source=heroes.db";

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }
    public DbSet<Fight> Fights { get; set; }
    public DbSet<MonsterCell> MonsterCells { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Monster>(m =>
        {
            m.Property(p => p.DisasterLevel)
                .HasConversion<EnumToStringConverter<DisasterLevel>>();
            m.Property(p => p.Status)
                .HasConversion<EnumToStringConverter<Status>>();
        });
        
        modelBuilder.Entity<Hero>(h =>
        {
            h.Property(p => p.Rank)
                .HasConversion<EnumToStringConverter<Rank>>();
            h.Property(p => p.Status)
                .HasConversion<EnumToStringConverter<Status>>();
        });

        modelBuilder.Entity<Sponsor>(s =>
        {
            s.Property(p => p.Status)
                .HasConversion<EnumToStringConverter<Status>>();
        });

        modelBuilder.Entity<Fight>()
            .HasKey(f => new {f.Id, f.HeroId, f.MonsterId});

    }
}