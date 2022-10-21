using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnePunchApi.Data.Model;

namespace OnePunchApi.Data;

public class AssociationDb : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Monster>()
            .Property(m => m.DisasterLevel)
            .HasConversion<EnumToStringConverter<DisasterLevel>>();

        modelBuilder.Entity<Hero>()
            .Property(h => h.Rank)
            .HasConversion<EnumToStringConverter<Rank>>();

        modelBuilder.Entity<HeroSponsorship>()
            .HasKey(hsp => new {hsp.SponsorId, hsp.HeroId});

        modelBuilder.Entity<MonsterSponsorship>()
            .HasKey(msp => new {msp.SponsorId, msp.MonsterId});
    }

    // TODO: use Enum Converter for DisasterLevel, Rank
}