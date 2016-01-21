using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryId);

            // Properties
            this.Property(t => t.EncId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CategoryCode)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.EncId).HasColumnName("EncId");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.CategoryCode).HasColumnName("CategoryCode");
        }
    }
}
