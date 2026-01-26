using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class EmployeeDepartmentHistoryConfig : IEntityTypeConfiguration<EmployeeDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.StartDate, e.DepartmentId, e.ShiftId })
                .HasName("PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID");

            entity.ToTable("EmployeeDepartmentHistory", "HumanResources");

            entity.HasComment("Employee department transfers.");

            entity.HasIndex(e => e.DepartmentId, "IX_EmployeeDepartmentHistory_DepartmentID");

            entity.HasIndex(e => e.ShiftId, "IX_EmployeeDepartmentHistory_ShiftID");

            entity.Property(e => e.BusinessEntityId)
                .HasColumnName("BusinessEntityID")
                .HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");

            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasComment("Date the employee started work in the department.");

            entity.Property(e => e.DepartmentId)
                .HasColumnName("DepartmentID")
                .HasComment("Department in which the employee worked including currently. Foreign key to Department.DepartmentID.");

            entity.Property(e => e.ShiftId)
                .HasColumnName("ShiftID")
                .HasComment("Identifies which 8-hour shift the employee works. Foreign key to Shift.Shift.ID.");

            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasComment("Date the employee left the department. NULL = Current department.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.EmployeeDepartmentHistories)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Department)
                .WithMany(p => p.EmployeeDepartmentHistories)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Shift)
                .WithMany(p => p.EmployeeDepartmentHistories)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
