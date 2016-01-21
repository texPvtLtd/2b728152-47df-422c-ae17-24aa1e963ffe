using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Models.Mapping
{
    public class BrandMap : EntityTypeConfiguration<Brand>
    {
        public BrandMap()
        {
            // Primary Key
            this.HasKey(t => t.BrandId);

            // Properties
            this.Property(t => t.EncId)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.BrandName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.BrandCode)
                .HasMaxLength(250);

            this.Property(t => t.BrandIcon)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Brand");
            this.Property(t => t.BrandId).HasColumnName("BrandId");
            this.Property(t => t.EncId).HasColumnName("EncId");
            this.Property(t => t.BrandName).HasColumnName("BrandName");
            this.Property(t => t.BrandCode).HasColumnName("BrandCode");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.BrandIcon).HasColumnName("BrandIcon");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.Brands)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
