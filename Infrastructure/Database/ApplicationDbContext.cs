using HogwartsAPI.Domain.Trait;
using HogwartsAPI.Domain.House;
using HogwartsAPI.Domain.Wizard;
using Microsoft.EntityFrameworkCore;

namespace HogwartsAPI.Infrastructure.Database
{

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Wizard> Wizards { get; set; }
        public DbSet<Trait> Traits { get; set; }
        public DbSet<HouseTrait> HouseTraits { get; set; }
        public DbSet<WizardTrait> WizardTraits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WizardTrait>()
                .HasKey(wt => new { wt.WizardId, wt.TraitId });

            modelBuilder.Entity<WizardTrait>()
                .HasOne(wt => wt.Wizard)
                .WithMany(w => w.Traits)
                .HasForeignKey(wt => wt.WizardId);

            modelBuilder.Entity<WizardTrait>()
                .HasOne(wt => wt.Trait)
                .WithMany()
                .HasForeignKey(wt => wt.TraitId);

            modelBuilder.Entity<HouseTrait>()
                .HasKey(ht => new { ht.HouseId, ht.TraitId });

            modelBuilder.Entity<HouseTrait>()
                .HasOne(ht => ht.House)
                .WithMany(h => h.Traits)
                .HasForeignKey(ht => ht.HouseId);

            modelBuilder.Entity<HouseTrait>()
                .HasOne(ht => ht.Trait)
                .WithMany()
                .HasForeignKey(ht => ht.TraitId);
        }

    }
}