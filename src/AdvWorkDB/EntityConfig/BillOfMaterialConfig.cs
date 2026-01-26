using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class BillOfMaterialConfig : IEntityTypeConfiguration<BillOfMaterial>
    {
        public void Configure(EntityTypeBuilder<BillOfMaterial> entity)
        {
            entity.HasKey(e => e.BillOfMaterialsId)
                .HasName("PK_BillOfMaterials_BillOfMaterialsID")
                .IsClustered(false);

            entity.ToTable("BillOfMaterials", "Production");

            entity.HasComment("Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components.");

            entity.HasIndex(e => new { e.ProductAssemblyId, e.ComponentId, e.StartDate }, "AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => e.UnitMeasureCode, "IX_BillOfMaterials_UnitMeasureCode");

            entity.Property(e => e.BillOfMaterialsId)
                .HasColumnName("BillOfMaterialsID")
                .HasComment("Primary key for BillOfMaterials records.");

            entity.Property(e => e.Bomlevel)
                .HasColumnName("BOMLevel")
                .HasComment("Indicates the depth the component is from its parent (AssemblyID).");

            entity.Property(e => e.ComponentId)
                .HasColumnName("ComponentID")
                .HasComment("Component identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasComment("Date the component stopped being used in the assembly item.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PerAssemblyQty)
                .HasColumnType("decimal(8, 2)")
                .HasDefaultValueSql("((1.00))")
                .HasComment("Quantity of the component needed to create the assembly.");

            entity.Property(e => e.ProductAssemblyId)
                .HasColumnName("ProductAssemblyID")
                .HasComment("Parent product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date the component started being used in the assembly item.");

            entity.Property(e => e.UnitMeasureCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Standard code identifying the unit of measure for the quantity.");

            entity.HasOne(d => d.Component)
                .WithMany(p => p.BillOfMaterialComponents)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductAssembly)
                .WithMany(p => p.BillOfMaterialProductAssemblies)
                .HasForeignKey(d => d.ProductAssemblyId);

            entity.HasOne(d => d.UnitMeasureCodeNavigation)
                .WithMany(p => p.BillOfMaterials)
                .HasForeignKey(d => d.UnitMeasureCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
