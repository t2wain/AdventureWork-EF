using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> entity)
        {
            entity.ToTable("CreditCard", "Sales");

            entity.HasComment("Customer credit card information.");

            entity.HasIndex(e => e.CardNumber, "AK_CreditCard_CardNumber")
                .IsUnique();

            entity.Property(e => e.CreditCardId)
                .HasColumnName("CreditCardID")
                .HasComment("Primary key for CreditCard records.");

            entity.Property(e => e.CardNumber)
                .IsRequired()
                .HasMaxLength(25)
                .HasComment("Credit card number.");

            entity.Property(e => e.CardType)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("Credit card name.");

            entity.Property(e => e.ExpMonth).HasComment("Credit card expiration month.");

            entity.Property(e => e.ExpYear).HasComment("Credit card expiration year.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");
        }
    }
}
