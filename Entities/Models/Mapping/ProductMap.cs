using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.ProductCode)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Price)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EncId)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Product");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Availableitems).HasColumnName("Availableitems");
            this.Property(t => t.ImageId).HasColumnName("ImageId");
            this.Property(t => t.EncId).HasColumnName("EncId");

            // Relationships
            this.HasOptional(t => t.Brand)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.BrandId);
            this.HasOptional(t => t.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.CategoryId);
            this.HasOptional(t => t.Image)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ImageId);

        }
    }
}
