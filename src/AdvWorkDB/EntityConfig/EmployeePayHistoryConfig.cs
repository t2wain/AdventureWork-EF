using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class EmployeePayHistoryConfig : IEntityTypeConfiguration<EmployeePayHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeePayHistory> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.RateChangeDate })
                .HasName("PK_EmployeePayHistory_BusinessEntityID_RateChangeDate");

            entity.ToTable("EmployeePayHistory", "HumanResources");

            entity.HasComment("Employee pay history.");

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");

            entity.Property(e => e.RateChangeDate)
                .HasColumnType("datetime")
                .HasComment("Date the change in pay is effective");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PayFrequency).HasComment("1 = Salary received monthly, 2 = Salary received biweekly");

            entity.Property(e => e.Rate)
                .HasColumnType("money")
                .HasComment("Salary hourly rate.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.EmployeePayHistories)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
