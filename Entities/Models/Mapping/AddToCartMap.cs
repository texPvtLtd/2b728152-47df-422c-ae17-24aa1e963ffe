using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Models.Mapping
{
    public class AddToCartMap : EntityTypeConfiguration<AddToCart>
    {
        public AddToCartMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("AddToCart");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ProductId).HasColumnName("ProductId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.AddToCarts)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
