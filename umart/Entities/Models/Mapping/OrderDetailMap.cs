using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Entities.Models.Mapping
{
    public class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderId, t.CustomerName, t.Address, t.City, t.Pincode, t.Contact, t.ProductId, t.OrderQuantity, t.DeliveryDate });

            // Properties
            this.Property(t => t.OrderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Pincode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Contact)
                .IsRequired()
                .HasMaxLength(14);

            this.Property(t => t.ProductId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrderQuantity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryDate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OrderDetails");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Pincode).HasColumnName("Pincode");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.OrderQuantity).HasColumnName("OrderQuantity");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.OrderDetails)
                .HasForeignKey(d => d.ProductId);

        }
    }
}
