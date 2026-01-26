using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class AwbuildVersionConfig : IEntityTypeConfiguration<AwbuildVersion>
    {
        public void Configure(EntityTypeBuilder<AwbuildVersion> entity)
        {
            entity.HasKey(e => e.SystemInformationId)
                .HasName("PK_AWBuildVersion_SystemInformationID");

            entity.ToTable("AWBuildVersion");

            entity.HasComment("Current version number of the AdventureWorks 2016 sample database. ");

            entity.Property(e => e.SystemInformationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SystemInformationID")
                .HasComment("Primary key for AWBuildVersion records.");

            entity.Property(e => e.DatabaseVersion)
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnName("Database Version")
                .HasComment("Version number of the database in 9.yy.mm.dd.00 format.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.VersionDate)
                .HasColumnType("datetime")
                .HasComment("Date and time the record was last updated.");
        }
    }
}
