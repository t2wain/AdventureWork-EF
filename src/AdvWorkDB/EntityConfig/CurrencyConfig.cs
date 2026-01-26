using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> entity)
        {
            entity.HasKey(e => e.CurrencyCode)
                .HasName("PK_Currency_CurrencyCode");

            entity.ToTable("Currency", "Sales");

            entity.HasComment("Lookup table containing standard ISO currencies.");

            entity.HasIndex(e => e.Name, "AK_Currency_Name")
                .IsUnique();

            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("The ISO code for the Currency.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Currency name.");
        }
    }
}
