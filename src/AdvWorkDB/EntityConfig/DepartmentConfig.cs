using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("Department", "HumanResources");

            entity.HasComment("Lookup table containing the departments within the Adventure Works Cycles company.");

            entity.HasIndex(e => e.Name, "AK_Department_Name")
                .IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasColumnName("DepartmentID")
                .HasComment("Primary key for Department records.");

            entity.Property(e => e.GroupName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Name of the group to which the department belongs.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Name of the department.");
        }
    }
}
