using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class BusinessEntityContactConfig : IEntityTypeConfiguration<BusinessEntityContact>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityContact> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.PersonId, e.ContactTypeId })
                .HasName("PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID");

            entity.ToTable("BusinessEntityContact", "Person");

            entity.HasComment("Cross-reference table mapping stores, vendors, and employees to people");

            entity.HasIndex(e => e.Rowguid, "AK_BusinessEntityContact_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.ContactTypeId, "IX_BusinessEntityContact_ContactTypeID");

            entity.HasIndex(e => e.PersonId, "IX_BusinessEntityContact_PersonID");

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Primary key. Foreign key to BusinessEntity.BusinessEntityID.");

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasComment("Primary key. Foreign key to Person.BusinessEntityID.");

            entity.Property(e => e.ContactTypeId)
                .HasColumnName("ContactTypeID")
                .HasComment("Primary key.  Foreign key to ContactType.ContactTypeID.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ContactType)
                .WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
