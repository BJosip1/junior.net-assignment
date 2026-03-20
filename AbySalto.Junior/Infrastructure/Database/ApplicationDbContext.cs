using Microsoft.EntityFrameworkCore;
//using Application.Interfaces;
using Domain.Models;

namespace AbySalto.Junior.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses{ get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Na čekanju" },
                new OrderStatus { Id = 2, Name = "Priprema" },
                new OrderStatus { Id = 3, Name = "Završeno" }
                );

            modelBuilder.Entity<PaymentMethod>().HasData(
               new PaymentMethod { Id = 1, Name = "Gotovina" },
               new PaymentMethod { Id = 2, Name = "Kartica" }
               );
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
