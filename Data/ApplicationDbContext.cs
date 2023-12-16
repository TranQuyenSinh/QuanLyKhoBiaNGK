using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoBiaNGK.Models;

namespace QuanLyKhoBiaNGK.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<ReceivedBill> ReceivedBills { get; set; }
        public virtual DbSet<DetailReceived> DetailReceiveds { get; set; }
        public virtual DbSet<DeliveryBill> DeliveryBills { get; set; }
        public virtual DbSet<DeliveryBillItem> DeliveryBillItems { get; set; }
        public virtual DbSet<DeliveryBill> DeliveryBill { get; set; }

    }
}