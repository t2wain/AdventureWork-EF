using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class CountryRegionConfig : IEntityTypeConfiguration<CountryRegion>
    {
        public void Configure(EntityTypeBuilder<CountryRegion> entity)
        {
            entity.HasKey(e => e.CountryRegionCode)
                .HasName("PK_CountryRegion_CountryRegionCode");

            entity.ToTable("CountryRegion", "Person");

            entity.HasComment("Lookup table containing the ISO standard codes for countries and regions.");

            entity.HasIndex(e => e.Name, "AK_CountryRegion_Name")
                .IsUnique();

            entity.Property(e => e.CountryRegionCode)
                .HasMaxLength(3)
                .HasComment("ISO standard code for countries and regions.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Country or region name.");
        }
    }
}
