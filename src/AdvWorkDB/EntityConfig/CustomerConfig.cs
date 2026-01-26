using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvWorkDB.EntityConfig
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customer", "Sales");

            entity.HasComment("Current customer information. Also see the Person and Store tables.");

            entity.HasIndex(e => e.AccountNumber, "AK_Customer_AccountNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_Customer_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.TerritoryId, "IX_Customer_TerritoryID");

            entity.Property(e => e.CustomerId)
                .HasColumnName("CustomerID")
                .HasComment("Primary key.");

            entity.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComputedColumnSql("(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))", false)
                .HasComment("Unique number identifying the customer assigned by the accounting system.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasComment("Foreign key to Person.BusinessEntityID");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StoreId)
                .HasColumnName("StoreID")
                .HasComment("Foreign key to Store.BusinessEntityID");

            entity.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasComment("ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.HasOne(d => d.Person)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.PersonId);

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.StoreId);

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.TerritoryId);
        }
    }
}
