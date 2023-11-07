using Discount.Grpc.Entities.V1;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>()
                .HasData(new List<Coupon>()
                {
                    new Coupon
                    {
                         Description  = "Samsung Discount",
                         ProductName = "Samsung 10",
                         Amount = 55,
                         Id = 1
                    },
                    new Coupon
                    {
                        Description  = "IPhone Discount",
                        ProductName = "IPhone X",
                        Amount = 65,
                        Id = 2
                    }
                });
        }
    }
}
