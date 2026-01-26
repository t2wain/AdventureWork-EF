using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class ContactTypeConfig : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> entity)
        {
            entity.ToTable("ContactType", "Person");

            entity.HasComment("Lookup table containing the types of business entity contacts.");

            entity.HasIndex(e => e.Name, "AK_ContactType_Name")
                .IsUnique();

            entity.Property(e => e.ContactTypeId)
                .HasColumnName("ContactTypeID")
                .HasComment("Primary key for ContactType records.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Contact type description.");
        }
    }
}
