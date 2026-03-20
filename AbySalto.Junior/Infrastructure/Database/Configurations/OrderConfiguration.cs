using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AbySalto.Junior.Infrastructure.Database.Configurations
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .IsRequired();

            builder.Property(o => o.CustomerName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.OrderTime)
                .IsRequired();

            builder.Property(o => o.DeliveryAddress)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.ContactNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.Note)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(o => o.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(o => o.PaymentMethodId)
                .IsRequired();

            builder.Property(o => o.OrderStatusId)
               .IsRequired();

            builder.HasOne(o => o.OrderStatus)
              .WithMany(os => os.Orders)
              .HasForeignKey(o => o.OrderStatusId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.PaymentMethod)
               .WithMany(pm => pm.Orders)
               .HasForeignKey(o => o.PaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(i => i.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
