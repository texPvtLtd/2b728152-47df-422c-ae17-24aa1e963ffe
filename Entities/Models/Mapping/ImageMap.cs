using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Models.Mapping
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageId);

            // Properties
            this.Property(t => t.Image1)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Images");
            this.Property(t => t.ImageId).HasColumnName("ImageId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.Image1).HasColumnName("Image");
        }
    }
}
