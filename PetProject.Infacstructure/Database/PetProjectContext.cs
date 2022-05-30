using Microsoft.EntityFrameworkCore;
using PetProject.Core.Entities;

namespace PetProject.Infacstructure.Database
{
    public class PetProjectContext : DbContext
    {
        public PetProjectContext(DbContextOptions<PetProjectContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryCode).HasMaxLength(10);

                entity.Property(e => e.CountryName).HasMaxLength(255);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedTime).HasPrecision(3);
            });

        }
    }
}
