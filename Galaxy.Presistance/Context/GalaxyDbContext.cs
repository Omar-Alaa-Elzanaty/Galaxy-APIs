using System.Reflection;
using Galaxy.Domain.Identity;
using Galaxy.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.Presistance.Context
{
    public class GalaxyDbContext : IdentityDbContext<ApplicationUser>
    {
        public GalaxyDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Account");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Account");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Account");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Account");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Account");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Account");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Account");

            modelBuilder.HasDefaultSchema("Galaxy");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Stock> ItemInStock { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CustomerInvoice> CusotmersInvoices { get; set; }
        public DbSet<SupplierInovice> SuppliersInovices { get; set; }

    }
}
