using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => e.BusinessEntityId)
                .HasName("PK_Employee_BusinessEntityID");

            entity.ToTable("Employee", "HumanResources");

            entity.HasComment("Employee information such as salary, department, and title.");

            entity.HasIndex(e => e.LoginId, "AK_Employee_LoginID")
                .IsUnique();

            entity.HasIndex(e => e.NationalIdnumber, "AK_Employee_NationalIDNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_Employee_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .ValueGeneratedNever()
                .HasColumnName("BusinessEntityID")
                .HasComment("Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.");

            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasComment("Date of birth.");

            entity.Property(e => e.CurrentFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Inactive, 1 = Active");

            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("M = Male, F = Female");

            entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasComment("Employee hired on this date.");

            entity.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Work title such as Buyer or Sales Representative.");

            entity.Property(e => e.LoginId)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("LoginID")
                .HasComment("Network login.");

            entity.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("M = Married, S = Single");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.NationalIdnumber)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("NationalIDNumber")
                .HasComment("Unique national identification number such as a social security number.");

            entity.Property(e => e.OrganizationLevel)
                .HasComputedColumnSql("([OrganizationNode].[GetLevel]())", false)
                .HasComment("The depth of the employee in the corporate hierarchy.");

            entity.Property(e => e.Rowguid)
                .HasColumnName("rowguid")
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalariedFlag)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasComment("Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.");

            entity.Property(e => e.SickLeaveHours).HasComment("Number of available sick leave hours.");

            entity.Property(e => e.VacationHours).HasComment("Number of available vacation hours.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
