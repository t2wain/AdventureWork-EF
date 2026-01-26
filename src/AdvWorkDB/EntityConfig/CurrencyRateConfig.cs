using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvWorkDB.EntityConfig
{
    public class CurrencyRateConfig : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> entity)
        {
            entity.ToTable("CurrencyRate", "Sales");

            entity.HasComment("Currency exchange rates.");

            entity.HasIndex(e => new { e.CurrencyRateDate, e.FromCurrencyCode, e.ToCurrencyCode }, "AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode")
                .IsUnique();

            entity.Property(e => e.CurrencyRateId)
                .HasColumnName("CurrencyRateID")
                .HasComment("Primary key for CurrencyRate records.");

            entity.Property(e => e.AverageRate)
                .HasColumnType("money")
                .HasComment("Average exchange rate for the day.");

            entity.Property(e => e.CurrencyRateDate)
                .HasColumnType("datetime")
                .HasComment("Date and time the exchange rate was obtained.");

            entity.Property(e => e.EndOfDayRate)
                .HasColumnType("money")
                .HasComment("Final exchange rate for the day.");

            entity.Property(e => e.FromCurrencyCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Exchange rate was converted from this currency code.");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ToCurrencyCode)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength()
                .HasComment("Exchange rate was converted to this currency code.");

            entity.HasOne(d => d.FromCurrencyCodeNavigation)
                .WithMany(p => p.CurrencyRateFromCurrencyCodeNavigations)
                .HasForeignKey(d => d.FromCurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ToCurrencyCodeNavigation)
                .WithMany(p => p.CurrencyRateToCurrencyCodeNavigations)
                .HasForeignKey(d => d.ToCurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
