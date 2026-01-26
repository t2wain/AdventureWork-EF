using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class EmailAddressConfig : IEntityTypeConfiguration<EmailAddress>
    {
        public void Configure(EntityTypeBuilder<EmailAddress> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.EmailAddressId })
                .HasName("PK_EmailAddress_BusinessEntityID_EmailAddressID");

            entity.ToTable("EmailAddress", "Person");

            entity.HasComment("Where to send a person email.");

            entity.HasIndex(e => e.EmailAddress1, "IX_EmailAddress_EmailAddress");

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Primary key. Person associated with this email address.  Foreign key to Person.BusinessEntityID");

            entity.Property(e => e.EmailAddressId)
                .ValueGeneratedOnAdd()
                .HasColumnName("EmailAddressID")
                .HasComment("Primary key. ID of this email address.");

            entity.Property(e => e.EmailAddress1)
                .HasMaxLength(50)
                .HasColumnName("EmailAddress")
                .HasComment("E-mail address for the person.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.EmailAddresses)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
