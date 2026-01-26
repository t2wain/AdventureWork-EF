using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class CultureConfig : IEntityTypeConfiguration<Culture>
    {
        public void Configure(EntityTypeBuilder<Culture> entity)
        {
            entity.ToTable("Culture", "Production");

            entity.HasComment("Lookup table containing the languages in which some AdventureWorks data is stored.");

            entity.HasIndex(e => e.Name, "AK_Culture_Name")
                .IsUnique();

            entity.Property(e => e.CultureId)
                .HasMaxLength(6)
                .HasColumnName("CultureID")
                .IsFixedLength()
                .HasComment("Primary key for Culture records.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Culture description.");
        }
    }
}
