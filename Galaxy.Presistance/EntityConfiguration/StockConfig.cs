using Galaxy.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galaxy.Presistance.EntityConfiguration
{
    internal class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(x => x.CreationDate).HasDefaultValueSql("GETDATE()");
            builder.HasIndex(x => x.BarCode).IsUnique(false).IsClustered(false);
        }
    }
}
