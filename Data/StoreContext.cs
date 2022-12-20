using Projektna.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Projektna.Data
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        { 
            

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set;}
        public DbSet<Branch> Branches { get; set; } 
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Trim> Trims { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<Trim>().ToTable("Trim");
            
        }
     
    }
}
