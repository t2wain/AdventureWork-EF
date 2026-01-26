using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class AddressTypeConfig : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> entity)
        {
            entity.ToTable("AddressType", "Person");

            entity.HasComment("Types of addresses stored in the Address table. ");

            entity.HasIndex(e => e.Name, "AK_AddressType_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_AddressType_rowguid")
                .IsUnique();

            entity.Property(e => e.AddressTypeId)
                .HasColumnName("AddressTypeID")
                .HasComment("Primary key for AddressType records.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Address type description. For example, Billing, Home, or Shipping.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
        }

    }
}
