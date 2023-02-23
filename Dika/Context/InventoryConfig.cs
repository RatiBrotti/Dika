using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dika.Models;

namespace Dika.Context
{
    public class InventoryConfig : IEntityTypeConfiguration<Invertory>
    {
        public void Configure(EntityTypeBuilder<Invertory> builder)
        {
            builder.ToTable("Inventory");

            builder.HasKey(e => e.Id);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.Property(e => e.Barcode)
                .HasMaxLength(50);   

            builder.HasIndex(e=>e.Barcode)
                .IsUnique();
               
            builder.Property(e=>e.SKU)
                .HasMaxLength(50);

            builder.Property(e => e.Size)
                .HasMaxLength(15);

            builder.Property(e=>e.Price)
                .HasColumnType("decimal(10,2)");
        }
    }

}
